using database_api.Entities;
using database_api.Models;

namespace database_api.Interfaces
{
    public interface ITelevisionRepository
    {
        Task<PaginatedResult<Television>> GetTelevisionsAsync(
          int limit,
          string? cursor,
          string? name = null,
          double? minPrice = null,
          double? maxPrice = null,
          string? sortBy = null,
          bool isDescending = false);
        Task<Television?> GetTelevisionByIdAsync(int id);
        Task<Television> AddTelevisionAsync(Television laptop);
    }
}
