using MongoDB.Driver;
using ContentService.Entities;

namespace ContentService.Repositories
{
    public class TracksRepo
    {
        private const string collectionName = "tracks";
        private readonly IMongoCollection<Track> dbCollection;

        public TracksRepo(IMongoDatabase database)
        {
            dbCollection = database.GetCollection<Track>(collectionName);
        }

        public async Task<List<Track>> GetAllTracksAsync()
        {
            return await dbCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Track> GetTrackByIdAsync(Guid id)
        {
            return await dbCollection.Find(track => track.id == id).FirstOrDefaultAsync();
        }

        public async Task CreateTrackAsync(Track track)
        {
            await dbCollection.InsertOneAsync(track);
        }

        public async Task UpdateTrackAsync(Guid id, Track updatedTrack)
        {
            await dbCollection.ReplaceOneAsync(track => track.id == id, updatedTrack);
        }

        public async Task DeleteTrackAsync(Guid id)
        {
            await dbCollection.DeleteOneAsync(track => track.id == id);
        }
    }
}
