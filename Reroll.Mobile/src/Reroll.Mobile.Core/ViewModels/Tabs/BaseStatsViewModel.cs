using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public string HealthString => $"HP: {this.Player.CurrentHealthPoints} / {this.Player.HealthPoints}";
        public string CurrentHealthPoints => this.Player.CurrentHealthPoints.ToString();
        public string HealthPoints => this.Player.HealthPoints.ToString();


        private IMvxCommand _goToChildCommand;
        private IMvxCommand _incrementValueCommand;
        private IMvxCommand _decrementValueCommand;

        public IMvxCommand GoToChildCommand
        {
            get
            {
                _goToChildCommand = _goToChildCommand ?? new MvxCommand(() =>
                {
                    
                });
                return _goToChildCommand;
            }
        }

        public IMvxCommand IncrementValueCommand
        {
            get
            {
                _incrementValueCommand = _incrementValueCommand ?? new MvxCommand(() =>
                {
                    Player newValue = Player;
                    newValue.Charisma++;
                    this._dataRepository.SendUpdate(newValue);
                });
                return _incrementValueCommand;
            }
        }

        public IMvxCommand DecrementValueCommand
        {
            get
            {
                _decrementValueCommand = _decrementValueCommand ?? new MvxCommand(() =>
                {
                    Player newValue = Player;
                    newValue.Charisma--;
                    this._dataRepository.SendUpdate(newValue);
                });
                return _decrementValueCommand;
            }
        }

        
    }
}
