import { Component } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { ResponseStatusEnum } from '../../models/ResponseStatusEnum';

@Component({
  selector: 'counter',
  templateUrl: './counter.component.html'
})

export class CounterComponent {
  public hubConnection: signalR.HubConnection;

  adressed = 'HughJass';
  playerName = 'HughJass';
  roomName = 'room1';
  password = '';
  nick = 'John';
  message = '';
  messages: string[] = [];
  groupExists = false;

  constructor() {
    //this.hubConnection = new signalR.HubConnectionBuilder()
    //  .withUrl("https://192.168.1.8:50793/rerollHub")
    //  .configureLogging(signalR.LogLevel.Trace)
    //  .build();
  }

  ngOnInit() {
    //this.hubConnection = new signalR.HubConnectionBuilder()
    //  .withUrl("/rerollHub")
    //  .build();

    //this.hubConnection.on('sendToAll', (nick: string, receivedMessage: string) => {
    //  const text = `${nick}: ${receivedMessage}`;
    //  this.messages.push(text);
    //});

    //this.hubConnection.on('sendToPlayer', (nick: string, receivedMessage: string) => {
    //  const text = `${nick}: ${receivedMessage}`;
    //  this.messages.push(text);
    //});

    //this.hubConnection.on('groupExistsResponse', (receivedMessage: ResponseStatusEnum) => {
    //  switch (receivedMessage) {
    //  case ResponseStatusEnum.groupExists:
    //    this.joinGroup(this.roomName);
    //    break;
    //  case ResponseStatusEnum.groupDoesNotExist:
    //    var confirmation = window.confirm('There is no room with such name, create new?');
    //    if (confirmation) {
    //      this.joinGroup(this.roomName);
    //    }
    //    break;
    //  case ResponseStatusEnum.invalidPassword:
    //      window.alert("Invalid password");
    //    break;
    //    default:
    //      window.alert('Unknown error');
    //  }
    //});


    //this.hubConnection
    //  .start()
    //  .then(() => console.log('Connection started!'))
    //  .catch(err => console.error(err.toString()));
  }
  public createRoom(): void {
    this.checkGroupExists(this.roomName);
  }

  private checkGroupExists(groupName: string) {
    this.hubConnection
      .invoke('groupExists', groupName, this.password)
      .catch(err => console.error(err));
  }

  private joinGroup(groupName: string) {
    var playerName = prompt("Input your player name");
    if (playerName == null || playerName == "") {
      window.alert("You didn't put your player name!");
    } else {
      this.hubConnection
        .invoke('joinGroup', this.roomName, playerName)
        .catch(err => console.error(err));
    }
  }




  public sendMessage(): void {
    this.hubConnection
      .invoke('sendToAll', this.nick, this.message)
      .catch(err => console.error(err));
  }
}
