namespace Examples.Data.KeylessEntities;

public class ArtistTracks
{
    public int ArtistId { get; set; }
    public string? ArtistName { get; set; }
    public int AlbumId { get; set; }
    public string AlbumTitle { get; set; } = default!;
    public int TrackId { get; set; }
    public string TrackName { get; set; } = default!;
    public decimal TrackPrice { get; set; }
    public string GenreName { get; set; } = default!;
}
