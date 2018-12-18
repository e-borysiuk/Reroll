using System.Collections.ObjectModel;
using MvvmCross.ViewModels;
using Reroll.Mobile.Core.Models;
using Reroll.Models;

namespace Reroll.Mobile.Core.Interfaces
{
    public interface IDataRepository
    {
        MvxObservableCollection<Roll> DiceRolls { get; set; }
        Player Player { get; set; }
        void SendUpdate(Player data);
    }
}
