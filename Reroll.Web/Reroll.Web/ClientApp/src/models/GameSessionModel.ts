
     import { Player } from './player';
import { GameMaster } from './game-master';

export class GameSession   {
	public groupName: string;
	public playerModels: Player[];
	public gameMaster: GameMaster;
}
