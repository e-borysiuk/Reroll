import { Component, Output, EventEmitter, Input, SimpleChanges, OnChanges } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'state-modal',
  templateUrl: './state-modal.component.html', 
})

export class StateModalComponent {
  myForm: FormGroup;

  constructor(public activeModal: NgbActiveModal, private formBuilder: FormBuilder) {
    this.myForm = this.formBuilder.group({
      name: '',
      description: ''
    });
  }

  private submitForm() {
    this.activeModal.close(this.myForm.value);
  }
}
