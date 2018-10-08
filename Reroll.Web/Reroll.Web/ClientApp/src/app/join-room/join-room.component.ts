import { Component } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { ResponseStatusEnum } from '../../models/ResponseStatusEnum';
import { SignalrService } from '../../services/SignalrService';
import { Router } from "@angular/router";

@Component({
  selector: 'join-room',
  templateUrl: './join-room.component.html'
})

export class JoinRoomComponent {
  public hubConnection: signalR.HubConnection;

  adressed = 'HughJass';
  playerName = 'HughJass';
  roomName = 'room1';
  password = '';
  nick = 'John';
  message = '';
  messages: string[] = [];
  groupExists = false;

  constructor(private signalrService: SignalrService, private router: Router) {
    this.hubConnection = signalrService.getConnection();
  }

  ngOnInit() {

    this.hubConnection.on('sendToAll', (nick: string, receivedMessage: string) => {
      const text = `${nick}: ${receivedMessage}`;
      this.messages.push(text);
    });

    this.hubConnection.on('sendToPlayer', (nick: string, receivedMessage: string) => {
      const text = `${nick}: ${receivedMessage}`;
      this.messages.push(text);
    });

    this.hubConnection.on('groupExistsResponse', (receivedMessage: ResponseStatusEnum) => {
      switch (receivedMessage) {
      case ResponseStatusEnum.groupExists:
          this.joinGroup(this.roomName);
          this.router.navigate(['/game-room']);
        break;
      case ResponseStatusEnum.groupDoesNotExist:
        var confirmation = window.confirm('There is no room with such name, create new?');
        if (confirmation) {
          this.joinGroup(this.roomName);
          this.router.navigate(['/game-room']);
        }
        break;
      case ResponseStatusEnum.invalidPassword:
          window.alert("Invalid password");
        break;
        default:
          window.alert('Unknown error');
      }
    });
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
