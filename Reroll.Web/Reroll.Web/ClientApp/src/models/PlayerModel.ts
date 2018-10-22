
     import { Tuple<string, string> } from './tuple<string, string>';

export class Player   {
	public connectionId: string;
	public name: string;
	public healthPoints: number;
	public experiencePoints: number;
	public copper: number;
	public silver: number;
	public gold: number;
	public platinum: number;
	public strength: number;
	public dexterity: number;
	public constitution: number;
	public intelligence: number;
	public wisdom: number;
	public charisma: number;
	public armorClass: number;
	public initiative: number;
	public fortitude: number;
	public reflex: number;
	public will: number;
	public baseAttackBonus: number;
	public state: Tuple<string, string>[];
}
