using SampleWeb.Repositories;

namespace SampleWeb.Endpoints;

internal sealed class LookupDeleteEndpoint
{
    private readonly ILookupRepository repository;

    public LookupDeleteEndpoint(ILookupRepository lookupRepository)
    {
        this.repository = lookupRepository;
    }

    public async Task<IResult> Execute(int id)
    {
        await this.repository.DeleteAsync(id);
        return Results.NoContent();
    }
}
