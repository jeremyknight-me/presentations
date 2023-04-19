using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples.Data.KeylessEntities;

public class AlbumArtist
{
    public int ArtistId { get; set; }
    public string? ArtistName { get; set; }
    public int AlbumId { get; set; }
    public string AlbumTitle { get; set; } = default!;
}
