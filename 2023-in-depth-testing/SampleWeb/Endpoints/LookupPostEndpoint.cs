using DataPersistence;
using SampleWeb.Repositories;
using SampleWeb.Requests;

namespace SampleWeb.Endpoints;

internal sealed class LookupPostEndpoint
{
    private readonly ILookupRepository repository;

    public LookupPostEndpoint(ILookupRepository lookupRepository)
    {
        this.repository = lookupRepository;
    }

    public async Task<IResult> Execute(LookupPostRequest request)
    {
        // todo: validate request

        var lookup = Lookup.Create(request.Name);
        await this.repository.AddAsync(lookup);
        return Results.Created($"/lookups/{lookup.Id}", lookup);
    }
}
