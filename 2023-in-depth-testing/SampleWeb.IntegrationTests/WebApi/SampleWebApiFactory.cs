using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace SampleWeb.IntegrationTests.WebApi;

public class SampleWebApiFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            //// remove dbcontext
            //var descriptor
            //    = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<SimpleContext>));
            //if (descriptor is not null)
            //{
            //    services.Remove(descriptor);
            //}

            // add test dbcontext
        });

        return base.CreateHost(builder);
    }
}
