import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { SignalrService } from "../services/SignalrService";
import { MessageService } from "../services/MessageService";

import { AppComponent } from './app.component';
import { JoinRoomComponent } from './join-room/join-room.component';
import { GameRoomComponent } from './game-room/game-room.component';
import { PlayerCardComponent } from './player-card/player-card.component';

@NgModule({
  declarations: [
    AppComponent,
    JoinRoomComponent,
    GameRoomComponent,
    PlayerCardComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: JoinRoomComponent, pathMatch: 'full' },
      { path: 'join-room', component: JoinRoomComponent },
      { path: 'game-room/:id', component: GameRoomComponent }
    ])
  ],
  providers: [SignalrService, MessageService],
  bootstrap: [AppComponent]
})
export class AppModule { }
