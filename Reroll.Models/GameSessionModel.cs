using System;
using System.Collections.Generic;
using System.Text;

namespace Reroll.Models
{
    public class GameSessionModel
    {
        public string GroupName { get; set; }
        public List<PlayerModel> PlayerModels { get; set; }
        public GameMasterModel GameMaster { get; set; }
        public string Password { get; set; }
    }
}
