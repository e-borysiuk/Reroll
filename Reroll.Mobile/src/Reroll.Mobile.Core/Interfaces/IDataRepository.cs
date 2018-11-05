using Reroll.Models;

namespace Reroll.Mobile.Core.Interfaces
{
    public interface IDataRepository
    {
        PlayerModel PlayerModel { get; set; }
        void SendUpdate(PlayerModel data);
    }
}
