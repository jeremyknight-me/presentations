using Examples.Data;
using Examples.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Examples.Web;

internal static class Endpoints
{
    internal static void UseApplicationEndpoints(this WebApplication app)
    {
        app.MapGet("/artists", async ([FromServices] ChinookContext ctx) =>
        {
            var artists
                = await ctx.Artists.AsNoTracking()
                    .Include(a => a.Albums)
                    .OrderBy(a => a.Name)
                    .Take(100)
                    .ToArrayAsync();
            return Results.Ok(artists);
        });

        app.MapGet("/artists/tracks", async ([FromServices] ChinookContext ctx) =>
        {
            var albumArtists = await ctx.AlbumArtists
                .FromSql($@"SELECT
                    ar.ArtistId, ar.Name as ArtistName,
                    al.AlbumId, al.Title as AlbumTitle,
                    t.TrackId, t.Name as TrackName, t.UnitPrice as TrackPrice,
                    g.Name as GenreName,
                    mt.Name as MediaTypeName
                    FROM dbo.Artist ar
                    INNER JOIN dbo.Album al ON ar.ArtistId = al.ArtistId
                    INNER JOIN dbo.Track t on al.AlbumId = t.AlbumId
                    INNER JOIN dbo.Genre g on t.GenreId = g.GenreId
                    INNER JOIN dbo.MediaType mt on t.MediaTypeId = mt.MediaTypeId")
                .Take(100)
                .ToArrayAsync();
            return Results.Ok(albumArtists);
        });

        app.MapGet("/artists/{id}/tracks", async (int id, [FromServices] ChinookContext ctx) =>
        {
            var albumArtists = await ctx.AlbumArtists
                .FromSql($@"SELECT
                    ar.ArtistId, ar.Name as ArtistName,
                    al.AlbumId, al.Title as AlbumTitle,
                    t.TrackId, t.Name as TrackName, t.UnitPrice as TrackPrice,
                    g.Name as GenreName
                    FROM dbo.Artist ar
                    INNER JOIN dbo.Album al ON ar.ArtistId = al.ArtistId
                    INNER JOIN dbo.Track t on al.AlbumId = t.AlbumId
                    INNER JOIN dbo.Genre g on t.GenreId = g.GenreId
                    INNER JOIN dbo.MediaType mt on t.MediaTypeId = mt.MediaTypeId
                    WHERE ar.ArtistId = {id}")
                .ToArrayAsync();
            return Results.Ok(albumArtists);
        });

        app.MapPatch("/artists/{id}/tracks/price/ef", async (int id, decimal price, [FromServices] ChinookContext ctx) =>
        {
            var query =
                from ar in ctx.Artists
                join al in ctx.Albums on ar.ArtistId equals al.ArtistId
                join t in ctx.Tracks on al.AlbumId equals t.AlbumId
                where ar.ArtistId == id
                select t;
            await query.ExecuteUpdateAsync(t
                => t.SetProperty(x => x.UnitPrice, x => price));
            return Results.NoContent();
        });

        app.MapPatch("/artists/{id}/tracks/price/sql", async (int id, decimal price, [FromServices] ChinookContext ctx) =>
        {
            await ctx.Database.ExecuteSqlAsync($@"UPDATE t
                SET t.UnitPrice = {price}
                FROM 
	                dbo.Artist ar
	                INNER JOIN dbo.Album al on ar.ArtistId = al.ArtistId
	                INNER JOIN dbo.Track t on al.AlbumId = t.AlbumId
                WHERE ar.ArtistId = {id}");
            return Results.NoContent();
        });

        app.MapGet("/tracks", async ([FromServices] ChinookContext ctx) =>
        {
            var query =
                from t in ctx.Tracks.AsNoTracking()
                join al in ctx.Albums on t.AlbumId equals al.AlbumId
                join ar in ctx.Artists on al.ArtistId equals ar.ArtistId
                join g in ctx.Genres on t.GenreId equals g.GenreId
                join mt in ctx.MediaTypes on t.MediaTypeId equals mt.MediaTypeId
                select new
                {
                    t.TrackId,
                    t.Name,
                    AlbumName = al.Title,
                    ArtistName = ar.Name,
                    MediaTypeName = mt.Name,
                    GenreName = g.Name,
                    t.Composer,
                    t.Milliseconds,
                    t.Bytes,
                    t.UnitPrice
                };

            var tracks
                = await query.Take(100).ToArrayAsync();

            return Results.Ok(tracks);
        });

        app.MapGet("/tracks/count", async ([FromServices] ChinookContext ctx) =>
        {
            // EF7 (new) => only scalar types
            // EF8 => any type including projections
            var count
                = await ctx.Database
                    .SqlQuery<int>($"SELECT COUNT(TrackId) FROM dbo.Track")
                    .ToListAsync();
            return Results.Ok(count[0]);
        });

        app.MapGet("/tracks/{id}", async (int id, [FromServices] ChinookContext ctx) =>
        {
            var track = await ctx.Tracks.FindAsync(id);
            return Results.Ok(track);
        });

        app.MapPatch("/tracks/{id}/price", async (int id, decimal price, [FromServices] ChinookContext ctx) =>
        {
            var track = new Track()
            {
                TrackId = id,
                UnitPrice = price,
            };
            var entry = ctx.Tracks.Attach(track);
            entry.Property(x => x.UnitPrice).IsModified = true;
            await ctx.SaveChangesAsync();
            return Results.NoContent();
        });

        app.MapDelete("/invoices/{id}/ef", async (int id, [FromServices] ChinookContext ctx) =>
        {
            await ctx.InvoiceLines.Where(x => x.InvoiceId == id).ExecuteDeleteAsync();
            await ctx.Invoices.Where(x => x.InvoiceId == id).ExecuteDeleteAsync();
            return Results.NoContent();
        });

        app.MapDelete("/invoices/{id}/sql", async (int id, [FromServices] ChinookContext ctx) =>
        {
            await ctx.Database.ExecuteSqlAsync(
                $@"DELETE FROM dbo.InvoiceLine WHERE InvoiceId = {id};
                        DELETE FROM dbo.Invoice WHERE InvoiceId = {id};");
            return Results.NoContent();
        });
    }
}
