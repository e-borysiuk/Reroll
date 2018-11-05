
     import { Player } from './Player';
import { GameMaster } from './GameMaster';

export class GameSession   {
	public groupName: string;
	public playerModels: Player[];
	public gameMaster: GameMaster;
	public password: string;
}
