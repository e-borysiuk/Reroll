using System;
using System.Collections.Generic;

namespace Reroll.Models
{
    public class Player
    {  
        public string ConnectionId { get; set; }
        public string Name { get; set; }
        public int HealthPoints { get; set; }
        public int CurrentHealthPoints { get; set; }
        public int ExperiencePoints { get; set; }

        #region Money   

        public int Copper { get; set; }
        public int Silver { get; set; }
        public int Gold { get; set; }
        public int Platinum { get; set; }

        #endregion

        #region BaseStats

        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }

        #endregion

        #region SecondaryStats

        public int ArmorClass { get; set; }
        public int Initiative { get; set; }
        public int Fortitude { get; set; }
        public int Reflex { get; set; }
        public int Will { get; set; }
        public int BaseAttackBonus { get; set; }

        #endregion

        #region Skills

        public List<Skill> Skills { get; set; }
        public List<string> Languages { get; set; }

        #endregion

        #region Ammunition
        //Type + quantity
        public List<Ammunition> AmmunitionList { get; set; }

        #endregion

        #region Weapons

        public List<Weapon> Weapons { get; set; }

        #endregion

        #region -Feats & Abilities
        //Name + note
        public List<Feat> Feats { get; set; }

        public List<Ability> SpecialAbilities { get; set; }

        #endregion

        #region Spells

        public List<AvailableSpellsRow> AvailableSpells { get; set; }

        public List<Spell> KnownSpells;

        //Spell+quantity
        public List<PreparedSpell> PreparedSpells { get; set; }

        #endregion

        #region Inventory

        public List<InventoryItem> InventoryItems { get; set; }

        #endregion

        #region ActiveStates

        //Name + effect
        public List<State> State { get; set; }

        #endregion
    }
}