﻿
     import { DiceTypeEnum } from './dice-type-enum';

export class Weapon   {
	public name: string;
	public attackBonus: number;
	public diceCount: number;
	public diceType: DiceTypeEnum;
	public critical: string;
	public notesList: string[];
	public isRanged: boolean;
}
