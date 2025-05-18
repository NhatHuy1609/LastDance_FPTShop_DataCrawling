using database_api.Entities;
using database_api.Models;

namespace database_api.Interfaces
{
    public interface IGamingGearRepository
    {
        Task<PaginatedResult<GamingGear>> GetGamingGearsAsync(int limit, string? cursor);
        Task<PagedResult<GamingGear>> GetGamingGearsPagedAsync(int pageNumber, int pageSize);
        Task<PagedResult<GamingGear>> SearchGamingGearsAsync(string? keyword, string? category, double? minPrice, double? maxPrice, bool? isAvailable, int pageNumberint,int pageSize);
        Task<GamingGear?> GetGamingGearByIdAsync(int id);
        Task<GamingGear> AddGamingGearAsync(GamingGear gamingGear);
    }
}
