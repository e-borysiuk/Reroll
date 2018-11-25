using MvvmCross.Commands;
using Reroll.Mobile.Core.Services;
using Reroll.Models;

namespace Reroll.Mobile.Core.ViewModels.Dialogs
{
    public class WeaponViewModel : BaseViewModel<Weapon>
    {
        public string WeaponName { get; set; }
        public string Damage { get; set; }
        public string Critical { get; set; }
        Weapon parameter;

        public WeaponViewModel()
        {

        }

        public override void Prepare(Weapon parameter)
        {
            this.IsEditMode = true;
            this.parameter = parameter;
            this.Critical = parameter.Critical;
            this.Damage = parameter.Damage;
            this.WeaponName = parameter.Name;
        }

        public MvxCommand SaveCommand =>
            new MvxCommand(() =>
            {
                if (string.IsNullOrEmpty(WeaponName))
                {
                    NotificationService.ReportError("Name cannot be empty");
                    return;
                }

                Player updated = this.Player;
                if (IsEditMode)
                    SaveEdit(ref updated);
                else
                    SaveNew(ref updated);
                if(IsEditMode)
                    this._signalrService.SendLog($"Edited weapon: {parameter.Name}");
                else
                    this._signalrService.SendLog($"Created weapon: {WeaponName}");
                this._dataRepository.SendUpdate(updated);
            });

        private void SaveEdit(ref Player updated)
        {
            var index = updated.Weapons.FindIndex(x => x == parameter);
            updated.Weapons[index] = new Weapon
            {
                Name = WeaponName,
                Damage = Damage,
                Critical = Critical,
            };
        }

        private void SaveNew(ref Player updated)
        {
            updated.Weapons.Add(new Weapon
            {
                Name = WeaponName,
                Damage = Damage,
                Critical = Critical,
            });
        }

        public MvxCommand DeleteCommand =>
            new MvxCommand(() =>
            {
                var updated = this.Player;
                var index = updated.Weapons.FindIndex(x => x == parameter);
                updated.Weapons.RemoveAt(index);
                this._signalrService.SendLog($"Deleted weapon: {parameter.Name}");
                this._dataRepository.SendUpdate(updated);
            });
    }
}
