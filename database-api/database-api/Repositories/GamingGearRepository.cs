using database_api.Data;
using database_api.Entities;
using database_api.Interfaces;
using database_api.Models;
using Microsoft.EntityFrameworkCore;

namespace database_api.Repositories
{
    public class GamingGearRepository : IGamingGearRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public GamingGearRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginatedResult<GamingGear>> GetGamingGearsAsync(
            int limit, 
            string? cursor, 
            string? name = null,
            string? category = null,
            double? minPrice = null,
            double? maxPrice = null,
            string? sortBy = null,
            bool isDescending = false)
        {
            var query = _dbContext.GamingGears.AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(name))
            {
                var search = name.ToLower();
                query = query.Where(g => g.Name.ToLower().Contains(search));
            }

            if (!string.IsNullOrEmpty(category))
            {
                var categorySearch = category.ToLower();
                query = query.Where(g => g.Category.ToLower().Contains(categorySearch));
            }

            if (minPrice.HasValue)
            {
                query = query.Where(g => g.PriceDiscount >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(g => g.PriceDiscount <= maxPrice.Value);
            }

            // Apply cursor pagination
            if (!string.IsNullOrEmpty(cursor))
            {
                var cursorId = int.Parse(cursor);
                query = query.Where(g => g.Id > cursorId);
            }

            // Apply sorting
            IOrderedQueryable<GamingGear> orderedQuery;
            
            switch (sortBy?.ToLower())
            {
                case "name":
                    orderedQuery = isDescending 
                        ? query.OrderByDescending(g => g.Name) 
                        : query.OrderBy(g => g.Name);
                    break;
                case "category":
                    orderedQuery = isDescending 
                        ? query.OrderByDescending(g => g.Category) 
                        : query.OrderBy(g => g.Category);
                    break;
                case "price":
                    orderedQuery = isDescending 
                        ? query.OrderByDescending(g => g.PriceDiscount) 
                        : query.OrderBy(g => g.PriceDiscount);
                    break;
                default:
                    orderedQuery = isDescending 
                        ? query.OrderByDescending(g => g.Id) 
                        : query.OrderBy(g => g.Id);
                    break;
            }

            // Get one extra item to determine if there are more results
            var items = await orderedQuery
                .Take(limit + 1)
                .ToListAsync();

            var hasMore = items.Count > limit;
            if (hasMore)
            {
                items.RemoveAt(items.Count - 1);
            }

            // Use the last item's ID as the next cursor
            var nextCursor = items.Count > 0 ? items.Last().Id.ToString() : null;

            return new PaginatedResult<GamingGear>
            {
                Items = items,
                NextCursor = nextCursor,
                HasMore = hasMore
            };
        }

        public async Task<GamingGear> AddGamingGearAsync(GamingGear gamingGear)
        {
            await _dbContext.GamingGears.AddAsync(gamingGear);
            await _dbContext.SaveChangesAsync();
            return gamingGear;
        }

        public async Task<GamingGear> GetGamingGearByIdAsync(int id)
        {
            var gamingGear = await _dbContext.GamingGears.FindAsync(id);
            if (gamingGear == null)
            {
                throw new Exception("GamingGear not found");
            }
            return gamingGear;
        }

        public async Task<PagedResult<GamingGear>> GetGamingGearsPagedAsync(int pageNumber, int pageSize)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            pageSize = pageSize < 1 ? 10 : pageSize;

            var totalItems = await _dbContext.GamingGears.CountAsync();
            
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            
            var skip = (pageNumber - 1) * pageSize;
            var items = await _dbContext.GamingGears
                .OrderBy(g => g.Id)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
            
            return new PagedResult<GamingGear>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
        }

        public async Task<PagedResult<GamingGear>> SearchGamingGearsAsync(
            string? keyword,
            string? category,
            double? minPrice,
            double? maxPrice,
            bool? isAvailable,
            int pageNumber,
            int pageSize)
        {
            // Đảm bảo pageNumber và pageSize hợp lệ
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            pageSize = pageSize < 1 ? 10 : pageSize;

            // Tạo query ban đầu
            var query = _dbContext.GamingGears.AsQueryable();

            // Áp dụng các bộ lọc nếu được cung cấp
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                // Tìm kiếm theo tên (case insensitive)
                keyword = keyword.ToLower();
                query = query.Where(g => g.Name.ToLower().Contains(keyword));
            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                // Lọc theo category
                query = query.Where(g => g.Category == category);
            }

            if (minPrice.HasValue)
            {
                // Lọc theo giá tối thiểu
                query = query.Where(g => g.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                // Lọc theo giá tối đa
                query = query.Where(g => g.Price <= maxPrice.Value);
            }

            if (isAvailable.HasValue)
            {
                // Lọc theo trạng thái có sẵn
                // Lưu ý: Cần chuyển đổi isAvailable từ string sang bool nếu thay đổi kiểu dữ liệu
                // Hiện tại đang giả định isAvailable là string
                var availabilityString = isAvailable.Value ? "true" : "false";
                query = query.Where(g => g.isAvailable == availabilityString);
            }

            // Đếm tổng số items sau khi áp dụng bộ lọc
            var totalItems = await query.CountAsync();

            // Tính tổng số trang
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            // Lấy dữ liệu cho trang hiện tại
            var skip = (pageNumber - 1) * pageSize;
            var items = await query
                .OrderBy(g => g.Id)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            // Tạo kết quả phân trang
            return new PagedResult<GamingGear>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
        }
    }
}
