using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SampleWeb.Endpoints;
using SampleWeb.Endpoints.Guids;
using SampleWeb.Endpoints.Lookups;
using SampleWeb.Endpoints.Lookups.Create;
using SampleWeb.Endpoints.Lookups.Delete;
using SampleWeb.Endpoints.Lookups.GetAll;
using SampleWeb.Endpoints.Lookups.GetById;
using SampleWeb.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var services = builder.Services;
services.AddEndpointsApiExplorer();

services.AddDbContext<SimpleContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("sample-db")
            ?? throw new InvalidOperationException("Connection string 'sample-db' not found.");
    options.UseSqlServer(connectionString);
});

services.AddValidatorsFromAssemblyContaining<Program>();

services.AddScoped<GuidGetEndpoint>();

services.AddScoped<ILookupRepository, LookupRepository>();
services.AddScoped<LookupGetAllEndpoint>();
services.AddScoped<LookupGetByIdEndpoint>();
services.AddScoped<LookupCreateEndpoint>();
services.AddScoped<LookupDeleteEndpoint>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapDefaultEndpoints(); // aspire integration
app.UseMinimalEndpoints();

app.Run();

// force Program to be public
public partial class Program {}
