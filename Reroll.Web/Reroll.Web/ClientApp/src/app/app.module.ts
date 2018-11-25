import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { SignalrService } from "../services/SignalrService";
import { MessageService } from "../services/MessageService";
import { TabService } from '../services/TabService';
import { ResourcesService } from '../services/ResourcesService';

import { AppComponent } from './app.component';
import { JoinRoomComponent } from './join-room/join-room.component';
import { GameRoomComponent } from './game-room/game-room.component';
import { PlayerCardComponent } from './player-card/player-card.component';

import { WeaponModalComponent } from './modals/weapon-modal/weapon-modal.component';
import { StateModalComponent } from './modals/state-modal/state-modal.component';
import { ItemModalComponent } from './modals/item-modal/item-modal.component';
import { AmmunitionModalComponent } from './modals/ammunition-modal/ammunition-modal.component';


@NgModule({
  declarations: [
    AppComponent,
    JoinRoomComponent,
    GameRoomComponent,
    PlayerCardComponent,
    WeaponModalComponent,
    StateModalComponent,
    ItemModalComponent,
    AmmunitionModalComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: JoinRoomComponent, pathMatch: 'full' },
      { path: 'join-room', component: JoinRoomComponent },
      { path: 'game-room/:id', component: GameRoomComponent }
    ])
  ],
  providers: [SignalrService, MessageService, TabService, ResourcesService],
  bootstrap: [AppComponent],
  entryComponents: [WeaponModalComponent, StateModalComponent, ItemModalComponent, AmmunitionModalComponent]
})
export class AppModule { }
