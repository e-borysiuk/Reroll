using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.AspNetCore.SignalR.Client;
using MvvmCross.Commands;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using Reroll.Mobile.Core.Models.MvxMessages;
using Reroll.Models;
using Reroll.Models.Enums;

namespace Reroll.Mobile.Core.ViewModels
{
    public class ChildViewModel : BaseViewModel
    {
        public ChildViewModel(string name = "default")
        {
            this.Name = name;
            this._refreshToken = this._messenger.Subscribe<RefreshMessage>(RefreshUi);
        }

        private string _name;
        private MvxSubscriptionToken _refreshToken;

        public Player Player => this._dataRepository.Player;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        protected void RefreshUi(RefreshMessage obj)
        {
            RaiseAllPropertiesChanged();
        }
    }
}
