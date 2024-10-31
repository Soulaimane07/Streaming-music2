using System;

namespace ContentService.Entities
{
    public class Track
    {
        public Guid id { get; set; } = Guid.NewGuid(); // Auto-generate a new Guid if not provided
        public string title { get; set; } // Track title
        public Guid albumId { get; set; } // Reference to the associated album
    }
}
