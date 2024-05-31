namespace Examples.Data.Entities;

public partial class Artist
{
    public int ArtistId { get; set; }
    public string? Name { get; set; }
    public ICollection<Album> Albums { get; set; } = new List<Album>();
}
