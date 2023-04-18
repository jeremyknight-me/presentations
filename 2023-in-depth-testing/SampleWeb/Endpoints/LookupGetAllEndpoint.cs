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
		var lookups = await this.repository.GetAllAsync();
		var response = lookups
			.Select(x => new LookupGetResponse
			{
				Id = x.Id,
				Name = x.Name,
				IsDeleted = x.IsDeleted
			});
		return Results.Ok(response);
    }
}

