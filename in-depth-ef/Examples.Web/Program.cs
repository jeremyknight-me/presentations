using System.Text.Json.Serialization;
using Examples.Data;
using Examples.Web;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

services.AddDbContext<ChinookContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Chinook"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/status", () => Results.Ok("active"))
    .ExcludeFromDescription();

app.UseApplicationEndpoints();

app.Run();
