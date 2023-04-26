using SampleWeb.Repositories;
using SampleWeb.Responses;

namespace SampleWeb.Endpoints;

internal sealed class LookupGetAllEndpoint
{
	private readonly ILookupRepository repository;

	public LookupGetAllEndpoint(ILookupRepository lookupRepository)
    {
		this.repository = lookupRepository;
	}

    public async Task<IResult> Execute()
    {
        try
        {
            var lookups = await this.repository.GetAllAsync();
            var response = lookups
                .Select(x => LookupResponse.Create(x));
            return Results.Ok(response);
        } 
		catch (Exception ex)
        {
            throw;
        }
    }
}

