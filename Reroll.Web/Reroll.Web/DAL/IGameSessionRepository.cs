using System.Collections.Generic;
using System.Threading.Tasks;
using Reroll.Models;

namespace Reroll.Web.DAL
{
    public interface IGameSessionRepository
    {
        Task<IEnumerable<GameSession>> GetAllGameSessions();
        Task<GameSession> GetGameSession(string groupName);
        Task Create(GameSession gameSession);
        Task<bool> Update(GameSession gameSession);
        Task<bool> AddOrUpdate(GameSession gameSession);
        Task<bool> Delete(string groupName);
    }
}