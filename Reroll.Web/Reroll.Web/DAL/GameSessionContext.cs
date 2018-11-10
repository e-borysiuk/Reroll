using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Reroll.Models;

namespace Reroll.Web.DAL
{
    public class GameSessionContext : IGameSessionContext
    {
        private readonly IMongoDatabase _db;
        public GameSessionContext(IOptions<Settings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.Database);
        }
        public IMongoCollection<GameSession> GameSessions => _db.GetCollection<GameSession>("GameSessions");
    }
}