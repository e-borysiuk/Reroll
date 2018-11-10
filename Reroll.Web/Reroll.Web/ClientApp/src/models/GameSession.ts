
     import { Player } from './Player';
import { GameMaster } from './GameMaster';

export class GameSession   {
	public groupName: string;
	public players: Player[];
	public gameMaster: GameMaster;
	public password: string;
	public connectedClients: number;
}
