using System;
using System.Collections.Generic;
using System.Text;
using Reroll.Models.Enums;

namespace Reroll.Models
{
    public class SkillModel
    {
        public string Name { get; set; }
        public KeyAbilityEnum KeyAbility { get; set; }
        public int SkillModifier { get; set; }
    }
}
