
using System;

namespace Reroll.Hubs
{
    using Microsoft.AspNetCore.SignalR;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using Reroll.Models;
    using Reroll.Models.Enums;
    using Reroll.Web.DAL;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class RerollHub : Hub
    {
        public RerollHub(IGameSessionRepository sessionsRepository)
        {
            this.sessionsRepository = sessionsRepository;
        }

        //TODO: On reconnect get player model
        /// <summary>
        /// Dictionary of (groupname +  (dictionary of player + connectionString))
        /// </summary>
        private static Dictionary<string, Dictionary<string, string>> groupPlayers = new Dictionary<string, Dictionary<string, string>>();

        private static List<GameSession> ActiveGameSessions = new List<GameSession>();

        private readonly IGameSessionRepository sessionsRepository;

        private static readonly List<string> Colors = new List<string> { "#00009a", "#8b008b", "#058205", "#a52a2a", "#2b2ba6", "#22008a" };
        
        #region Connection methods

        public async Task GroupExists(string groupName, string password)
        {
            var gameSession = ActiveGameSessions.FirstOrDefault(x => x.GroupName == groupName);
            if (gameSession == null)
                gameSession = await sessionsRepository.GetGameSession(groupName);

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
            await Clients.Caller.SendAsync("groupExistsResponse", response);
        }

        public async Task JoinGroup(string groupName, string playerName, string password, bool isGameMaster)
        {
            var gameSession = ActiveGameSessions.FirstOrDefault(x => x.GroupName == groupName);
            GameSession newGameSession;
            //No active session
            if (gameSession == null)
            {
                gameSession = await sessionsRepository.GetGameSession(groupName);
                //No session in database
                if (gameSession == null)
                {
                    newGameSession = CreateNewSession(groupName, playerName, password, isGameMaster);
                }
                //There is a session in database
                else
                {
                    newGameSession = await AssignClientToSession(gameSession, groupName, playerName, isGameMaster);
                }
                newGameSession.ConnectedClients++;
                ActiveGameSessions.Add(newGameSession);
            }
            //There is an active session
            else
            {
                newGameSession = await AssignClientToSession(gameSession, groupName, playerName, isGameMaster);
                newGameSession.ConnectedClients++;
                var index = ActiveGameSessions.FindIndex(g => g.GroupName == groupName);
                if (index == -1)
                    return;
                ActiveGameSessions[index] = newGameSession;
            }

            Context.Items.Add("Name", playerName);
            Context.Items.Add("Group", groupName);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        private GameSession CreateNewSession(string groupName, string playerName, string password, bool isGameMaster)
        {
            GameSession gameSession = new GameSession
            {
                Id = ObjectId.GenerateNewId(),
                GroupName = groupName,
                Players = new List<Player>(),
                Password = password,
                ActivityLogs = new List<ActivityMessage>()
            };
            if (isGameMaster)
            {
                gameSession.GameMaster = new GameMaster()
                {
                    Name = playerName,
                    ConnectionId = Context.ConnectionId
                };
            }
            else
            {
                gameSession.Players.Add(new Player()
                {
                    Name = playerName,
                    ConnectionId = Context.ConnectionId,
                    Color = Colors[gameSession.Players.Count],
                    AmmunitionList = new List<Ammunition>(),
                    Weapons = new List<Weapon>(),
                    LearnedSpells = new List<Spell>(),
                    PreparedSpells = new List<PreparedSpell>(),
                    State = new List<State>(),
                    InventoryItems = new List<InventoryItem>()
                });
            }

            return gameSession;
        }

        private GameSession AddOrUpdateGameMaster(GameSession gameSession, string playerName)
        {
            if (gameSession.GameMaster == null)
            {
                gameSession.GameMaster = new GameMaster()
                {
                    Name = playerName,
                    ConnectionId = Context.ConnectionId
                };
            }
            else
            {
                gameSession.GameMaster.ConnectionId = Context.ConnectionId;
                gameSession.GameMaster.Name = playerName;
            }
            return gameSession;
        }

        private async Task<GameSession> AddOrUpdatePlayer(GameSession gameSession, string groupName, string playerName)
        {
            var index = gameSession.Players.FindIndex(p => p.Name == playerName);
            if (index == -1)
            {
                gameSession.Players.Add(new Player()
                {
                    Name = playerName,
                    ConnectionId = Context.ConnectionId,
                    Color = Colors[gameSession.Players.Count],
                    AmmunitionList = new List<Ammunition>(),
                    Weapons = new List<Weapon>(),
                    LearnedSpells = new List<Spell>(),
                    PreparedSpells = new List<PreparedSpell>(),
                    State = new List<State>(),
                    InventoryItems = new List<InventoryItem>()
                });
                await this.SendInitialGmData(gameSession.GroupName);
            }
            else
            {
                gameSession.Players[index].ConnectionId = Context.ConnectionId;
                await this.SendInitialPlayerData(groupName, playerName);
            }

            return gameSession;
        }

        private async Task<GameSession> AssignClientToSession(GameSession gameSession, string groupName, string playerName, bool isGameMaster)
        {
            if (isGameMaster)
            {
                AddOrUpdateGameMaster(gameSession, playerName);
            }
            else
            {
                await AddOrUpdatePlayer(gameSession, groupName, playerName);
            }

            return gameSession;
        }

        #endregion

        #region Functional methods

        public Task ChangeName(string value)
        {
            Context.Items.TryGetValue("Group", out var groupItem);
            string group = (string)groupItem;
            Context.Items.TryGetValue("Name", out var nameItem);
            string name = (string)nameItem;
            var player = ActiveGameSessions.First(x => x.GroupName == group).Players.First(x => x.Name == name);

            player.Name = value;
            Context.Items.Remove("Name");
            Context.Items.Add("Name", value);

            return Clients.Groups(group).SendAsync("sendUpdateToGM", name, player);
        }

        public Task UpdateModel(Player value)
        {
            Context.Items.TryGetValue("Group", out var groupItem);
            string group = (string)groupItem;
            Context.Items.TryGetValue("Name", out var nameItem);
            string name = (string)nameItem;
            value.Name = name;
            value.ConnectionId = Context.ConnectionId;

            var Players = ActiveGameSessions.First(x => x.GroupName == group).Players;
            var player = Players.First(x => x.Name == name);
            var index = Players.IndexOf(player);
            value.Color = Colors[index];
            Players.Remove(player);
            Players.Add(value);

            return Clients.Groups(group).SendAsync("sendUpdateToGM", name, value);
        }

        public Task UpdatePlayerModel(string playerName, Player value)
        {
            if (playerName != value.Name)
                return null;
            Context.Items.TryGetValue("Group", out var groupItem);
            string group = (string)groupItem;

            var players = ActiveGameSessions.First(x => x.GroupName == group).Players;
            players.Remove(players.First(x => x.Name == playerName));
            players.Add(value);

            return Clients.Client(value.ConnectionId).SendAsync("sendUpdateToPlayer", value);
        }

        public async Task GetInitialGmData()
        {
            Context.Items.TryGetValue("Group", out var groupItem);
            await this.SendInitialGmData(groupItem?.ToString());
        }

        private async Task SendInitialGmData(string groupName = "")
        {
            if(string.IsNullOrEmpty(groupName))
                Context.Items.TryGetValue("Group", out var groupItem);
            var gameSession = ActiveGameSessions.FirstOrDefault(x => x.GroupName == groupName);
            var data = gameSession?.Players;
            if (gameSession != null && data != null)
                await Clients.Client(gameSession.GameMaster.ConnectionId).SendAsync("receiveInitialGmData", data);
        }

        private async Task SendInitialPlayerData(string groupName, string playerName)
        {
            var gameSession = ActiveGameSessions.FirstOrDefault(x => x.GroupName == groupName);
            var data = gameSession?.Players.FirstOrDefault(x => x.Name == playerName);
            if (gameSession != null && data != null)
                await Clients.Client(data.ConnectionId).SendAsync("receiveInitialPlayerData", data);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (Context.Items.TryGetValue("Group", out var groupItem))
            {
                string groupName = (string)groupItem;

                var gameSession = ActiveGameSessions.FirstOrDefault(x => x.GroupName == groupName);
                if (gameSession?.ConnectedClients == 1)
                {
                    gameSession.ConnectedClients--;
                    await sessionsRepository.Update(gameSession);
                }
                else
                {
                    if (gameSession != null)
                        gameSession.ConnectedClients--;
                }

                await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            }
            await base.OnDisconnectedAsync(exception);
        }

        public async Task GetInitialLogs()
        {
            Context.Items.TryGetValue("Group", out var groupItem);
            string groupName = (string)groupItem;
            var gameSession = ActiveGameSessions.FirstOrDefault(x => x.GroupName == groupName);
            var data = gameSession?.ActivityLogs;
            if (gameSession != null && data != null)
                await Clients.Group(gameSession.GroupName).SendAsync("receiveInitialLogs", data);
        }

        public async Task SendActivityLog(string message)
        {
            Context.Items.TryGetValue("Group", out var groupItem);
            string groupName = (string)groupItem;
            Context.Items.TryGetValue("Name", out var nameItem);
            string name = (string)nameItem;
            var gameSession = ActiveGameSessions.FirstOrDefault(x => x.GroupName == groupName);
            var player = gameSession.Players.FirstOrDefault(p => p.Name == name);
            var activityMessage = new ActivityMessage
            {
                Color = player.Color,
                Message = $"{name}: {message}"
            };
            if(gameSession.ActivityLogs.Count != 0)
                if (gameSession.ActivityLogs[gameSession.ActivityLogs.Count - 1] == activityMessage) return;
            gameSession.ActivityLogs.Add(activityMessage);
            await Clients.Client(gameSession.GameMaster.ConnectionId).SendAsync("receiveActivityLog", activityMessage);
        }

        public async Task SendDiceRoll(int rollValue, string diceType)
        {
            Context.Items.TryGetValue("Group", out var groupItem);
            string groupName = (string)groupItem;
            Context.Items.TryGetValue("Name", out var nameItem);
            string name = (string)nameItem;
            var gameSession = ActiveGameSessions.FirstOrDefault(x => x.GroupName == groupName);
            string roll;
            DateTime dt = DateTime.Now;

            if (name == gameSession.GameMaster.Name)
                roll = $"{dt:T}: Game Master rolled {rollValue} on {diceType} dice.";
            else
                roll = $"{dt:T}: Player {name} rolled {rollValue} on {diceType} dice.";
            await Clients.Group(gameSession.GroupName).SendAsync("receiveDiceRoll", roll);
        }

        public Player CreateSampleModel()
        {
            return new Player
            {
                AmmunitionList = new List<Ammunition>
                {
                    new Ammunition
                    {
                        Name = "Arrows",
                        Quantity = 12
                    }
                },
                ArmorClass = 11,
                BaseAttackBonus = 2,
                Charisma = 12,
                Constitution = 13,
                ConnectionId = "",
                Copper = 3,
                Dexterity = 15,
                ExperiencePoints = 120,
                Fortitude = 10,
                Gold = 1,
                HealthPoints = 40,
                Initiative = 2,
                Intelligence = 11,
                InventoryItems = new List<InventoryItem>
                {
                    new InventoryItem
                    {
                        Name = "Some item",
                        Note = "Doing something",
                        Quantity = 1
                    }
                },
                Name = "PlayerOne",
                LearnedSpells = new List<Spell>
                {
                    new Spell
                    {
                        Name = "Missles",
                        Level = 1
                    },
                    new Spell
                    {
                        Name = "Invisiblility",
                        Level = 3
                    }
                },
                Platinum = 0,
                PreparedSpells = new List<PreparedSpell>
                {
                    new PreparedSpell
                    {
                        Spell = new Spell {Name = "Missles", Level = 1},
                        CastQuantity = 2
                    }
                },
                Reflex = 11,
                Silver = 2,
                State = new List<State>
                {
                    new State
                    {
                        Name = "Sleepy",
                        Description = "zzz"
                    }
                },
                Strength = 15,
                Weapons = new List<Weapon>
                {
                    new Weapon
                    {
                        Name = "Axe",
                        Notes = "",
                        Critical = "20",
                        Damage = "2d8"
                    }
                },
                Will = 8,
                Wisdom = 9
            };
        }
        #endregion
    }
}