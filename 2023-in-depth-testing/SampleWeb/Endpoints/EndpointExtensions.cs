using SampleWeb.Endpoints.Lookups;

namespace SampleWeb.Endpoints;

internal static class EndpointExtensions
{
    internal static WebApplication UseMinimalEndpoints(this WebApplication app)
        => app.UseLookupEndpoints();
}
