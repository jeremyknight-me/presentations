﻿using System;
using System.Collections.Generic;

namespace Examples.Data.Entities;

public partial class MediaType
{
    public int MediaTypeId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
}
