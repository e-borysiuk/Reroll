using System.Threading.Tasks;
using Reroll.Models;

namespace Reroll.Mobile.Core.Interfaces
{
    public interface ISignalrService
    {
        Task StartConnection();
        void CheckGroupExists(string roomName, string roomPassword);
        void SendMessage(string message);
        void SendUpdate(Player data);
        void JoinGroup(string roomName, string roomPassword, string playerName);
        void SendLog(string message);
        void SendDiceRoll(int value, string diceType);
    }
}
