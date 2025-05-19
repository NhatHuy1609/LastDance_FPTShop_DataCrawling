using database_api.Data;
using database_api.Dtos.Monitor;
using database_api.Interfaces;
using database_api.Mappers;
using database_api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
            // Load all monitors to perform in-memory filtering
            var monitorList = await _context.Monitors.ToListAsync();

            // Xử lý tìm kiếm không dấu
            if (!string.IsNullOrEmpty(queryParams.SearchTerm))
            {
                var search = queryParams.SearchTerm.RemoveDiacritics().ToLower();
                monitorList = monitorList
                    .Where(m => m.Name.RemoveDiacritics().ToLower().Contains(search))
                    .ToList();
            }

            // Filter by category - improve to handle partial matches
            if (!string.IsNullOrEmpty(queryParams.Category))
            {
                var categorySearch = queryParams.Category.RemoveDiacritics().ToLower();
                monitorList = monitorList
                    .Where(m => m.Category.RemoveDiacritics().ToLower().Contains(categorySearch))
                    .ToList();
            }
            
            // Filter by price - only consider discount price as requested
            if (queryParams.MinPrice != null)
            {
                monitorList = monitorList
                    .Where(m => m.PriceDiscount >= queryParams.MinPrice)
                    .ToList();
            }
            if (queryParams.MaxPrice != null)
            {
                monitorList = monitorList
                    .Where(m => m.PriceDiscount <= queryParams.MaxPrice)
                    .ToList();
            }

            // Apply sorting to the filtered list
            var sortedList = monitorList;
            
            switch (queryParams.SortBy?.ToLower())
            {
                case "name":
                    sortedList = queryParams.IsDescending
                        ? sortedList.OrderByDescending(m => m.Name).ToList()
                        : sortedList.OrderBy(m => m.Name).ToList();
                    break;

                case "category":
                    sortedList = queryParams.IsDescending
                        ? sortedList.OrderByDescending(m => m.Category).ToList()
                        : sortedList.OrderBy(m => m.Category).ToList();
                    break;

                case "price":
                    sortedList = queryParams.IsDescending
                        ? sortedList.OrderByDescending(m => m.PriceDiscount).ToList()
                        : sortedList.OrderBy(m => m.PriceDiscount).ToList();
                    break;

                default:
                    sortedList = queryParams.IsDescending
                        ? sortedList.OrderByDescending(m => m.Id).ToList()
                        : sortedList.OrderBy(m => m.Id).ToList();
                    break;
            }

            // Calculate pagination values
            var totalItems = sortedList.Count;
            var totalPages = (int)Math.Ceiling(totalItems / (double)queryParams.PageSize);

            // Apply pagination
            var pagedMonitors = sortedList
                .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .ToList();

            // Create and return paged result
            var result = new PagedResult<Entities.Monitor>
            {
                Items = pagedMonitors,
                TotalItems = totalItems,
                PageNumber = queryParams.PageNumber,
                PageSize = queryParams.PageSize,
                TotalPages = totalPages
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