using System.Threading.Tasks;
using Reroll.Models;

namespace Reroll.Mobile.Core.Interfaces
{
    public interface ISignalrService
    {
        Task StartConnection();
        void CheckGroupExists(string roomName, string roomPassword);
        void SendMessage(string message);
        void SendUpdate(PlayerModel data);
        void JoinGroup(string roomName, string roomPassword);
    }
}
