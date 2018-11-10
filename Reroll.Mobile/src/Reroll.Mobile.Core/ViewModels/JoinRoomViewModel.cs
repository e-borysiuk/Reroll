
using Android.Views;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Reroll.Mobile.Core.Models.Enums;
using Reroll.Mobile.Core.Models.MvxMessages;
using Reroll.Mobile.Core.Services;
using Reroll.Models;

namespace Reroll.Mobile.Core.ViewModels
{
    public class JoinRoomViewModel : BaseViewModel
    {
        public string RoomName { get; set; }
        public string Password { get; set; }

        public JoinRoomViewModel()
        {
            RoomName = "123";
            Password = "123123";
            this._signalrService.StartConnection();
            this._messenger.Subscribe<JoinResponseMessage>(JoinRoomResponse);
        }

        void JoinRoomResponse(JoinResponseMessage obj)
        {
            switch (obj.Response)
            {
                case ResponseStatusEnum.GroupDoesNotExist:
                    ErrorReportingService.ReportError("Room doesn't exist, create?", "Create", JoinRoom);
                    break;
                case ResponseStatusEnum.GroupExists:
                    JoinRoom(null);
                    break;
                case ResponseStatusEnum.InvalidPassword:
                    ErrorReportingService.ReportError("Invalid password!");
                    break;
            }
        }

        void JoinRoom(View obj)
        {
            this._signalrService.JoinGroup(this.RoomName, this.Password);
            this._navigationService.Navigate<MainViewModel>();
        }

        public MvxCommand JoinRoomCommand =>
            new MvxCommand(() =>
            {
                if (string.IsNullOrEmpty(this.RoomName))
                    ErrorReportingService.ReportError("Room name is empty");
                if (string.IsNullOrEmpty(this.Password))
                    ErrorReportingService.ReportError("Password is empty");
                if (this.Password.Length < 6)
                    ErrorReportingService.ReportError("Password is too short");

                this._signalrService.CheckGroupExists(this.RoomName, this.Password);

                //this._navigationService.Navigate<MainViewModel>();
            });
    }
}
