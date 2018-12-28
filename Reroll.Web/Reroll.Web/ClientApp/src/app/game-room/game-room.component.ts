import { Component } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { SignalrService } from '../../services/SignalrService';
import { Player } from "../../models/Player";
import { MessageService } from '../../services/MessageService';
import { Subscription } from 'rxjs/Subscription';
import { ActivityMessage } from '../../models/ActivityMessage';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'game-room',
  templateUrl: './game-room.component.html',
  styleUrls: ['./game-room.component.css']
})

export class GameRoomComponent {
  public hubConnection: signalR.HubConnection;

  subscription: Subscription;
  playersData: any[];
  activityMessages: ActivityMessage[];
  diceRolls: string[];

  constructor(private signalrService: SignalrService, private messageService: MessageService, private toastr: ToastrService) {
    this.hubConnection = signalrService.getConnection();
    this.hubConnection.invoke('getInitialGmData')
      .catch(err => console.error(err));
    this.hubConnection.invoke('getInitialLogs')
      .catch(err => console.error(err));
    this.subscription = this.messageService.getMessage().subscribe(message => {
      this.playersData = [];
      for (let obj of message.text) {
        let player = obj as Player;
        this.playersData.push(player);
      }
      let player = this.playersData[0];
    });
    this.diceRolls = [];
  }

  ngOnInit() {
    this.hubConnection.on('sendToAll', (nick: string, receivedMessage: string) => {

    });
    this.hubConnection.on('sendToPlayer', (nick: string, receivedMessage: string) => {

    });
    this.hubConnection.on('receiveInitialLogs', (logs: ActivityMessage[]) => {
      this.activityMessages = logs;
    });
    this.hubConnection.on('receiveActivityLog', (message: ActivityMessage) => {
      this.activityMessages.unshift(message);
      this.toastr.info(message.message, 'Player update:', { timeOut: 2000});
    });
    this.hubConnection.on('receiveDiceRoll', (message: string) => {
      this.diceRolls.unshift(message);
    });
    this.hubConnection.on('sendUpdateToGM', (name: string, value: Player) => {
      let updatePlayer = this.playersData.find(p => p.name === value.name);
      let index = this.playersData.indexOf(updatePlayer);
      this.playersData[index] = value;
      let player = this.playersData[0];
    });
  }

  public rollDice(diceType: number) {
    let rollValue = Math.floor(Math.random() * diceType) + 1;
    this.hubConnection
      .invoke('sendDiceRoll', rollValue, `D${diceType}`)
      .catch(err => console.error(err));
  }

  public sendMessage(playerData): void {
    this.hubConnection
      .invoke('updatePlayerModel', playerData.name, playerData)
      .catch(err => console.error(err));
  }
} 
