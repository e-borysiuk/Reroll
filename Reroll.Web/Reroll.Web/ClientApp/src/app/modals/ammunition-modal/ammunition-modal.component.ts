import { Component, Output, EventEmitter, Input, SimpleChanges, OnChanges } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Ammunition } from '../../../models/Ammunition';

@Component({
  selector: 'ammunition-modal',
  templateUrl: './ammunition-modal.component.html', 
})

export class AmmunitionModalComponent {
  @Input() ammunition: Ammunition;
  myForm: FormGroup;

  constructor(public activeModal: NgbActiveModal, private formBuilder: FormBuilder) {
    this.myForm = this.formBuilder.group({
      name: '',
      quantity: 0,
    });
  }

  ngOnInit() {
    if (this.ammunition != null) {
      this.createForm();
    }
  }

  private createForm() {
    this.myForm = this.formBuilder.group({
      name: this.ammunition.name,
      quantity: this.ammunition.quantity
    });
  }

  private submitForm() {
    this.activeModal.close(this.myForm.value);
  }
}
