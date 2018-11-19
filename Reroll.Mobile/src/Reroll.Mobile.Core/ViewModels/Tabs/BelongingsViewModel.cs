using System.Collections.ObjectModel;
using MvvmCross.Commands;
using Reroll.Models;

namespace Reroll.Mobile.Core.ViewModels.Tabs
{
    public class BelongingsViewModel : ChildViewModel
    {
        public BelongingsViewModel(string name = "2") : base(name)
        {
        }

        public ObservableCollection<Weapon> Weapons =>
           new ObservableCollection<Weapon>(this.Player.Weapons);
        public ObservableCollection<InventoryItem> Items =>
            new ObservableCollection<InventoryItem>(this.Player.InventoryItems);
        public ObservableCollection<Ammunition> Ammunition =>
            new ObservableCollection<Ammunition>(this.Player.AmmunitionList);

        public MvxCommand AddWeaponCommand =>
            new MvxCommand(() =>
            {
                this._navigationService.Navigate<Dialogs.WeaponViewModel>();
            });
        public MvxCommand AddAmmunitionCommand =>
            new MvxCommand(() =>
            {
                this._navigationService.Navigate<Dialogs.AmmunitionViewModel>();
            });
        public MvxCommand AddItemCommand =>
            new MvxCommand(() =>
            {
                this._navigationService.Navigate<Dialogs.ItemViewModel>();
            });
    }
}
