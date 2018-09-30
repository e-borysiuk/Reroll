using System.Linq;
using Reroll.Web.Models.Enums;

namespace Reroll.Hubs
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;

    public class RerollHub : Hub
    {
        /// <summary>
        /// Dictionary of (groupname +  (dictionary of player + connectionString))
        /// </summary>
        private static Dictionary<string, Dictionary<string, string>> groupPlayers = new Dictionary<string, Dictionary<string, string>>();

        public async Task SendToAll(string name, string message)
        {
            await Clients.All.SendAsync("sendToAll", name, message);
        }

        public async Task SendToPlayer(string player, string message)
        {
            var connectionId = groupPlayers[(string)Context.Items["Group"]][player]; 
            await Clients.Client(connectionId).SendAsync("sendToPlayer", "GM", message);
        }

        public Task SendMessageToGroups(string name, string message)
        {
            List<string> groups = new List<string>() { "SignalR Users" };
            return Clients.Groups(groups).SendAsync("sendToAll", name, message);
        }

        public Task GroupExists(string groupName, string password)
        {
            var exists =  groupPlayers.Keys.Any(x => x == groupName);
            ResponseStatusEnum response = exists ? ResponseStatusEnum.GroupExists : ResponseStatusEnum.GroupDoesNotExist;
            return Clients.Caller.SendAsync("groupExistsResponse", response);
        }

        public async Task JoinGroup(string groupName, string playerName)
        {
            if(!groupPlayers.ContainsKey(groupName))
                groupPlayers.Add(groupName, new Dictionary<string, string>());

            groupPlayers[groupName].Add(playerName, Context.ConnectionId);
            Context.Items.Add("Group", groupName);
            Context.Items.Add("Name", playerName);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
    }
}