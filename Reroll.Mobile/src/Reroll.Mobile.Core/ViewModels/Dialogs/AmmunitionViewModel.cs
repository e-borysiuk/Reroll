using MvvmCross.Commands;
using Reroll.Mobile.Core.Services;
using Reroll.Models;

namespace Reroll.Mobile.Core.ViewModels.Dialogs
{
    public class AmmunitionViewModel : ChildViewModel
    {
        public string AmmunitionName { get; set; }
        public int Quantity { get; set; }

        public AmmunitionViewModel()
        {

        }

        public MvxCommand SaveCommand =>
            new MvxCommand(() =>
            {
                if(string.IsNullOrEmpty(Name))
                    NotificationService.ReportError("Name cannot be empty");
                if (Quantity <= 0)
                    NotificationService.ReportError("Quantity cannot be less than 0");

                Player updated = this.Player;
                updated.AmmunitionList.Add(new Ammunition()
                {
                    Quantity = Quantity,
                    Name = AmmunitionName
                });
                this._dataRepository.SendUpdate(updated);
            });
    }
}
