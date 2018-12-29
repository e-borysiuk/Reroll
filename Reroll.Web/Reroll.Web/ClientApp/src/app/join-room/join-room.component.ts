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
  groupExists = false;

  constructor(private signalrService: SignalrService, private router: Router) {
    this.hubConnection = signalrService.getConnection();
    this.model.roomName = 123;
    this.model.password = 123123;
  }

  ngOnInit() {
    this.hubConnection.on('groupExistsResponse', (receivedMessage: ResponseStatusEnum, playerNames: string[]) => {
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
    this.hubConnection
        .invoke('joinGroup', this.model.roomName, 'GameMaster', this.model.password, true)
        .catch(err => console.error(err));
  }
}
