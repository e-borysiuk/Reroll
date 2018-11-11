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
  value: number;

  constructor() {
  }

  ngOnChanges(changes: SimpleChanges) {
    this.player = changes['playerData'].currentValue;
    this.value++;
  }

  ngOnInit() {
    this.value = 10;

  }
}
