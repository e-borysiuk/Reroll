import { Component, Output, EventEmitter, Input, SimpleChanges, OnChanges } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Ammunition } from '../../../models/Ammunition';
import { ResourcesService } from '../../../services/ResourcesService';
import { Observable } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'ammunition-modal',
  templateUrl: './ammunition-modal.component.html', 
})

export class AmmunitionModalComponent {
  @Input() ammunition: Ammunition;
  myForm: FormGroup;
  ammunitionItems: any[];
  ammunitionNames;

  constructor(public activeModal: NgbActiveModal, private formBuilder: FormBuilder, private resourcesService: ResourcesService) {
    this.myForm = this.formBuilder.group({
      name: '',
      quantity: 0,
    });

    this.ammunitionItems = this.resourcesService.getAmmunitionResources();
    this.ammunitionNames = this.ammunitionItems.map(w => w.name);
  }

  search = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(200),
      distinctUntilChanged(),
      map((term: any) => term.length < 2 ? []
        : this.ammunitionNames.filter(v => v.toLowerCase().indexOf(term.toLowerCase()) > -1).slice(0, 10))
    );

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
