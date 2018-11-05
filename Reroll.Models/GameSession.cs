using System;
using System.Collections.Generic;
using System.Text;

namespace Reroll.Models
{
    public class GameSession
    {
        public string GroupName { get; set; }
        public List<Player> PlayerModels { get; set; }
        public GameMaster GameMaster { get; set; }
        public string Password { get; set; }
    }
}
