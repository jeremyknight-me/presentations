using System;
using System.Collections.Generic;

namespace BackToBasicsAdoNet.EntityFramework.Chinook;

public partial class Playlist
{
    public int PlaylistId { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
}
