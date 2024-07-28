using Microsoft.AspNetCore.Mvc;
using SampleWeb.Endpoints.Lookups.Create;
using SampleWeb.Endpoints.Lookups.Delete;
using SampleWeb.Endpoints.Lookups.GetAll;
using SampleWeb.Endpoints.Lookups.GetById;

namespace SampleWeb.Endpoints.Lookups;

internal static class EndpointExtensions
{
    internal static WebApplication UseLookupEndpoints(this WebApplication app)
    {
        var lookups = app
            .MapGroup("/lookups")
            .WithTags("lookups")
            .WithOpenApi();

        lookups
            .MapGet("/", async ([FromServices] LookupGetAllEndpoint endpoint)
                => await endpoint.Execute())
            .Produces<IEnumerable<LookupResponse>>(StatusCodes.Status200OK);

        lookups
            .MapGet("/{id}", async ([FromServices] LookupGetByIdEndpoint endpoint, int id)
                => await endpoint.Execute(id))
            .Produces<LookupResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        lookups
            .MapPost("/", async ([FromServices] LookupCreateEndpoint endpoint, LookupCreateRequest request)
                => await endpoint.Execute(request))
            .Produces<LookupResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

        lookups
            .MapDelete("/{id}", async ([FromServices] LookupDeleteEndpoint endpoint, int id)
                => await endpoint.Execute(id))
            .Produces(StatusCodes.Status204NoContent);

        return app;
    }
}

