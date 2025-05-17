using database_api.Entities;
using database_api.Models;

namespace database_api.Interfaces
{
    public interface ILaptopRepository
    {
        Task<PaginatedResult<Laptop>> GetLaptopsAsync(int limit, string? cursor);
        Task<Laptop?> GetLaptopByIdAsync(int id);
        Task<Laptop> AddLaptopAsync(Laptop laptop);
    }
} 