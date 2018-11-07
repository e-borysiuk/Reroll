using System.Collections.Generic;

namespace Reroll.Models
{
    public class GameMaster
    {
        public string ConnectionId { get; set; }
        public string Name { get; set; }
        public List<string> NotesList { get; set; }
    }
}