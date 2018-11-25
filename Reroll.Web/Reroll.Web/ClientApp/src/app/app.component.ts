import { Component } from '@angular/core';
import { ResourcesService } from '../services/ResourcesService';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';
  constructor(private resourcesService: ResourcesService) {
    resourcesService.init();
  }
}
