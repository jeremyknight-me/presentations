using DataPersistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SampleWeb.Endpoints;
using SampleWeb.Endpoints.Lookups;
using SampleWeb.Endpoints.Lookups.Create;
using SampleWeb.Endpoints.Lookups.Delete;
using SampleWeb.Endpoints.Lookups.GetAll;
using SampleWeb.Endpoints.Lookups.GetById;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddDbContext<SimpleContext>(options =>
{
    var connectionString = ConnectionStringFactory.Make();
    options.UseSqlServer(connectionString);
});

services.AddValidatorsFromAssemblyContaining<Program>();

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
