using System;
using System.Collections.Generic;

namespace Reroll.Models
{
    public class PlayerModel
    {
        public string ConnectionId { get; set; }
        public string Name { get; set; }
        public int HealthPoints { get; set; }
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

        public List<SkillModel> Skills;
        public List<string> Languages;

        #endregion

        #region Ammunition
        //Type + quantity
        public List<Tuple<string, int>> AmmunitionList;

        #endregion

        #region Weapons

        public List<WeaponModel> Weapons;

        #endregion

        #region Feats & Abilities
        //Name + note
        public List<Tuple<string, string>> Feats;

        public List<Tuple<string, string>> SpecialAbilities;

        #endregion

        #region Spells

        public List<AvailableSpellsRow> AvailableSpells;

        public List<Spell> KnownSpells;

        //Spell+quantity
        public List<Tuple<Spell, int>> PreparedSpells;

        #endregion

        #region Inventory

        public List<InventoryItem> InventoryItems;

        #endregion

        #region ActiveStates

        //Name + effect
        public List<Tuple<string, string>> State { get; set; }

        #endregion
    }
}