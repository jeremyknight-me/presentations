using DataPersistence;
using Microsoft.EntityFrameworkCore;

namespace SampleWeb.Endpoints.Lookups;

public interface ILookupRepository
{
    Task AddAsync(Lookup lookup);
    Task DeleteAsync(int id);
    Task<IReadOnlyList<Lookup>> GetAllAsync();
    Task<Lookup?> GetByIdAsync(int id);
}

internal sealed class LookupRepository : ILookupRepository
{
    private readonly SimpleContext context;

    public LookupRepository(SimpleContext simpleContext)
    {
        this.context = simpleContext;
    }

    public async Task AddAsync(Lookup lookup)
    {
        this.context.Lookups.Add(lookup);
        await this.context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var lookup = new Lookup { Id = id };
        this.context.Lookups.Remove(lookup);
        await this.context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<Lookup>> GetAllAsync()
        => await this.context.Lookups.ToListAsync();

    public async Task<Lookup?> GetByIdAsync(int id)
        => await this.context.Lookups.FindAsync(id);
}
