using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace Reroll.Models
{
    public class GameSession
    {
        public ObjectId Id { get; set; }
        public string GroupName { get; set; }
        public List<Player> Players { get; set; }
        public GameMaster GameMaster { get; set; }
        public string Password { get; set; }
        public int ConnectedClients { get; set; }
    }
}
