using Microsoft.AspNetCore.Mvc;
using SampleWeb.Endpoints.Guids;
using SampleWeb.Endpoints.Lookups;

namespace SampleWeb.Endpoints;

internal static class EndpointExtensions
{
    internal static WebApplication UseMinimalEndpoints(this WebApplication app)
    {
        app.UseLookupEndpoints();
        app
            .MapGet("/guids/", async ([FromServices] GuidGetEndpoint endpoint)
                => await endpoint.Execute())
            .Produces<LookupResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);
        return app;
    }
}
