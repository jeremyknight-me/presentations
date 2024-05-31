namespace BackToBasicsAdoNet.EntityFramework.Chinook;

public class Album
{
    public int AlbumId { get; set; }
    public string Title { get; set; }
    public int ArtistId { get; set; }
    public Artist Artist { get; set; }
    public ICollection<Track> Tracks { get; set; } = new List<Track>();
}
