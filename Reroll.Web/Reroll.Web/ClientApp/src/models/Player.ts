﻿
     import { Ammunition } from './Ammunition';
import { Weapon } from './Weapon';
import { PreparedSpell } from './PreparedSpell';
import { InventoryItem } from './InventoryItem';
import { State } from './State';

export class Player   {
	public connectionId: string;
	public name: string;
	public healthPoints: number;
	public currentHealthPoints: number;
	public experiencePoints: number;
	public color: string;
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
	public ammunitionList: Ammunition[];
	public weapons: Weapon[];
	public preparedSpells: PreparedSpell[];
	public inventoryItems: InventoryItem[];
	public state: State[];
}
