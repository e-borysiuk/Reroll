import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { MessageService } from './MessageService';
import { Player } from '../models/Player';

@Injectable()

export class SignalrService {
  public hubConnection: signalR.HubConnection;

  constructor(private messageService: MessageService) {

  }

  sendUpdateToPlayer(playerName: string, playerData: Player, message: string) {
    this.hubConnection.invoke("updatePlayerModel", playerName, playerData, message);
  }

  getConnection() : signalR.HubConnection {
    if (this.hubConnection === undefined || this.hubConnection === null) {
      this.hubConnection = new signalR.HubConnectionBuilder()
        .withUrl("/rerollHub")
        .build();

      this.hubConnection
        .start()
        .then(() => console.log('Connection started!'))
        .catch(err => console.error(err.toString()));

      this.hubConnection.on("receiveInitialGmData", (data: any) => {
        this.messageService.sendMessage(data);
      });

      return this.hubConnection;
    }
    return this.hubConnection;
  }
}
