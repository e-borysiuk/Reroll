using MvvmCross.Commands;
using Reroll.Mobile.Core.Services;
using Reroll.Models;

namespace Reroll.Mobile.Core.ViewModels.Dialogs
{
    public class WeaponViewModel : BaseViewModel<Weapon>
    {
        public string WeaponName { get; set; }
        public int AttackBonus { get; set; }
        public int DiceCount { get; set; }
        public string DiceType { get; set; }
        public string Critical { get; set; }
        Weapon parameter;

        public WeaponViewModel()
        {

        }

        public override void Prepare(Weapon parameter)
        {
            this.IsEditMode = true;
            this.parameter = parameter;
            this.AttackBonus = parameter.AttackBonus;
            this.Critical = parameter.Critical;
            this.DiceCount = parameter.DiceCount;
            this.DiceType = parameter.DiceType;
            this.WeaponName = parameter.Name;
        }

        public MvxCommand SaveCommand =>
            new MvxCommand(() =>
            {
                if(string.IsNullOrEmpty(WeaponName))
                    NotificationService.ReportError("Name cannot be empty");

                Player updated = this.Player;
                if (IsEditMode)
                    SaveEdit(ref updated);
                else
                    SaveNew(ref updated);

                this._dataRepository.SendUpdate(updated);
            });

        private void SaveEdit(ref Player updated)
        {
            var index = updated.Weapons.FindIndex(x => x == parameter);
            updated.Weapons[index] = new Weapon
            {
                Name = WeaponName,
                DiceCount = DiceCount,
                DiceType = DiceType,
                Critical = Critical,
                AttackBonus = AttackBonus
            };
        }

        private void SaveNew(ref Player updated)
        {
            updated.Weapons.Add(new Weapon
            {
                Name = WeaponName,
                DiceCount = DiceCount,
                DiceType = DiceType,
                Critical = Critical,
                AttackBonus = AttackBonus
            });
        }
    }
}
