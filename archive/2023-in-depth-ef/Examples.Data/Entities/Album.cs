namespace Examples.Data.Entities;

public class Album
{
    public int AlbumId { get; set; }
    public string Title { get; set; } = null!;
    public int ArtistId { get; set; }
    public Artist Artist { get; set; } = null!;
    public ICollection<Track> Tracks { get; set; } = new List<Track>();
}
