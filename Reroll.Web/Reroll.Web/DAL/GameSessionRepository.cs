using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Reroll.Models;

namespace Reroll.Web.DAL
{
    public class GameSessionRepository : IGameSessionRepository
    {
        private readonly IGameSessionContext _context;
        public GameSessionRepository(IGameSessionContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GameSession>> GetAllGameSessions()
        {
            return await _context
                .GameSessions
                .Find(_ => true)
                .ToListAsync();
        }

        public Task<GameSession> GetGameSession(string groupName)
        {
            FilterDefinition<GameSession> filter = Builders<GameSession>.Filter.Eq(m => m.GroupName, groupName);
            return _context
                .GameSessions
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        public async Task Create(GameSession game)
        {
            await _context.GameSessions.InsertOneAsync(game);
        }

        public async Task<bool> AddOrUpdate(GameSession game)
        {
            UpdateResult updateResult =
                await _context
                    .GameSessions
                    .UpdateOneAsync(
                        options: new UpdateOptions { IsUpsert = true }, 
                        filter: g => g.Id == game.Id, 
                        update: game.ToBsonDocument());
            return updateResult.IsAcknowledged
                   && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Update(GameSession game)
        {
            ReplaceOneResult updateResult =
                await _context
                    .GameSessions
                    .ReplaceOneAsync(
                        filter: g => g.Id == game.Id,
                        replacement: game,
                        options: new UpdateOptions { IsUpsert = true });
            return updateResult.IsAcknowledged
                   && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string groupName)
        {
            FilterDefinition<GameSession> filter = Builders<GameSession>.Filter.Eq(m => m.GroupName, groupName);
            DeleteResult deleteResult = await _context
                .GameSessions
                .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                   && deleteResult.DeletedCount > 0;
        }
    }
}