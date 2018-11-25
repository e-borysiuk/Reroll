import { Injectable } from '@angular/core';

export interface IHash {
  [key: string]: string;
}

@Injectable()
export class TabService {
  myHash: IHash = {};

  getCurrentCard(name: string) : string {
    return this.myHash[name];
  }

  setCurrentCard(name: string, currentTab: string) {
    this.myHash[name] = currentTab;
  }
}
