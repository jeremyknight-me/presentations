using AdvancedApi.Data;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;

namespace AdvancedApi;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateBootstrapLogger();

        try
        {
            Log.Information("Starting web application");

            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((ctx, svc, loggerConfig) =>
            {
                loggerConfig
                    .ReadFrom.Configuration(ctx.Configuration)
                    .ReadFrom.Services(svc)
                    .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                        .WithDefaultDestructurers()
                        .WithDestructurers([
                            new Serilog.Exceptions.EntityFrameworkCore.Destructurers.DbUpdateExceptionDestructurer()
                        ])
                    )
                    .Enrich.FromLogContext()
                    .WriteTo.Console();
            });

            builder.Services
                .AddOpenTelemetry()
                .ConfigureResource(resource => resource.AddService("Advanced.Api"))
                .WithTracing(tracing =>
                {
                    tracing
                        .AddHttpClientInstrumentation()
                        .AddAspNetCoreInstrumentation()
                        .AddEntityFrameworkCoreInstrumentation(
                            c => c.SetDbStatementForText = true);

                    tracing.AddOtlpExporter();
                });

            builder.Services.AddHealthChecks();

            builder.Services.AddDbContext<TodoContext>(
                options => options.UseNpgsql(builder.Configuration.GetConnectionString("Todos")));

            var app = builder.Build();
            app.UseSerilogRequestLogging();
            app.MapHealthChecks("/healthz");

            app.MapEndpoints();
            app.ApplyTodoMigrations();

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
