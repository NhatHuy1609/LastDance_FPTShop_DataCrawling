using database_api.Models;

namespace database_api.Interfaces
{
    public interface IMonitorRepository
    {
        Task<PaginatedResult<Entities.Monitor>> GetMonitorsAsync(
            int limit, 
            string? cursor, 
            string? name = null,
            string? category = null,
            double? minPrice = null,
            double? maxPrice = null,
            string? sortBy = null,
            bool isDescending = false);
        Task<Entities.Monitor> GetMonitorById(int id);
        Task<Entities.Monitor> CreateMonitor(Entities.Monitor monitor);
    }
}