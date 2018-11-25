import { Component, Output, EventEmitter, Input, SimpleChanges, OnChanges } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Weapon } from '../../../models/Weapon';
import { DiceTypeEnum } from '../../../models/DiceTypeEnum';
import { ResourcesService } from '../../../services/ResourcesService';
import { Observable } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'weapon-modal',
  templateUrl: './weapon-modal.component.html', 
})

export class WeaponModalComponent {
  @Input() weapon: Weapon;
  myForm: FormGroup;
  weapons: any[];
  weaponNames;

  constructor(public activeModal: NgbActiveModal, private formBuilder: FormBuilder, private resourcesService: ResourcesService) {
    this.myForm = this.formBuilder.group({
      name: '',
      damage: '',
      critical: '',
      notes: '',
    });

    this.weapons = this.resourcesService.getWeaponResources();
    this.weaponNames = this.weapons.map(w => w.name);
  }

  selectedItem(item) {
    let selected: any = this.weapons.filter(i => i.name === item.item);
    if (selected.length > 0) {
      var selectedWeapon = selected[0];
      this.myForm.patchValue({
        damage: selectedWeapon.dmg_m,
        critical: selectedWeapon.critical
      });
    }
  }

  search = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(200),
      distinctUntilChanged(),
      map((term: any)=> term.length < 2 ? []
        : this.weaponNames.filter(v => v.toLowerCase().indexOf(term.toLowerCase()) > -1).slice(0, 10))
    );

  ngOnInit() {
    if (this.weapon != null) {
      this.createForm();
    }
  }

  private createForm() {
    this.myForm = this.formBuilder.group({
      name: this.weapon.name,
      damage: this.weapon.damage,
      critical: this.weapon.critical,
      notes: this.weapon.notes,
    });
  }

  private submitForm() {
    this.activeModal.close(this.myForm.value);
  }
}
