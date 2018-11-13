
     import { DiceTypeEnum } from './DiceTypeEnum';

export class Weapon   {
	public name: string;
	public attackBonus: number;
	public diceCount: number;
	public diceType: DiceTypeEnum;
	public critical: string;
	public notes: string;
}
