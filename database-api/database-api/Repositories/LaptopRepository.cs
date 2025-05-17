using database_api.Data;
using database_api.Entities;
using database_api.Interfaces;
using database_api.Models;
using Microsoft.EntityFrameworkCore;

namespace database_api.Repositories
{
    public class LaptopRepository : ILaptopRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public LaptopRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Laptop> AddLaptopAsync(Laptop laptop)
        {
            await _dbContext.Laptops.AddAsync(laptop);
            await _dbContext.SaveChangesAsync();
            return laptop;
        }

        public async Task<Laptop?> GetLaptopByIdAsync(int id)
        {
            return await _dbContext.Laptops.FindAsync(id);
        }

        public async Task<PaginatedResult<Laptop>> GetLaptopsAsync(int limit, string? cursor)
        {
            int cursorId = 0;
            
            if (!string.IsNullOrEmpty(cursor))
            {
                int.TryParse(cursor, out cursorId);
            }

            var query = _dbContext.Laptops.AsQueryable();
            
            if (cursorId > 0)
            {
                query = query.Where(l => l.Id > cursorId);
            }

            // Get one more item to determine if there are more items
            var laptops = await query.OrderBy(l => l.Id)
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

            return new PaginatedResult<Laptop>
            {
                Items = laptops,
                NextCursor = nextCursor,
                HasMore = hasMore
            };
        }
    }
} 