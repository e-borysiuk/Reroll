
     import { Player } from './Player';
import { GameMaster } from './GameMaster';
import { ActivityMessage } from './ActivityMessage';

export class GameSession   {
	public groupName: string;
	public players: Player[];
	public gameMaster: GameMaster;
	public password: string;
	public connectedClients: number;
	public activityLogs: ActivityMessage[];
}
