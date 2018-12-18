import { Component, Input, Output, SimpleChanges, OnChanges } from '@angular/core';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { Player } from '../../models/Player';
import { WeaponModalComponent } from '../modals/weapon-modal/weapon-modal.component';
import { Weapon } from '../../models/Weapon';
import { StateModalComponent } from '../modals/state-modal/state-modal.component';
import { State } from '../../models/State';
import { DiceTypeEnum } from '../../models/DiceTypeEnum';
import { KeyAbilityEnum } from '../../models/KeyAbilityEnum';
import { ItemModalComponent } from '../modals/item-modal/item-modal.component';
import { InventoryItem } from '../../models/InventoryItem';
import { AmmunitionModalComponent } from '../modals/ammunition-modal/ammunition-modal.component';
import { Ammunition } from '../../models/Ammunition';
import { NgbTabChangeEvent } from '@ng-bootstrap/ng-bootstrap';
import { ElementRef, Renderer2 } from '@angular/core';
import { TabService } from '../../services/TabService';
import { SignalrService } from '../../services/SignalrService';
import { HealthModalComponent } from '../modals/health-modal/health-modal.component';
import { ExperienceModalComponent } from '../modals/experience-modal/experience-modal.component';

@Component({
  selector: 'player-card',
  templateUrl: './player-card.component.html',
  styleUrls: ['./player-card.component.css']
})

export class PlayerCardComponent implements OnChanges {
  @Input() playerData: Player;
  player: Player;
  healthValue: number;
  healthMax: number;
  currentTab: string = 'belongingsCard';

  public isWeaponsCollapsed = false;
  public isAmmunitionCollapsed = false;
  public isItemsCollapsed = false;

  constructor(private signalrService: SignalrService, private modalService: NgbModal, private tabService: TabService) {
  }

  ngOnChanges(changes: SimpleChanges) {
    this.player = changes['playerData'].currentValue;
  }

  ngOnInit() {
    this.currentTab = this.tabService.getCurrentCard(this.playerData.name);
  }

  public tabChange($event: NgbTabChangeEvent) {
    this.tabService.setCurrentCard(this.playerData.name, $event.nextId);
  }

  getIdAttributeValue(event): string {
    var target = event.currentTarget;
    var idAttr = target.attributes.id;
    if (idAttr != null) {
      return idAttr.nodeValue;
    } else {
      return '';
    }
  }

  deleteState(event) {
    var value = this.getIdAttributeValue(event);
    let state = this.player.state.find(s => s.name === value);
    let index = this.player.state.indexOf(state);
    this.player.state.splice(index, 1);
    this.signalrService.sendUpdateToPlayer(this.player.name, this.player);
  }

  deleteWeapon(event) {
    var confirmation = window.confirm('Are you sure you want to delete this item?');
    if (confirmation) {
      var value = this.getIdAttributeValue(event);
      let item = this.player.weapons.find(s => s.name === value);
      let index = this.player.weapons.indexOf(item);
      this.player.weapons.splice(index, 1);
      this.signalrService.sendUpdateToPlayer(this.player.name, this.player);
    }
  }

  deleteItem(event) {
    var confirmation = window.confirm('Are you sure you want to delete this item?');
    if (confirmation) {
      var value = this.getIdAttributeValue(event);
      let item = this.player.inventoryItems.find(s => s.name === value);
      let index = this.player.inventoryItems.indexOf(item);
      this.player.inventoryItems.splice(index, 1);
      this.signalrService.sendUpdateToPlayer(this.player.name, this.player);
    }
  }

  deleteAmmunition(event) {
    var confirmation = window.confirm('Are you sure you want to delete this item?');
    if (confirmation) {
      var value = this.getIdAttributeValue(event);
      let item = this.player.ammunitionList.find(s => s.name === value);
      let index = this.player.ammunitionList.indexOf(item);
      this.player.ammunitionList.splice(index, 1);
      this.signalrService.sendUpdateToPlayer(this.player.name, this.player);
    }
  }

  addState() {
    const modalRef = this.modalService.open(StateModalComponent);
    modalRef.result.then((result) => {
      let state = result as State;
      this.player.state.push(state);
      this.signalrService.sendUpdateToPlayer(this.player.name, this.player);
    }).catch((error) => {
      console.log(error);
    });
  }

  openWeaponModal(event) {
    const modalRef = this.modalService.open(WeaponModalComponent);
    var index = -1;
    let value = this.getIdAttributeValue(event);
    if (value !== '') {
      let weapon = this.player.weapons.find(w => w.name === value);
      index = this.player.weapons.indexOf(weapon);
      modalRef.componentInstance.weapon = this.player.weapons[index];
    }
    modalRef.result.then((result) => {
      this.saveWeaponResult(result, index);
    }).catch((error) => {
      console.log(error);
    });
  }

  saveWeaponResult(result, index) {
    let weapon = result as Weapon;
    if (index !== -1) {
      this.player.weapons[index] = weapon;
      this.signalrService.sendUpdateToPlayer(this.player.name, this.player);
    } else {
      this.player.weapons.push(weapon);
      this.signalrService.sendUpdateToPlayer(this.player.name, this.player);
    }
  }

  openItemModal(event) {
    const modalRef = this.modalService.open(ItemModalComponent);
    var index = -1;
    let value = this.getIdAttributeValue(event);
    if (value !== '') {
      let item = this.player.inventoryItems.find(i => i.name === value);
      index = this.player.inventoryItems.indexOf(item);
      modalRef.componentInstance.item = this.player.inventoryItems[index];
    }
    modalRef.result.then((result) => {
      let value = result as InventoryItem;
      if (index !== -1) {
        this.player.inventoryItems[index] = value;
        this.signalrService.sendUpdateToPlayer(this.player.name, this.player);
      } else {
        this.player.inventoryItems.push(value);
        this.signalrService.sendUpdateToPlayer(this.player.name, this.player);
      }
    }).catch((error) => {
      console.log(error);
    });
  }

  openAmmunitionModal(event) {
    const modalRef = this.modalService.open(AmmunitionModalComponent);
    var index = -1;
    let value = this.getIdAttributeValue(event);
    if (value !== '') {
      let ammunition = this.player.ammunitionList.find(a => a.name === value);
      index = this.player.ammunitionList.indexOf(ammunition);
      modalRef.componentInstance.ammunition = this.player.ammunitionList[index];
    }
    modalRef.result.then((result) => {
      let value = result as Ammunition;
      if (index !== -1) {
        this.player.ammunitionList[index] = value;
        this.signalrService.sendUpdateToPlayer(this.player.name, this.player);
      } else {
        this.player.ammunitionList.push(value);
        this.signalrService.sendUpdateToPlayer(this.player.name, this.player);
      }
    }).catch((error) => {
      console.log(error);
    });
  }

  openHealthModal(event) {
    const modalRef = this.modalService.open(HealthModalComponent);
    modalRef.componentInstance.currentHealth = this.player.currentHealthPoints;
    modalRef.componentInstance.maxHealth = this.player.healthPoints;
    modalRef.result.then((result) => {
      this.player.currentHealthPoints = result.currentHealth;
      this.player.healthPoints= result.maxHealth;
      this.signalrService.sendUpdateToPlayer(this.player.name, this.player);
    }).catch((error) => {
      console.log(error);
    });
  }

  openExperienceModal(event) {
    const modalRef = this.modalService.open(ExperienceModalComponent);
    modalRef.componentInstance.experience = this.player.experiencePoints;
    modalRef.result.then((result) => {
      this.player.experiencePoints = result.experience;
      this.signalrService.sendUpdateToPlayer(this.player.name, this.player);
    }).catch((error) => {
      console.log(error);
    });
  }
  
  updateMoney() {
    this.signalrService.sendUpdateToPlayer(this.player.name, this.player);
  }

}
