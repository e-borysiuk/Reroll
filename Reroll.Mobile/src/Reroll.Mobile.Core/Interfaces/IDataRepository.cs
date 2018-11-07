using Reroll.Models;

namespace Reroll.Mobile.Core.Interfaces
{
    public interface IDataRepository
    {
        Player Player { get; set; }
        void SendUpdate(Player data);
    }
}
