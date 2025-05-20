using database_api.Data;
using database_api.Entities;
using database_api.Interfaces;
using database_api.Models;
using Microsoft.EntityFrameworkCore;

namespace database_api.Repositories
{
    public class TelevisionRepository : ITelevisionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TelevisionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Television> AddTelevisionAsync(Television laptop)
        {
            await _dbContext.Televisions.AddAsync(laptop);
            await _dbContext.SaveChangesAsync();
            return laptop;
        }

        public async Task<Television?> GetTelevisionByIdAsync(int id)
        {
            return await _dbContext.Televisions.FindAsync(id);
        }

        public async Task<PaginatedResult<Television>> GetTelevisionsAsync(
            int limit,
            string? cursor,
            string? name = null,
            double? minPrice = null,
            double? maxPrice = null,
            string? sortBy = null,
            bool isDescending = false)
        {
            int cursorId = 0;

            if (!string.IsNullOrEmpty(cursor))
            {
                int.TryParse(cursor, out cursorId);
            }

            var query = _dbContext.Televisions.AsQueryable();

            // Apply cursor pagination
            if (cursorId > 0)
            {
                query = query.Where(l => l.Id > cursorId);
            }

            // Apply sorting
            IOrderedQueryable<Television> orderedQuery;

            switch (sortBy?.ToLower())
            {
                case "name":
                    orderedQuery = isDescending
                        ? query.OrderByDescending(m => m.Name)
                        : query.OrderBy(m => m.Name);
                    break;
                case "category":
                    orderedQuery = isDescending
                        ? query.OrderByDescending(m => m.Category)
                        : query.OrderBy(m => m.Category);
                    break;
                case "price":
                    orderedQuery = isDescending
                        ? query.OrderByDescending(m => m.PriceDiscount)
                        : query.OrderBy(m => m.PriceDiscount);
                    break;
                default:
                    orderedQuery = isDescending
                        ? query.OrderByDescending(m => m.Id)
                        : query.OrderBy(m => m.Id);
                    break;
            }

            // Get one more item to determine if there are more items
            var laptops = await orderedQuery
                                    .Take(limit + 1)
                                    .ToListAsync();

            bool hasMore = laptops.Count > limit;

            // Remove the extra item if we fetched one more than requested
            if (hasMore)
            {
                laptops.RemoveAt(laptops.Count - 1);
            }

            string? nextCursor = null;
            if (hasMore && laptops.Any())
            {
                nextCursor = laptops.Last().Id.ToString();
            }

            return new PaginatedResult<Television>
            {
                Items = laptops,
                NextCursor = nextCursor,
                HasMore = hasMore
            };
        }
    }
}
