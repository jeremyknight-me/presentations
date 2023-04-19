using Examples.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Examples.Web;

internal static class Endpoints
{
    internal static void UseApplicationEndpoints(this WebApplication app)
    {
        app.MapGet("/artists", async ([FromServices] ChinookContext context) =>
        {
            var artists
                = await context.Artists.AsNoTracking()
                    .Include(a => a.Albums)
                    .OrderBy(a => a.Name)
                    .Take(100)
                    .ToArrayAsync();
            return Results.Ok(artists);
        });

        app.MapGet("/artists/flat", async ([FromServices] ChinookContext context) =>
        {
            var albumArtists = context.AlbumArtists
                .FromSql($"SELECT ar.ArtistId, ar.Name as ArtistName, al.AlbumId, al.Title as AlbumTitle FROM dbo.Artist ar INNER JOIN dbo.Album al ON ar.ArtistId = al.ArtistId")
                .Take(100)
                .ToArray();
            return Results.Ok(albumArtists);
        });

        app.MapGet("/tracks", async ([FromServices] ChinookContext context) =>
        {
            var query =
                from t in context.Tracks.AsNoTracking()
                join al in context.Albums on t.AlbumId equals al.AlbumId
                join ar in context.Artists on al.ArtistId equals ar.ArtistId
                join g in context.Genres on t.GenreId equals g.GenreId
                join mt in context.MediaTypes on t.MediaTypeId equals mt.MediaTypeId
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

        app.MapGet("/tracks/count", async ([FromServices] ChinookContext context) =>
        {
            // EF7 (new) => only scalar types
            // EF8 => any type including projections
            var count
                = await context.Database
                    .SqlQuery<int>($"SELECT COUNT(TrackId) FROM dbo.Track")
                    .ToListAsync();
            return Results.Ok(count[0]);
        });
    }
}
