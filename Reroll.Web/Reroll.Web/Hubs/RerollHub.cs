using System.Linq;

namespace Reroll.Hubs
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;
    using Reroll.Models;

    public class RerollHub : Hub
    {
        //TODO: On reconnect get player model
        /// <summary>
        /// Dictionary of (groupname +  (dictionary of player + connectionString))
        /// </summary>
        private static Dictionary<string, Dictionary<string, string>> groupPlayers = new Dictionary<string, Dictionary<string, string>>();

        private static List<GameSessionModel> GameSessions = new List<GameSessionModel>();

        #region Connection methods

        public Task GroupExists(string groupName, string password)
        {
            var gameSession = GameSessions.FirstOrDefault(x => x.GroupName == groupName);
            ResponseStatusEnum response;
            if (gameSession != null)
            {
                if (gameSession.Password == password)
                    response = ResponseStatusEnum.GroupExists;
                else
                    response = ResponseStatusEnum.InvalidPassword;
            }
            else
            {
                response = ResponseStatusEnum.GroupDoesNotExist;
            }
            return Clients.Caller.SendAsync("groupExistsResponse", response);
        }

        public async Task JoinGroup(string groupName, string playerName, string password, bool isGameMaster)
        {
            var gameSession = GameSessions.FirstOrDefault(x => x.GroupName == groupName);
            if (gameSession == null)
            {
                gameSession = new GameSessionModel
                {
                    GroupName = groupName,
                    PlayerModels = new List<PlayerModel>(),
                    Password = password
                };
                if (isGameMaster)
                {
                    gameSession.GameMaster = new GameMasterModel()
                    {
                        Name = playerName,
                        ConnectionId = Context.ConnectionId
                    };
                }
                else
                {
                    gameSession.PlayerModels.Add(new PlayerModel()
                    {
                        Name = playerName,
                        ConnectionId = Context.ConnectionId
                    });
                }
                GameSessions.Add(gameSession);
            }
            else
            {
                if (isGameMaster)
                {
                    gameSession.GameMaster = new GameMasterModel()
                    {
                        Name = playerName,
                        ConnectionId = Context.ConnectionId
                    };
                    await this.SendInitialGmData(groupName);
                }
                else
                {
                    var index = gameSession.PlayerModels.FindIndex(p => p.Name == playerName);
                    if (index == -1)
                    {
                        gameSession.PlayerModels.Add(new PlayerModel()
                        {
                            Name = playerName,
                            ConnectionId = Context.ConnectionId
                        });
                    }
                    else
                    {
                        gameSession.PlayerModels[index].ConnectionId = Context.ConnectionId;
                        await this.SendInitialPlayerData(groupName, playerName);
                    }
                }
            }
            Context.Items.Add("Name", playerName);
            Context.Items.Add("Group", groupName);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        #endregion

        #region Functional methods

        public Task ChangeName(string value)
        {
            Context.Items.TryGetValue("Group", out var groupItem);
            string group = (string) groupItem;
            Context.Items.TryGetValue("Name", out var nameItem);
            string name = (string) nameItem;
            var player = GameSessions.First(x => x.GroupName == group).PlayerModels.First(x => x.Name == name);

            player.Name = value;
            Context.Items.Remove("Name");
            Context.Items.Add("Name", value);

            return Clients.Groups(group).SendAsync("sendUpdateToGM", name, player);
        }

        public Task UpdateModel(PlayerModel value)
        {
            Context.Items.TryGetValue("Group", out var groupItem);
            string group = (string)groupItem;
            Context.Items.TryGetValue("Name", out var nameItem);
            string name = (string)nameItem;
            value.Name = name;
            value.ConnectionId = Context.ConnectionId;

            var playerModels = GameSessions.First(x => x.GroupName == group).PlayerModels;
            playerModels.Remove(playerModels.First(x => x.Name == name));
            playerModels.Add(value);

            return Clients.Groups(group).SendAsync("sendUpdateToGM", name, value);
        }

        public Task UpdatePlayerModel(string playerName, PlayerModel value)
        {
            if (playerName != value.Name)
                return null;
            Context.Items.TryGetValue("Group", out var groupItem);
            string group = (string)groupItem;

            var playerModels = GameSessions.First(x => x.GroupName == group).PlayerModels;
            playerModels.Remove(playerModels.First(x => x.Name == playerName));
            playerModels.Add(value);

            return Clients.Client(value.ConnectionId).SendAsync("sendUpdateToPlayer", value);
        }

        private async Task SendInitialGmData(string groupName)
        {
            var gameSession = GameSessions.FirstOrDefault(x => x.GroupName == groupName);
            var data = gameSession?.PlayerModels;
            if (gameSession != null && data != null)
                await Clients.Client(gameSession.GameMaster.ConnectionId).SendAsync("receiveInitialGmData", data);
        }

        private async Task SendInitialPlayerData(string groupName, string playerName)
        {
            var gameSession = GameSessions.FirstOrDefault(x => x.GroupName == groupName);
            var data = gameSession?.PlayerModels.FirstOrDefault(x => x.Name == playerName);
            if (gameSession != null && data != null)
                await Clients.Client(data.ConnectionId).SendAsync("receiveInitialPlayerData", data);
        }

        #endregion
    }
}