using MvvmCross.Commands;
using Reroll.Mobile.Core.Services;
using Reroll.Models;

namespace Reroll.Mobile.Core.ViewModels.Dialogs
{
    public class ItemViewModel : ChildViewModel
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public string Note { get; set; }

        public ItemViewModel()
        {

        }

        public MvxCommand SaveCommand =>
            new MvxCommand(() =>
            {
                if(string.IsNullOrEmpty(Name))
                    NotificationService.ReportError("Item Name cannot be empty");

                Player updated = this.Player;
                updated.InventoryItems.Add(new InventoryItem
                {
                    Quantity = Quantity,
                    Name = ItemName,
                    Note = Note
                });
                this._dataRepository.SendUpdate(updated);
            });
    }
}
