import { Component, Output, EventEmitter, Input, SimpleChanges, OnChanges } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { InventoryItem } from '../../../models/InventoryItem';
import { DiceTypeEnum } from '../../../models/DiceTypeEnum';

@Component({
  selector: 'item-modal',
  templateUrl: './item-modal.component.html', 
})

export class ItemModalComponent {
  @Input() item: InventoryItem;
  myForm: FormGroup;

  constructor(public activeModal: NgbActiveModal, private formBuilder: FormBuilder) {
    this.myForm = this.formBuilder.group({
      name: '',
      note: '',
      quantity: 0,
    });
  }

  ngOnInit() {
    if (this.item != null) {
      this.createForm();
    }
  }

  private createForm() {
    this.myForm = this.formBuilder.group({
      name: this.item.name,
      note: this.item.note,
      quantity: this.item.quantity
    });
  }

  private submitForm() {
    this.activeModal.close(this.myForm.value);
  }
}
