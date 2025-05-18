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

        public async Task<GamingGear> AddGamingGearAsync(GamingGear gamingGear)
        {
            await _dbContext.GamingGears.AddAsync(gamingGear);
            await _dbContext.SaveChangesAsync();
            return gamingGear;
        }

        public async Task<GamingGear?> GetGamingGearByIdAsync(int id)
        {
            return await _dbContext.GamingGears.FindAsync(id);
        }

        public async Task<PaginatedResult<GamingGear>> GetGamingGearsAsync(int limit, string? cursor)
        {
            int cursorId = 0;

            if (!string.IsNullOrEmpty(cursor))
            {
                int.TryParse(cursor, out cursorId);
            }

            var query = _dbContext.GamingGears.AsQueryable();

            if (cursorId > 0)
            {
                query = query.Where(g => g.Id > cursorId);
            }

            var gamingGears = await query.OrderBy(g => g.Id)
                .Take(limit + 1)
                .ToListAsync();

            bool hasMore = gamingGears.Count > limit;

            if (hasMore)
            {
                gamingGears.RemoveAt(gamingGears.Count - 1);
            }

            string? nextCursor = null;
            if (hasMore && gamingGears.Any())
            {
                nextCursor = gamingGears.Last().Id.ToString();
            }

            return new PaginatedResult<GamingGear>
            {
                Items = gamingGears,
                HasMore = hasMore,
                NextCursor = nextCursor
            };
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
