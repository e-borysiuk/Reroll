import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class ResourcesService {
  data: any;

  constructor(private http: HttpClient) {
  }

  init() {
    this.http.get('assets/items.json').subscribe((items: any) => {
      // Read the user infos  from the JSON response
      this.data = items.equipments.equipment;
    });
  }

  getResources() {
  }

  getWeaponResources() {
    return this.data.filter(item => item.family === 'Weapons' && item.subcategory !== 'Ammunition');
  }

  getItemsResources() {
    return this.data.filter(item => item.family === 'Trade Goods' || item.family === 'Trade Goods' || 'Goods and Services');
  }

  getAmmunitionResources() {
    return this.data.filter(item => item.subcategory === 'Ammunition');
  }
}
