import { Component, Output, EventEmitter, Input, SimpleChanges, OnChanges } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { InventoryItem } from '../../../models/InventoryItem';
import { ResourcesService } from '../../../services/ResourcesService';
import { Observable } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'item-modal',
  templateUrl: './item-modal.component.html', 
})

export class ItemModalComponent {
  @Input() item: InventoryItem;
  myForm: FormGroup;
  itemItems: any[];
  itemNames;

  constructor(public activeModal: NgbActiveModal, private formBuilder: FormBuilder, private resourcesService: ResourcesService) {
    this.myForm = this.formBuilder.group({
      name: '',
      note: '',
      quantity: 0,
    });

    this.itemItems = this.resourcesService.getItemsResources();
    this.itemNames = this.itemItems.map(w => w.name);
  }

  search = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(200),
      distinctUntilChanged(),
      map((term: any) => term.length < 2 ? []
        : this.itemNames.filter(v => v.toLowerCase().indexOf(term.toLowerCase()) > -1).slice(0, 10))
    );

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
