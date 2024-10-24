using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleApi.Data;

namespace SimpleApi;

internal static class SimpleApiEndpoints
{
    internal static WebApplication MapEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/todos");

        group.MapGet("/", async (
            [FromServices] TodoContext context,
            CancellationToken cancellationToken) =>
        {
            var todos = await context.Todos.ToListAsync(cancellationToken);
            return Results.Ok(todos);
        });

        group.MapPost("/", async (
            [FromBody] TodoCreateRequest request,
            [FromServices] TodoContext context) =>
        {
            var todo = new Todo { Text = request.Text };
            await context.Todos.AddAsync(todo);
            _ = await context.SaveChangesAsync();
            return Results.Created();
        });

        group.MapPut("/{id:int}", async (
            [FromRoute] int id,
            [FromBody] TodoUpdateRequest request,
            [FromServices] TodoContext context) =>
        {
            await context.Todos.Where(t => t.Id == id)
                .ExecuteUpdateAsync(s => s.SetProperty(x => x.Text, request.Text));
            return Results.Ok();
        });

        group.MapDelete("/{id:int}", async (
            [FromRoute] int id,
            [FromServices] TodoContext context) =>
        {
            await context.Todos.Where(t => t.Id == id).ExecuteDeleteAsync();
            return Results.Ok();
        });

        return app;
    }
}

internal record TodoCreateRequest(string Text);
internal record TodoUpdateRequest(string Text);