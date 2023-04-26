using DataPersistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SampleWeb.Endpoints;
using SampleWeb.Repositories;

namespace SampleWeb.Extensions;

internal static class DependencyInjectionExtensions
{
	internal static IServiceCollection AddDependencyInjection(this IServiceCollection services)
	{
		services.AddDbContext<SimpleContext>(options =>
		{
			var connectionString = ConnectionStringFactory.Make();
			options.UseSqlServer(connectionString);
		});

        services.AddValidatorsFromAssemblyContaining<Program>();

        services.AddScoped<ILookupRepository, LookupRepository>();

		services.AddScoped<LookupGetAllEndpoint>();
        services.AddScoped<LookupGetByIdEndpoint>();
        services.AddScoped<LookupPostEndpoint>();
        services.AddScoped<LookupDeleteEndpoint>();

		return services;
	}
}
