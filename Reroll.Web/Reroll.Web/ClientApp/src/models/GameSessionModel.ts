
import { Player } from './PlayerModel';
import { GameMaster } from './GameMasterModel';

export class GameSession   {
	public groupName: string;
	public playerModels: Player[];
	public gameMaster: GameMaster;
	public password: string;
}
