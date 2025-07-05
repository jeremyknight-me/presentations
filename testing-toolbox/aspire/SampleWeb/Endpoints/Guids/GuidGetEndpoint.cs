namespace SampleWeb.Endpoints.Guids;

internal sealed class GuidGetEndpoint
{
    public Task<IResult> Execute()
    {
        var response = new GuidResponse { Guid = Guid.NewGuid() };
        return Task.FromResult(Results.Ok(response));
    }
}
