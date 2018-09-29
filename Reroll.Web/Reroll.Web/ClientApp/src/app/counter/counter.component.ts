import { Component } from '@angular/core';
import * as signalR from '@aspnet/signalr';

@Component({
  selector: 'counter',
  templateUrl: './counter.component.html'
})
export class CounterComponent {
  public hubConnection: signalR.HubConnection;

  adressed = 'HughJass';
  playerName = 'HughJass';
  roomName = 'room1';
  nick = 'John';
  message = '';
  messages: string[] = [];

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl("https://192.168.1.8:50793/rerollHub")
      .configureLogging(signalR.LogLevel.Trace)
      .build();
  }

  ngOnInit() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl("/rerollHub")
      .build();

    this.hubConnection.on('sendToAll', (nick: string, receivedMessage: string) => {
      const text = `${nick}: ${receivedMessage}`;
      this.messages.push(text);
    });

    this.hubConnection.on('sendToPlayer', (nick: string, receivedMessage: string) => {
      const text = `${nick}: ${receivedMessage}`;
      this.messages.push(text);
    });

    this.hubConnection
      .start()
      .then(() => console.log('Connection started!'))
      .catch(err => console.error(err.toString()));
  }

  dataMessage = {
    healthPoints: 100,
    ammunition: 20,
    spellsPerDay: 10,
    spellsUsed: 5
  };

  public sendData(): void {
    this.hubConnection
      .invoke('sendToPlayer', this.adressed, this.message)
      .catch(err => console.error(err));
  }

  public sendMessage(): void {
    this.hubConnection
      .invoke('sendToAll', this.nick, this.message)
      .catch(err => console.error(err));
  }

  public createRoom(): void {
    this.hubConnection
      .invoke('joinGroup', this.roomName, this.playerName)
      .catch(err => console.error(err));
  }

  public confirm(): void {
    window.confirm('There is no room with such name, create new?');
  }

  public currentCount = 0;

  public incrementCounter() {
    this.currentCount++;
  }
}
