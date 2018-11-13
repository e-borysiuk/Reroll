import { Component, Output, EventEmitter, Input, SimpleChanges, OnChanges } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Weapon } from '../../../models/Weapon';
import { DiceTypeEnum } from '../../../models/DiceTypeEnum';

@Component({
  selector: 'weapon-modal',
  templateUrl: './weapon-modal.component.html', 
})

export class WeaponModalComponent {
  @Input() weapon: Weapon;
  myForm: FormGroup;

  constructor(public activeModal: NgbActiveModal, private formBuilder: FormBuilder) {
    this.myForm = this.formBuilder.group({
      name: '',
      attackBonus: '',
      diceCount: '',
      diceType: '',
      critical: '',
      notes: '',
    });
  }

  ngOnInit() {
    this.createForm();
  }

  private createForm() {
    this.myForm = this.formBuilder.group({
      name: this.weapon.name,
      attackBonus: this.weapon.attackBonus,
      diceCount: this.weapon.diceCount,
      diceType: this.weapon.diceType,
      critical: this.weapon.critical,
      notes: this.weapon.notes,
    });
  }

  private submitForm() {
    this.activeModal.close(this.myForm.value);
  }
}
