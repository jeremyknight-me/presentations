namespace BackToBasicsAdoNet.EntityFramework.Chinook;

public partial class Track
{
    public int TrackId { get; set; }
    public string Name { get; set; }
    public int? AlbumId { get; set; }
    public int MediaTypeId { get; set; }
    public int? GenreId { get; set; }
    public string Composer { get; set; }
    public int Milliseconds { get; set; }
    public int? Bytes { get; set; }
    public decimal UnitPrice { get; set; }
    public Album Album { get; set; }
    public Genre Genre { get; set; }
    public ICollection<InvoiceLine> InvoiceLines { get; set; } = new List<InvoiceLine>();
    public MediaType MediaType { get; set; }
    public ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
}
