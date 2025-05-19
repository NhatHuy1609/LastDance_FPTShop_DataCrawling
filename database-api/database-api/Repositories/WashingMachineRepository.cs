using database_api.Data;
using database_api.Entities;
using database_api.Interfaces;
using database_api.Models;
using Microsoft.EntityFrameworkCore;

namespace database_api.Repositories;

public class WashingMachineRepository : IWashingMachineRepository
{
    
    private readonly ApplicationDbContext _context;

    public WashingMachineRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<PaginatedResult<WashingMachine>> GetWashingMachineAsync(int limit, string? cursor)
    {
        int cursorId = 0;
            
        if (!string.IsNullOrEmpty(cursor))
        {
            int.TryParse(cursor, out cursorId);
        }

        var query = _context.WashingMachines.AsQueryable();
            
        if (cursorId > 0)
        {
            query = query.Where(l => l.Id > cursorId);
        }
    
        query = query.Where(w => w.Price != 0);
        var washingMachines = await query.OrderBy(w => w.Id)
            .Take(limit + 1)
            .ToListAsync();

        bool hasMore = washingMachines.Count > limit;
        if (hasMore)
        {
            washingMachines.RemoveAt(washingMachines.Count - 1);
        }

        string? nextCursor = null;
        if (hasMore && washingMachines.Any())
        {
            nextCursor = washingMachines.Last().Id.ToString();
        }

        return new PaginatedResult<WashingMachine>
        {
            Items = washingMachines,
            NextCursor = nextCursor,
            HasMore = hasMore
        };
    }

    public async Task<WashingMachine?> GetWashingMachineByIdAsync(int id)
    {
        return await _context.WashingMachines.FindAsync(id);
    }

    public async Task<WashingMachine> AddWashingMachineAsync(WashingMachine washingMachine)
    {
        await _context.WashingMachines.AddAsync(washingMachine);
        await _context.SaveChangesAsync();
        return washingMachine;
    }
}