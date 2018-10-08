import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';

@Injectable()

export class SignalrService {
  public hubConnection: signalR.HubConnection;

  constructor() {
  }

  getConnection() : signalR.HubConnection {
    if (this.hubConnection === null) {
      this.hubConnection = new signalR.HubConnectionBuilder()
        .withUrl("/rerollHub")
        .build();

      this.hubConnection
        .start()
        .then(() => console.log('Connection started!'))
        .catch(err => console.error(err.toString()));

      return this.hubConnection;
    }
    return this.hubConnection;
  }
}
