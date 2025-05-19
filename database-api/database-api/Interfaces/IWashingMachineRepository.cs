using database_api.Entities;
using database_api.Models;

namespace database_api.Interfaces;

public interface IWashingMachineRepository
{
    Task<PaginatedResult<WashingMachine>> GetWashingMachineAsync(int limit, string? cursor);
    Task<WashingMachine?> GetWashingMachineByIdAsync(int id);
    Task<WashingMachine> AddWashingMachineAsync(WashingMachine washingMachine);
}