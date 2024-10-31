using Microsoft.AspNetCore.Mvc;
using ContentService.Entities;
using ContentService.Repositories;

namespace ContentService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TracksController : ControllerBase
    {
        private readonly TracksRepo _tracksRepo;

        public TracksController(TracksRepo tracksRepo)
        {
            _tracksRepo = tracksRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Track>>> GetTracks()
        {
            var tracks = await _tracksRepo.GetAllTracksAsync();
            return Ok(tracks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Track>> GetTrack(Guid id)
        {
            var track = await _tracksRepo.GetTrackByIdAsync(id);
            if (track == null)
            {
                return NotFound();
            }
            return Ok(track);
        }

        [HttpPost]
        public async Task<ActionResult<Track>> CreateTrack([FromBody] Track track)
        {
            await _tracksRepo.CreateTrackAsync(track);
            return CreatedAtAction(nameof(GetTrack), new { id = track.id }, track);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrack(Guid id, [FromBody] Track updatedTrack)
        {
            var track = await _tracksRepo.GetTrackByIdAsync(id);
            if (track == null)
            {
                return NotFound();
            }

            await _tracksRepo.UpdateTrackAsync(id, updatedTrack);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrack(Guid id)
        {
            var track = await _tracksRepo.GetTrackByIdAsync(id);
            if (track == null)
            {
                return NotFound();
            }

            await _tracksRepo.DeleteTrackAsync(id);
            return NoContent();
        }
    }
}
