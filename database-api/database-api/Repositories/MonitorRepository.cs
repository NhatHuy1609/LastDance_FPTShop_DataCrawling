using database_api.Data;
using database_api.Interfaces;
using database_api.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace database_api.Repositories
{
    public class MonitorRepository : IMonitorRepository
    {
        private readonly ApplicationDbContext _context;
        public MonitorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedResult<Entities.Monitor>> GetMonitorsAsync(
            int limit, 
            string? cursor, 
            string? name = null,
            string? category = null,
            double? minPrice = null,
            double? maxPrice = null,
            string? sortBy = null,
            bool isDescending = false)
        {
            var query = _context.Monitors.AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(name))
            {
                var search = name.ToLower();
                query = query.Where(m => m.Name.ToLower().Contains(search));
            }

            if (!string.IsNullOrEmpty(category))
            {
                var categorySearch = category.ToLower();
                query = query.Where(m => m.Category.ToLower().Contains(categorySearch));
            }

            if (minPrice.HasValue)
            {
                query = query.Where(m => m.PriceDiscount >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(m => m.PriceDiscount <= maxPrice.Value);
            }

            // Apply cursor pagination
            if (!string.IsNullOrEmpty(cursor))
            {
                var cursorId = int.Parse(cursor);
                query = query.Where(m => m.Id > cursorId);
            }

            // Apply sorting
            IOrderedQueryable<Entities.Monitor> orderedQuery;
            
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

            return new PaginatedResult<Entities.Monitor>
            {
                Items = items,
                NextCursor = nextCursor,
                HasMore = hasMore
            };
        }

        public async Task<Entities.Monitor> CreateMonitor(Entities.Monitor monitor)
        {
            await _context.Monitors.AddAsync(monitor);
            await _context.SaveChangesAsync();
            return monitor;
        }

        public async Task<Entities.Monitor> GetMonitorById(int id)
        {
            var monitor = await _context.Monitors.FindAsync(id);
            if (monitor == null)
            {
                throw new Exception("Monitor not found");
            }
            return monitor;
        }
    }
}