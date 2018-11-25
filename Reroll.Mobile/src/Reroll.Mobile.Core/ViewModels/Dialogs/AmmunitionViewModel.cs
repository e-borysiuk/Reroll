using MvvmCross.Commands;
using Reroll.Mobile.Core.Services;
using Reroll.Models;

namespace Reroll.Mobile.Core.ViewModels.Dialogs
{
    public class AmmunitionViewModel : BaseViewModel<Ammunition>
    {
        Ammunition parameter;
        public string AmmunitionName { get; set; }
        public int Quantity { get; set; }

        public AmmunitionViewModel()
        {

        }

        public override void Prepare(Ammunition parameter)
        {
            this.parameter = parameter;
            this.IsEditMode = true;
            this.AmmunitionName = parameter.Name;
            this.Quantity = parameter.Quantity;
        }

        public void SetQuantityValue(int value)
        {
            Quantity = value;
        }

        public MvxCommand SaveCommand =>
            new MvxCommand(() =>
            {
                if (string.IsNullOrEmpty(AmmunitionName))
                {
                    NotificationService.ReportError("Name cannot be empty");
                    return;
                }

                if (Quantity <= 0)
                {
                    NotificationService.ReportError("Quantity cannot be less than 0");
                    return;
                }

                Player updated = this.Player;
                if (IsEditMode)
                    SaveEdit(ref updated);
                else
                    SaveNew(ref updated);

                if (IsEditMode)
                    this._signalrService.SendLog($"Edited ammunition: {updated.Name}");
                else
                    this._signalrService.SendLog($"Created ammunition: {AmmunitionName}");
                this._dataRepository.SendUpdate(updated);
            });

        void SaveNew(ref Player updated)
        {
            updated.AmmunitionList.Add(new Ammunition()
            {
                Quantity = Quantity,
                Name = AmmunitionName
            });
        }

        void SaveEdit(ref Player updated)
        {
            var index = updated.AmmunitionList.FindIndex(x => x == parameter);
            updated.AmmunitionList[index] = new Ammunition()
            {
                Quantity = Quantity,
                Name = AmmunitionName
            };
        }

        public MvxCommand DeleteCommand =>
            new MvxCommand(() =>
            {
                var updated = this.Player;
                var index = updated.AmmunitionList.FindIndex(x => x == parameter);
                updated.AmmunitionList.RemoveAt(index);
                this._signalrService.SendLog($"Deleted ammunition: {parameter.Name}");
                this._dataRepository.SendUpdate(updated);
            });
    }
}
