using database_api.Dtos.Monitor;
using database_api.Models;

namespace database_api.Interfaces
{
    public interface IMonitorRepository
    {
        Task<PagedResult<Entities.Monitor>> GetAllMonitors(QueryParams queryParams);
        Task<Entities.Monitor> GetMonitorById(int id);
        Task<Entities.Monitor> CreateMonitor(Entities.Monitor monitor);
    }
}