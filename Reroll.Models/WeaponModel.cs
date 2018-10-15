using System.Collections.Generic;

namespace Reroll.Models
{
    public class WeaponModel
    {
        public string Name { get; set; }
        public int AttackBonus { get; set; }
        public int DiceCount { get; set; }
        public DiceTypeEnum DiceType { get; set; }
        public string Critical { get; set; }
        public List<string> NotesList { get; set; }
        public bool IsRanged { get; set; }
    }
}