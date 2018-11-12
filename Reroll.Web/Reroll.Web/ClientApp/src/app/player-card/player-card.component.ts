import { Component, Input, Output, SimpleChanges, OnChanges } from '@angular/core';
import { Player } from '../../models/Player';

@Component({
  selector: 'player-card',
  templateUrl: './player-card.component.html',
  styleUrls: [ './player-card.component.css' ]
})

export class PlayerCardComponent implements OnChanges {
  @Input() playerData: Player;
  player: Player;
  healthValue: number;
  healthMax: number;

  constructor() {
  }

  ngOnChanges(changes: SimpleChanges) {
    this.player = changes['playerData'].currentValue;
  }

  ngOnInit() {
    this.player.currentHealthPoints = 50;
    this.player.healthPoints = 60;
  }
}
