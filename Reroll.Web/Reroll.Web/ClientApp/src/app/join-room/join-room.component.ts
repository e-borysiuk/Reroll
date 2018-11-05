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
  model: any = {};
  adressed = 'HughJass';
  playerName = 'HughJass';
  nick = 'John';
  message = '';
  messages: string[] = [];
  groupExists = false;

  constructor(private signalrService: SignalrService, private router: Router) {
    this.hubConnection = signalrService.getConnection();
    this.model.roomName = 123;
    this.model.password = 123123;
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
            this.joinGroup(this.model.roomName);
          this.router.navigate(['/game-room', this.model.roomName]);
          break;
        case ResponseStatusEnum.groupDoesNotExist:
          var confirmation = window.confirm('There is no room with such name, create new?');
          if (confirmation) {
            this.joinGroup(this.model.roomName);
            this.router.navigate(['/game-room', this.model.roomName]);
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

  private createRoom() {
    this.hubConnection
      .invoke('groupExists', this.model.roomName, this.model.password)
      .catch(err => console.error(err));
  }

  private joinGroup(groupName: string) {
    var playerName = prompt("Input your player name");
    if (playerName == null || playerName == "") {
      window.alert("You didn't put your player name!");
    } else {
      this.hubConnection
        .invoke('joinGroup', this.model.roomName, playerName, this.model.password, true)
        .catch(err => console.error(err));
    }
  }
}
