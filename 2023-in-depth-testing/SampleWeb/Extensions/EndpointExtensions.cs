using Microsoft.AspNetCore.Mvc;
using SampleWeb.Endpoints;
using SampleWeb.Requests;
using SampleWeb.Responses;

namespace SampleWeb.Extensions;

internal static class EndpointExtensions
{
	internal static WebApplication UseMinimalEndpoints(this WebApplication app)
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
            .MapPost("/", async ([FromServices] LookupPostEndpoint endpoint, LookupPostRequest request)
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
