using Microsoft.AspNetCore.Mvc;
using ContentService.Entities;
using ContentService.Repositories;

namespace ContentService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtistsController : ControllerBase
    {
        private readonly ArtistsRepo _artistsRepo;

        public ArtistsController(ArtistsRepo artistsRepo)
        {
            _artistsRepo = artistsRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtists()
        {
            var artists = await _artistsRepo.GetAllArtistsAsync();
            return Ok(artists);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Artist>> GetArtist(Guid id)
        {
            var artist = await _artistsRepo.GetArtistByIdAsync(id);
            if (artist == null)
            {
                return NotFound();
            }
            return Ok(artist);
        }

        [HttpPost]
        public async Task<ActionResult<Artist>> CreateArtist([FromBody] Artist artist)
        {
            await _artistsRepo.CreateArtistAsync(artist);
            return CreatedAtAction(nameof(GetArtist), new { id = artist.id }, artist);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArtist(Guid id, [FromBody] Artist updatedArtist)
        {
            var artist = await _artistsRepo.GetArtistByIdAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            await _artistsRepo.UpdateArtistAsync(id, updatedArtist);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(Guid id)
        {
            var artist = await _artistsRepo.GetArtistByIdAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            await _artistsRepo.DeleteArtistAsync(id);
            return NoContent();
        }
    }
}
