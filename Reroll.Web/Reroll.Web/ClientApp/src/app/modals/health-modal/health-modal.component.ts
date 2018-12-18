import { Component, Output, EventEmitter, Input, SimpleChanges, OnChanges } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Weapon } from '../../../models/Weapon';
import { DiceTypeEnum } from '../../../models/DiceTypeEnum';
import { ResourcesService } from '../../../services/ResourcesService';
import { Observable } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'health-modal',
  templateUrl: './health-modal.component.html', 
})

export class HealthModalComponent {
  @Input() currentHealth: number;
  @Input() maxHealth: number;
  myForm: FormGroup;

  constructor(public activeModal: NgbActiveModal, private formBuilder: FormBuilder, private resourcesService: ResourcesService) {
    this.myForm = this.formBuilder.group({
      currentHealth: 0,
      maxHealth: 0
    });
  }

  ngOnInit() {
    if (this.maxHealth !== 0) {
      this.createForm();
    }
  }

  private createForm() {
    this.myForm = this.formBuilder.group({
      currentHealth: this.currentHealth,
      maxHealth: this.maxHealth
    });
  }

  private submitForm() {
    this.activeModal.close(this.myForm.value);
  }
}
