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

  }

  ngOnInit() {

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
