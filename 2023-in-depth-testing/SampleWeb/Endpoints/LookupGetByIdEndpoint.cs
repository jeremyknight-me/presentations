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
		var response = new LookupGetResponse
		{
			Id = lookup.Id,
			Name = lookup.Name,
			IsDeleted = lookup.IsDeleted
		};
		return Results.Ok(response);
	}
}
