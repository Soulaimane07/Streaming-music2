using MongoDB.Driver;
using ContentService.Entities;

namespace ContentService.Repositories
{
    public class AlbumsRepo
    {
        private const string collectionName = "albums";
        private readonly IMongoCollection<Album> dbCollection;

        public AlbumsRepo(IMongoDatabase database)
        {
            dbCollection = database.GetCollection<Album>(collectionName);
        }

        public async Task<List<Album>> GetAllAlbumsAsync()
        {
            return await dbCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Album> GetAlbumByIdAsync(Guid id)
        {
            return await dbCollection.Find(album => album.id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAlbumAsync(Album album)
        {
            await dbCollection.InsertOneAsync(album);
        }

        public async Task UpdateAlbumAsync(Guid id, Album updatedAlbum)
        {
            await dbCollection.ReplaceOneAsync(album => album.id == id, updatedAlbum);
        }

        public async Task DeleteAlbumAsync(Guid id)
        {
            await dbCollection.DeleteOneAsync(album => album.id == id);
        }
    }
}
