namespace Examples.Data.Entities;

public class MediaType
{
    public int MediaTypeId { get; set; }
    public string? Name { get; set; }
    public ICollection<Track> Tracks { get; set; } = new List<Track>();
}
