import { Component, Input, Output, SimpleChanges, OnChanges } from '@angular/core';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { Player } from '../../models/Player';
import { WeaponModalComponent } from '../modals/weapon-modal/weapon-modal.component';
import { Weapon } from '../../models/Weapon';

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

  public isWeaponsCollapsed = false;
  public isAmmunitionCollapsed = false;
  public isItemsCollapsed = false;

  constructor(private modalService: NgbModal) {
  }

  ngOnChanges(changes: SimpleChanges) {
    this.player = changes['playerData'].currentValue;
  }

  ngOnInit() {
    this.player.currentHealthPoints = 50;
    this.player.healthPoints = 60;
  }

  open() {
    const modalRef = this.modalService.open(WeaponModalComponent);
    modalRef.componentInstance.weapon = this.player.weapons[0];
    modalRef.result.then((result) => {
      let weapon = result as Weapon;
      this.player.weapons[0] = weapon;
    }).catch((error) => {
      console.log(error);
    });
  }
}
