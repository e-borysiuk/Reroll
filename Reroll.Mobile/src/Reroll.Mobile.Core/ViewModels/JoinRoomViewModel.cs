
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
        public string Passowrd { get; set; }

        public JoinRoomViewModel()
        {
            this._signalrService.StartConnection();
            this._messenger.Subscribe<JoinResponseMessage>(JoinRoomResponse);
        }

        void JoinRoomResponse(JoinResponseMessage obj)
        {
            switch (obj.Response)
            {
                case ResponseStatusEnum.GroupDoesNotExist:
                    break;
                case ResponseStatusEnum.GroupExists:
                    break;
                case ResponseStatusEnum.InvalidPassword:
                    ErrorReportingService.ReportError("Invalid password!");
                    break;
            }
            //this._navigationService.Navigate<MainViewModel>();
        }

        public MvxCommand JoinRoomCommand =>
            new MvxCommand(() =>
            {
                this._signalrService.CheckGroupExists(this.RoomName, this.Passowrd);
            });
    }
}

//export class JoinRoomComponent
//{
//    public hubConnection: signalR.HubConnection;

//  adressed = 'HughJass';
//  playerName = 'HughJass';
//  roomName = 'room1';
//  password = '';
//  nick = 'John';
//  message = '';
//  messages: string[] = [];
//  groupExists = false;

//  constructor(private signalrService: SignalrService, private router: Router) {
//    this.hubConnection = signalrService.getConnection();
//  }

//ngOnInit()
//{

//    this.hubConnection.on('sendToAll', (nick: string, receivedMessage: string) => {
//        const text = `${ nick}: ${ receivedMessage}`;
//        this.messages.push(text);
//    });

//    this.hubConnection.on('sendToPlayer', (nick: string, receivedMessage: string) => {
//        const text = `${ nick}: ${ receivedMessage}`;
//        this.messages.push(text);
//    });

//    this.hubConnection.on('groupExistsResponse', (receivedMessage: ResponseStatusEnum) => {
//        switch (receivedMessage)
//        {
//            case ResponseStatusEnum.groupExists:
//                this.joinGroup(this.roomName);
//                this.router.navigate(['/game-room']);
//                break;
//            case ResponseStatusEnum.groupDoesNotExist:
//                var confirmation = window.confirm('There is no room with such name, create new?');
//                if (confirmation)
//                {
//                    this.joinGroup(this.roomName);
//                    this.router.navigate(['/game-room']);
//                }
//                break;
//            case ResponseStatusEnum.invalidPassword:
//                window.alert("Invalid password");
//                break;
//            default:
//                window.alert('Unknown error');
//        }
//    });
//}

//public createRoom() : void {
//    this.checkGroupExists(this.roomName);
//}

//private checkGroupExists(groupName: string)
//{
//    this.hubConnection
//      .invoke('groupExists', groupName, this.password)
//      .catch (err => console.error(err));
//    }

//    private joinGroup(groupName: string) {
//        var playerName = prompt("Input your player name");
//        if (playerName == null || playerName == "")
//        {
//            window.alert("You didn't put your player name!");
//        }
//        else
//        {
//            this.hubConnection
//              .invoke('joinGroup', this.roomName, playerName)
//              .catch (err => console.error(err));
//            }
//        }

//        public sendMessage(): void {
//            this.hubConnection
//              .invoke('sendToAll', this.nick, this.message)
//              .catch (err => console.error(err));
//            }
//        }
