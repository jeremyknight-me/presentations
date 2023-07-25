namespace BackToBasicsAdoNet.EntityFramework.Chinook;

public class Genre
{
    public int GenreId { get; set; }
    public string Name { get; set; }
    public ICollection<Track> Tracks { get; set; } = new List<Track>();
}
