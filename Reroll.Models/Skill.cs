using Reroll.Models.Enums;

namespace Reroll.Models
{
    public class Skill
    {
        public string Name { get; set; }
        public KeyAbilityEnum KeyAbility { get; set; }
        public int SkillModifier { get; set; }
    }
}
