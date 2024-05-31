using FluentValidation;
using Microsoft.Data.SqlClient;
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
var services = builder.Services;
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddDbContext<SimpleContext>(options =>
{
    var baseConnection = builder.Configuration.GetConnectionString("Simple");
    var connectionStringBuilder = new SqlConnectionStringBuilder(baseConnection)
    {
        // get password from user secrets file
        Password = builder.Configuration.GetValue<string>("DbPassword")
    };
    var connectionString = connectionStringBuilder.ToString();
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMinimalEndpoints();
app.Run();

public partial class Program
{
    // force Program to be public
}
