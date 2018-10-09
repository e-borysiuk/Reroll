using System.Threading.Tasks;

namespace Reroll.Mobile.Core.Services.Interfaces
{
    public interface ISignalrService
    {
        Task StartConnection();
        void CheckGroupExists(string roomName, string roomPassword);
        void SendMessage(string message);
    }
}
