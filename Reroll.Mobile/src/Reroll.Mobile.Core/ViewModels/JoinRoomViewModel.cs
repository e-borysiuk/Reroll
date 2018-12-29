
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Android.Views;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Reroll.Mobile.Core.Models.Enums;
using Reroll.Mobile.Core.Models.MvxMessages;
using Reroll.Mobile.Core.Services;
using Reroll.Mobile.Core.ViewModels.Dialogs;
using Reroll.Models;
using Reroll.Models.Enums;

namespace Reroll.Mobile.Core.ViewModels
{
    public class JoinRoomViewModel : BaseViewModel
    {
        public string RoomName { get; set; }
        public string Password { get; set; }
        List<string> PlayerNames;

        public JoinRoomViewModel()
        {
            RoomName = "123";
            Password = "123123";
            this._signalrService.StartConnection();
            this._messenger.Subscribe<JoinResponseMessage>( async (message) => JoinRoomResponse(message));
        }

        private async void JoinRoomResponse(JoinResponseMessage obj)
        {
            PlayerNames = obj.PlayerNames;
            switch (obj.Response)
            {
                case ResponseStatusEnum.GroupDoesNotExist:
                    NotificationService.ReportError("Room doesn't exist, create?", "Create", JoinRoomAsync);
                    break;
                case ResponseStatusEnum.GroupExists:
                    JoinRoomAsync(null);
                    break;
                case ResponseStatusEnum.InvalidPassword:
                    NotificationService.ReportError("Invalid password!");
                    break;
            }
        }

        private async void JoinRoomAsync(View obj)
        {
            var playerName = await _navigationService.Navigate<NameViewModel, string>();
            if (string.IsNullOrEmpty(playerName))
                return;
            var playerExists = PlayerNames?.Contains(playerName);
            if (playerExists == false || playerExists == null)
            {
                NotificationService.ReportError("Player doesn't exist, create new", "Create", (view =>
                {
                    this._signalrService.JoinGroup(this.RoomName, this.Password, playerName);
                    this._navigationService.Navigate<MainViewModel>();
                }));
            }
            else
            {
                this._signalrService.JoinGroup(this.RoomName, this.Password, playerName);
                await this._navigationService.Navigate<MainViewModel>();
            }
        }

        public MvxCommand JoinRoomCommand =>
            new MvxCommand(() =>
            {
                if (string.IsNullOrEmpty(this.RoomName))
                    NotificationService.ReportError("Room name is empty");
                if (string.IsNullOrEmpty(this.Password))
                    NotificationService.ReportError("Password is empty");
                if (this.Password.Length < 6)
                    NotificationService.ReportError("Password is too short");

                this._signalrService.CheckGroupExists(this.RoomName, this.Password);

                //this._navigationService.Navigate<MainViewModel>();
            });
    }
}
