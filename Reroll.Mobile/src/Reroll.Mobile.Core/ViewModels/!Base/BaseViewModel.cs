using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using Reroll.Mobile.Core.Interfaces;
using Reroll.Mobile.Core.Models.MvxMessages;
using Reroll.Models;

namespace Reroll.Mobile.Core.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {
        protected IMvxMessenger _messenger;
        protected IMvxNavigationService _navigationService;
        protected ISignalrService _signalrService;
        protected HubConnection _connection;
        protected IDataRepository _dataRepository;

        protected BaseViewModel()
        {
            _navigationService = Mvx.Resolve<IMvxNavigationService>();
            _signalrService = Mvx.Resolve<ISignalrService>();
            _messenger = Mvx.Resolve<IMvxMessenger>();
            _dataRepository = Mvx.Resolve<IDataRepository>();
            this._refreshToken = this._messenger.Subscribe<RefreshMessage>(RefreshUi);
        }

        public BaseViewModel(string name = "default") : this()
        {
            this.Name = name;
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
