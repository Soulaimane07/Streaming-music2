using System;
using System.Collections.Generic;

namespace ContentService.Entities
{
    public class Album
    {
        public Guid id { get; set; } = Guid.NewGuid(); // Auto-generate a new Guid if not provided
        public string title { get; set; } // Album title
        public List<Track> tracks { get; set; } = new List<Track>(); // List of tracks in the album
        public Guid artistId { get; set; } // Reference to the associated artist
    }
}
