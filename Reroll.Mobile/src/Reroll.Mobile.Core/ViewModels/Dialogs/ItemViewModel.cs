using MvvmCross.Commands;
using Reroll.Mobile.Core.Services;
using Reroll.Models;

namespace Reroll.Mobile.Core.ViewModels.Dialogs
{
    public class ItemViewModel : BaseViewModel<InventoryItem>
    {
        InventoryItem parameter;
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public string Note { get; set; }

        public ItemViewModel()
        {

        }

        public override void Prepare(InventoryItem parameter)
        {
            this.parameter = parameter;
            this.IsEditMode = true;
            this.ItemName = parameter.Name;
            this.Quantity = parameter.Quantity;
            this.Note = parameter.Note;
        }

        public MvxCommand SaveCommand =>
            new MvxCommand(() =>
            {
                if (string.IsNullOrEmpty(ItemName))
                {
                    NotificationService.ReportError("Item Name cannot be empty");
                    return;
                }

                Player updated = this.Player;
                if (IsEditMode)
                    SaveEdit(ref updated);
                else
                    SaveNew(ref updated);
                if(IsEditMode)
                    this._signalrService.SendLog($"Edited item: {parameter.Name}");
                else
                    this._signalrService.SendLog($"Created item: {ItemName}");

                this._dataRepository.SendUpdate(updated);
            });

        void SaveNew(ref Player updated)
        {
            updated.InventoryItems.Add(new InventoryItem
            {
                Quantity = Quantity,
                Name = ItemName,
                Note = Note
            });
        }

        void SaveEdit(ref Player updated)
        {
            var index = updated.InventoryItems.FindIndex(x => x == parameter);
            updated.InventoryItems[index] = new InventoryItem
            {
                Quantity = Quantity,
                Name = ItemName,
                Note = Note
            };
        }

        public MvxCommand DeleteCommand =>
            new MvxCommand(() =>
            {
                var updated = this.Player;
                var index = updated.InventoryItems.FindIndex(x => x == parameter);
                updated.InventoryItems.RemoveAt(index);
                this._signalrService.SendLog($"Deleted item: {parameter.Name}");
                this._dataRepository.SendUpdate(updated);
            });
    }
}
