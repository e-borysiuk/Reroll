using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using Reroll.Mobile.Core.Services;
using Reroll.Mobile.Core.Services.Interfaces;

namespace Reroll.Mobile.Core.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {
        protected IMvxMessenger _messenger;
        protected IMvxNavigationService _navigationService;
        protected ISignalrService _signalrService;
        protected HubConnection _connection;

        protected BaseViewModel()
        {
            _navigationService = Mvx.Resolve<IMvxNavigationService>();
            _signalrService = Mvx.Resolve<ISignalrService>();
            _messenger = Mvx.Resolve<IMvxMessenger>();
        }
    }
}
