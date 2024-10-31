using MongoDB.Driver;
using ContentService.Entities;

namespace ContentService.Repositories
{
    public class ArtistsRepo
    {
        private const string collectionName = "artists";
        private readonly IMongoCollection<Artist> dbCollection;

        public ArtistsRepo(IMongoDatabase database)
        {
            dbCollection = database.GetCollection<Artist>(collectionName);
        }

        public async Task<List<Artist>> GetAllArtistsAsync()
        {
            return await dbCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Artist> GetArtistByIdAsync(Guid id)
        {
            return await dbCollection.Find(artist => artist.id == id).FirstOrDefaultAsync();
        }

        public async Task CreateArtistAsync(Artist artist)
        {
            await dbCollection.InsertOneAsync(artist);
        }

        public async Task UpdateArtistAsync(Guid id, Artist updatedArtist)
        {
            await dbCollection.ReplaceOneAsync(artist => artist.id == id, updatedArtist);
        }

        public async Task DeleteArtistAsync(Guid id)
        {
            await dbCollection.DeleteOneAsync(artist => artist.id == id);
        }
    }
}
