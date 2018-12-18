import { Component, Output, EventEmitter, Input, SimpleChanges, OnChanges } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Weapon } from '../../../models/Weapon';
import { DiceTypeEnum } from '../../../models/DiceTypeEnum';
import { ResourcesService } from '../../../services/ResourcesService';
import { Observable } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'experience-modal',
  templateUrl: './experience-modal.component.html', 
})

export class ExperienceModalComponent {
  @Input() experience: number;
  myForm: FormGroup;

  constructor(public activeModal: NgbActiveModal, private formBuilder: FormBuilder, private resourcesService: ResourcesService) {
    this.myForm = this.formBuilder.group({
      experience: 0
    });
  }

  ngOnInit() {
    if (this.experience !== 0) {



      this.createForm();
    }
  }

  private createForm() {
    this.myForm = this.formBuilder.group({
      experience: this.experience
    });
  }

  private submitForm() {
    this.activeModal.close(this.myForm.value);
  }
}
