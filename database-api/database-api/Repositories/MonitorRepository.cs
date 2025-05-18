using database_api.Data;
using database_api.Dtos.Monitor;
using database_api.Interfaces;
using database_api.Mappers;
using database_api.Models;
using Microsoft.EntityFrameworkCore;

namespace database_api.Repositories
{
    public class MonitorRepository : IMonitorRepository
    {
        private readonly ApplicationDbContext _context;
        public MonitorRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Entities.Monitor> CreateMonitor(Entities.Monitor monitor)
        {
            await _context.Monitors.AddAsync(monitor);
            await _context.SaveChangesAsync();
            return monitor;
        }

        public async Task<PagedResult<Entities.Monitor>> GetAllMonitors(QueryParams queryParams)
        {
            var monitors = _context.Monitors.AsQueryable();

            // Filter by name
            var monitorList = await _context.Monitors.ToListAsync(); // Chuyển sang list để bypass lỗi EF Core

            // Xử lý tìm kiếm không dấu
            if (!string.IsNullOrEmpty(queryParams.SearchTerm))
            {
                var search = queryParams.SearchTerm.RemoveDiacritics().ToLower();
                monitorList = monitorList
                    .Where(m => m.Name.RemoveDiacritics().ToLower().Contains(search))
                    .ToList();
            }

            // Filter by price
            if (queryParams.MinPrice != null)
            {
                monitors = monitors.Where(m => m.Price >= queryParams.MinPrice);
            }
            if (queryParams.MaxPrice != null)
            {
                monitors = monitors.Where(m => m.Price <= queryParams.MaxPrice);
            }

            // Filter by discount
            if (queryParams.MinDiscount != null)
            {
                monitors = monitors.Where(m => m.PriceDiscount >= queryParams.MinDiscount);
            }
            if (queryParams.MaxDiscount != null)
            {
                monitors = monitors.Where(m => m.PriceDiscount <= queryParams.MaxDiscount);
            }
            // Sorting
            switch (queryParams.SortBy)
            {
                case "name":
                    monitors = queryParams.IsDescending
                        ? monitors.OrderByDescending(m => m.Name)
                        : monitors.OrderBy(m => m.Name);
                    break;

                case "price":
                    monitors = queryParams.IsDescending
                        ? monitors.OrderByDescending(m => m.Price)
                        : monitors.OrderBy(m => m.Price);
                    break;

                case "priceDiscount":
                    monitors = queryParams.IsDescending
                        ? monitors.OrderByDescending(m => m.PriceDiscount)
                        : monitors.OrderBy(m => m.PriceDiscount);
                    break;

                default:
                    monitors = queryParams.IsDescending
                        ? monitors.OrderByDescending(m => m.Id)
                        : monitors.OrderBy(m => m.Id);
                    break;
            }

            var items = await monitors.Skip((queryParams.PageNumber - 1) * queryParams.PageSize).Take(queryParams.PageSize).ToListAsync();
            var totalCount = await monitors.CountAsync();

            // Pagination
            var result = new PagedResult<Entities.Monitor>
            {
                Items = items,
                TotalItems = totalCount,
                PageNumber = queryParams.PageNumber,
                PageSize = queryParams.PageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)queryParams.PageSize)
            };
            return result;
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