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


        public MvxCommand SaveCommand =>
            new MvxCommand(() =>
            {
                if(string.IsNullOrEmpty(AmmunitionName))
                    NotificationService.ReportError("Name cannot be empty");
                if (Quantity <= 0)
                    NotificationService.ReportError("Quantity cannot be less than 0");

                Player updated = this.Player;
                if (IsEditMode)
                    SaveEdit(ref updated);
                else
                    SaveNew(ref updated);

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
    }
}
