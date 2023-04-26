namespace Examples.Data.Entities;

public class Playlist
{
    public int PlaylistId { get; set; }
    public string? Name { get; set; }
    public ICollection<Track> Tracks { get; set; } = new List<Track>();
}
