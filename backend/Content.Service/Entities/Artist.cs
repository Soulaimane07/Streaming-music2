using System;
using System.Collections.Generic;

namespace ContentService.Entities
{
    public class Artist
    {
        public Guid id { get; set; } = Guid.NewGuid(); // Auto-generate a new Guid if not provided
        public string name { get; set; } // Artist's name
        public List<Album> Albums { get; set; } = new List<Album>(); // List of albums associated with the artist
    }
}
