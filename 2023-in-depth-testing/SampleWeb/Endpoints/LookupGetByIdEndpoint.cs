using SampleWeb.Repositories;
using SampleWeb.Responses;

namespace SampleWeb.Endpoints;

internal sealed class LookupGetByIdEndpoint
{
    private readonly ILookupRepository repository;

    public LookupGetByIdEndpoint(ILookupRepository lookupRepository)
    {
        this.repository = lookupRepository;
    }

    public async Task<IResult> Execute(int id)
	{
		var lookup = await this.repository.GetByIdAsync(id);
        if (lookup is null)
        {
            return Results.NotFound();
        }

        var response = LookupResponse.Create(lookup);
		return Results.Ok(response);
	}
}
