import { Component } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { SignalrService } from '../../services/SignalrService';
import { Player } from "../../models/Player";
import { MessageService } from '../../services/MessageService';
import { Subscription } from 'rxjs/Subscription';

@Component({
  selector: 'game-room',
  templateUrl: './game-room.component.html'
})

export class GameRoomComponent {
  public hubConnection: signalR.HubConnection;

  subscription: Subscription;
  initialData: Player[];
  messages: string[] = [];
  constValue: number = 6;
  playerModel: Player;

  constructor(private signalrService: SignalrService, private messageService: MessageService) {
    this.hubConnection = signalrService.getConnection();
    this.hubConnection.invoke('getInitialGmData')
      .catch(err => console.error(err));
    this.subscription = this.messageService.getMessage().subscribe(message => {
       this.initialData = message.text as Player[];
    });
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

    this.hubConnection.on('sendUpdateToGM', (name: string, value: Player) => {
      this.playerModel = value;
      this.constValue = value.charisma;
    });
  }

  public sendMessage(): void {
    this.hubConnection
      .invoke('updatePlayerModel', this.playerModel.name, this.playerModel)
      .catch(err => console.error(err));
  }
} 
