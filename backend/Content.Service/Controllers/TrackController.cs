using Microsoft.AspNetCore.Mvc;

namespace ContentService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrackController : ControllerBase
    {
        // Temporary in-memory list of tracks (replace with a database in a real application)
        private static List<Track> tracks = new List<Track>();

        // GET: api/track
        [HttpGet]
        public ActionResult<IEnumerable<Track>> GetTracks()
        {
            return Ok(tracks);
        }

        // GET: api/track/{id}
        [HttpGet("{id}")]
        public ActionResult<Track> GetTrack(int id)
        {
            var track = tracks.FirstOrDefault(u => u.Id == id);
            if (track == null)
            {
                return NotFound();
            }
            return Ok(track);
        }

        // POST: api/track
        [HttpPost]
        public ActionResult<Track> CreateTrack(Track track)
        {
            track.Id = tracks.Count > 0 ? tracks.Max(u => u.Id) + 1 : 1; // Simple ID generation
            tracks.Add(track);
            return CreatedAtAction(nameof(GetTrack), new { id = track.Id }, track);
        }

        // PUT: api/track/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateTrack(int id, Track updatedTrack)
        {
            var track = tracks.FirstOrDefault(u => u.Id == id);
            if (track == null)
            {
                return NotFound();
            }

            track.title = updatedTrack.title;
            return NoContent();
        }

        // DELETE: api/track/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteTrack(int id)
        {
            var track = tracks.FirstOrDefault(u => u.Id == id);
            if (track == null)
            {
                return NotFound();
            }

            tracks.Remove(track);
            return NoContent();
        }
    }
}
