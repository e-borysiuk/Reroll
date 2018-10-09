using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.AspNetCore.SignalR.Client;
using MvvmCross.Commands;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using Reroll.Mobile.Core.Models.MvxMessages;

namespace Reroll.Mobile.Core.ViewModels
{
    public class ChildViewModel : BaseViewModel
    {
        public ChildViewModel(string name = "default")
        {
            this.messageToken = this._messenger.Subscribe<NewMessage>(NewMessageArrived);
            Name = "";
        }

        void NewMessageArrived(NewMessage obj)
        {
            this.Name += obj.User + ": " + obj.Message + "\n";
        }

        public string Message { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }
        private IMvxCommand _goToChildCommand;
        MvxSubscriptionToken messageToken;

        public IMvxCommand GoToChildCommand
        {
            get
            {
                _goToChildCommand = _goToChildCommand ?? new MvxCommand(() =>
                {
                    this._signalrService.SendMessage(this.Message);
                });
                return _goToChildCommand;
            }
        }
    }
}
