using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmCross.Commands;
using Reroll.Models;

namespace Reroll.Mobile.Core.ViewModels.Tabs
{
    public class BelongingsViewModel : BaseViewModel
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
        
        public MvxCommand<Ammunition> DecreaseAmmunitionCommand =>
            new MvxCommand<Ammunition>((a) =>
            {
                if (a.Quantity > 0)
                {
                    Player updated = this.Player;
                    var index = updated.AmmunitionList.FindIndex(x => x == a);
                    updated.AmmunitionList[index] = new Ammunition()
                    {
                        Quantity = --a.Quantity,
                        Name = a.Name
                    };
                    this._signalrService.SendLog($"Edited decreased ammunition count: {a.Name}");
                    this._dataRepository.SendUpdate(updated);
                }
            });

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
        public MvxCommand<Weapon> EditWeaponCommand =>
            new MvxCommand<Weapon>((w) =>
            {
                this._navigationService.Navigate<Dialogs.WeaponViewModel, Weapon>(w);
            });
        public MvxCommand<Ammunition> EditAmmunitionCommand =>
            new MvxCommand<Ammunition>((a) =>
            {
                this._navigationService.Navigate<Dialogs.AmmunitionViewModel, Ammunition>(a);
            });
        public MvxCommand<InventoryItem> EditItemCommand =>
            new MvxCommand<InventoryItem>((i) =>
            {
                this._navigationService.Navigate<Dialogs.ItemViewModel, InventoryItem>(i);
            });

        public async void UpdateBaseStat(string propertyName, string eText)
        {
            this._signalrService.SendLog($"Changed {propertyName} value to {eText}");
            this._dataRepository.SendUpdate(this.Player);
        }
    }
}
