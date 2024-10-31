using Microsoft.AspNetCore.Mvc;
using ContentService.Entities;
using ContentService.Repositories;

namespace ContentService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumsController : ControllerBase
    {
        private readonly AlbumsRepo _albumsRepo;

        public AlbumsController(AlbumsRepo albumsRepo)
        {
            _albumsRepo = albumsRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Album>>> GetAlbums()
        {
            var albums = await _albumsRepo.GetAllAlbumsAsync();
            return Ok(albums);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Album>> GetAlbum(Guid id)
        {
            var album = await _albumsRepo.GetAlbumByIdAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            return Ok(album);
        }

        [HttpPost]
        public async Task<ActionResult<Album>> CreateAlbum([FromBody] Album album)
        {
            await _albumsRepo.CreateAlbumAsync(album);
            return CreatedAtAction(nameof(GetAlbum), new { id = album.id }, album);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAlbum(Guid id, [FromBody] Album updatedAlbum)
        {
            var album = await _albumsRepo.GetAlbumByIdAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            await _albumsRepo.UpdateAlbumAsync(id, updatedAlbum);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(Guid id)
        {
            var album = await _albumsRepo.GetAlbumByIdAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            await _albumsRepo.DeleteAlbumAsync(id);
            return NoContent();
        }
    }
}
