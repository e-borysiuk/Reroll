using MvvmCross.Commands;
using Reroll.Mobile.Core.Services;
using Reroll.Models;

namespace Reroll.Mobile.Core.ViewModels.Dialogs
{
    public class WeaponViewModel : ChildViewModel
    {
        public string WeaponName { get; set; }
        public int AttackBonus { get; set; }
        public int DiceCount { get; set; }
        public string DiceType { get; set; }
        public string Critical { get; set; }


        public WeaponViewModel()
        {

        }

        public MvxCommand SaveCommand =>
            new MvxCommand(() =>
            {
                if(string.IsNullOrEmpty(Name))
                    NotificationService.ReportError("Name cannot be empty");

                Player updated = this.Player;
                updated.Weapons.Add(new Weapon
                {
                    Name = WeaponName,
                    DiceCount = DiceCount,
                    DiceType = DiceType,
                    Critical = Critical,
                    AttackBonus = AttackBonus
                });
                this._dataRepository.SendUpdate(updated);
            });
    }
}
