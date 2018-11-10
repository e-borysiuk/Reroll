using MongoDB.Driver;
using Reroll.Models;

namespace Reroll.Web.DAL
{
    public interface IGameSessionContext
    {
        IMongoCollection<GameSession> GameSessions { get; }
    }
}