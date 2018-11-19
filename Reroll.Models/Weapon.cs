using System.Collections.Generic;
using Reroll.Models.Enums;

namespace Reroll.Models
{
    public class Weapon
    {
        public string Name { get; set; }
        public int AttackBonus { get; set; }
        public int DiceCount { get; set; }
        public string DiceType { get; set; }
        public string Critical { get; set; }
    }
}