using System;
using System.Collections.Generic;
using MvvmCross;
using MvvmCross.Plugin.Messenger;
using Reroll.Mobile.Core.Interfaces;
using Reroll.Mobile.Core.Models.MvxMessages;
using Reroll.Models;
using Reroll.Models.Enums;

namespace Reroll.Mobile.Core.Repositories
{
    public class DataRepository : IDataRepository
    {
        public DataRepository()
        {
            this._messenger = Mvx.Resolve<IMvxMessenger>();
            this._signalrService = Mvx.Resolve<ISignalrService>();
            this._messageToken = this._messenger.Subscribe<UpdateMessage>(ReceivedUpdate);
            this.Player = CreateSampleModel();
        }

        private readonly IMvxMessenger _messenger;
        private readonly ISignalrService _signalrService;
        private readonly MvxSubscriptionToken _messageToken;

        private Player _Player;

        public Player Player
        {
            get => _Player;
            set
            {
                _Player = value;
                RefreshUi();
            }
        }

        public void SendUpdate(Player data)
        {
            this.Player = data;
            this._signalrService.SendUpdate(data);
        }

        private void ReceivedUpdate(UpdateMessage obj)
        {
            this.Player = obj.Player;
        }
        
        private void RefreshUi()
        {
            this._messenger.Publish(new RefreshMessage(this));
        }

        public Player CreateSampleModel()
        {
            return new Player
            {
                AmmunitionList = new List<Ammunition>
                {
                    new Ammunition
                    {
                        Name = "Arrows",
                        Quantity = 12
                    }
                },
                ArmorClass = 11,
                AvailableSpells = new List<AvailableSpellsRow>
                {
                    new AvailableSpellsRow()
                    {
                        Level = 0,
                        BonusSpells = 2,
                        SpellSaveDC = 2,
                        SpellsKnows = 4,
                        SpellsPerDay = 4
                    }
                },
                BaseAttackBonus = 2,
                Charisma = 12,
                Constitution = 13,
                ConnectionId = "",
                Copper = 3,
                Dexterity = 15,
                ExperiencePoints = 120,
                Feats = new List<Feat>
                {
                    new Feat
                    {
                        Name = "Very Strong",
                        Description = "Many muscle"
                    }
                },
                Fortitude = 10,
                Gold = 1,
                HealthPoints = 40,
                CurrentHealthPoints = 24,
                Initiative = 2,
                Intelligence = 11,
                InventoryItems = new List<InventoryItem>
                {
                    new InventoryItem
                    {
                        Name = "Some item",
                        Note = "Doing something",
                        Quantity = 1
                    }
                },
                Name = "PlayerOne",
                LearnedSpells = new List<Spell>
                {
                    new Spell
                    {
                        Name = "Missles",
                        Level = 1
                    },
                    new Spell
                    {
                        Name = "Invisiblility",
                        Level = 3
                    }
                },
                Languages = new List<string>
                {
                    "Common", "Orcish"
                },
                Platinum = 0,
                PreparedSpells = new List<PreparedSpell>
                {
                    new PreparedSpell
                    {
                        Spell = new Spell {Name = "Missles", Level = 1},
                        CastQuantity = 2
                    }
                },
                Reflex = 11,
                Silver = 2,
                Skills = new List<Skill>
                {
                    new Skill
                    {
                        Name = "Riding",
                        KeyAbility = KeyAbilityEnum.Dex,
                        SkillModifier = 5
                    }
                },
                SpecialAbilities = new List<Ability>
                {
                    new Ability
                    {
                        Name = "Strong attack",
                        Description = "Hits strong"
                    }
                },
                State = new List<State>
                {
                    new State
                    {
                        Name = "Sleepy",
                        Description = "zzz"
                    }
                },
                Strength = 15,
                Weapons = new List<Weapon>
                {
                    new Weapon
                    {
                        Name = "Axe",
                        AttackBonus = 1,
                        Critical = "20",
                        DiceCount = 2,
                        DiceType = "d8"
                    }
                },
                Will = 8,
                Wisdom = 9
            };
        }
    }
}
