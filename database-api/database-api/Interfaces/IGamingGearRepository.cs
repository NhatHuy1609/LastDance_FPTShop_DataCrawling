using database_api.Entities;
using database_api.Models;

namespace database_api.Interfaces
{
    public interface IGamingGearRepository
    {
        Task<PaginatedResult<GamingGear>> GetGamingGearsAsync(
            int limit, 
            string? cursor, 
            string? name = null,
            string? category = null,
            double? minPrice = null,
            double? maxPrice = null,
            string? sortBy = null,
            bool isDescending = false);
        Task<GamingGear> GetGamingGearByIdAsync(int id);
        Task<GamingGear> AddGamingGearAsync(GamingGear gamingGear);
    }
}
