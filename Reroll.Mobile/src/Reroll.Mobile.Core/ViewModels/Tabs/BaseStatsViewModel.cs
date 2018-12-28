using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmCross.Base;
using MvvmCross.Commands;
using MvvmCross.Plugin.Messenger;
using Reroll.Mobile.Core.Models.MvxMessages;
using Reroll.Models;
using Reroll.Models.Enums;

namespace Reroll.Mobile.Core.ViewModels.Tabs
{
    public class BaseStatsViewModel : BaseViewModel
    {
        public BaseStatsViewModel(string name = "1") : base(name)
        {
        }

        public ObservableCollection<State> States =>
            new ObservableCollection<State>(this.Player.State);

        public string ExpString => "XP: ";
        public string HealthString => $"HP: {this.Player.CurrentHealthPoints} / {this.Player.HealthPoints}";
        public string CurrentHealthPoints => this.Player.CurrentHealthPoints.ToString();
        public string HealthPoints => this.Player.HealthPoints.ToString();

        public async void UpdateBaseStat(string propertyName, string eText)
        {
            this._signalrService.SendLog($"Changed {propertyName} value to {eText}");
            await Task.Delay(TimeSpan.FromSeconds(1));
            this._dataRepository.SendUpdate(this.Player);
        }
    }
}
