using System;
using System.Collections.Generic;
using MvvmCross.Commands;
using MvvmCross.Plugin.Messenger;
using Reroll.Mobile.Core.Models.MvxMessages;
using Reroll.Models;
using Reroll.Models.Enums;

namespace Reroll.Mobile.Core.ViewModels.Tabs
{
    public class BaseStatsViewModel : ChildViewModel
    {
        public BaseStatsViewModel(string name = "1") : base(name)
        {
            this._messageToken = this._messenger.Subscribe<UpdateMessage>(ReceivedUpdate);
        }

        private IMvxCommand _goToChildCommand;
        private IMvxCommand _incrementValueCommand;
        private IMvxCommand _decrementValueCommand;
        private readonly MvxSubscriptionToken _messageToken;

        private PlayerModel _playerModel;
        public PlayerModel PlayerModel
        {
            get => this._dataRepository.PlayerModel;
            set
            {
                _playerModel = value;
                RaisePropertyChanged(() => PlayerModel);
            }
        }
        
        public string StatValue => PlayerModel.Charisma.ToString();

        void ReceivedUpdate(UpdateMessage obj)
        {
            this.PlayerModel = obj.PlayerModel;
        }


        public IMvxCommand GoToChildCommand
        {
            get
            {
                _goToChildCommand = _goToChildCommand ?? new MvxCommand(() =>
                {
                    this.PlayerModel = CreateSampleModel();
                    this._signalrService.SendUpdate(PlayerModel);
                });
                return _goToChildCommand;
            }
        }

        public IMvxCommand IncrementValueCommand
        {
            get
            {
                _incrementValueCommand = _incrementValueCommand ?? new MvxCommand(() =>
                {
                    PlayerModel.Charisma++;
                    this._signalrService.SendUpdate(PlayerModel);
                });
                return _incrementValueCommand;
            }
        }

        public IMvxCommand DecrementValueCommand
        {
            get
            {
                _decrementValueCommand = _decrementValueCommand ?? new MvxCommand(() =>
                {
                    PlayerModel.Charisma--;
                    this._signalrService.SendUpdate(PlayerModel);
                });
                return _decrementValueCommand;
            }
        }

        private PlayerModel CreateSampleModel()
        {
            return new PlayerModel
            {
                AmmunitionList = new List<Tuple<string, int>>
                {
                    new Tuple<string, int>("Arrows", 12)
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
                Feats = new List<Tuple<string, string>>
                {
                    new Tuple<string, string>("Feat1", "description")
                },
                Fortitude = 10,
                Gold = 1,
                HealthPoints = 40,
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
                KnownSpells = new List<Spell>
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
                PreparedSpells = new List<Tuple<Spell, int>>
                {
                    new Tuple<Spell, int>(new Spell {Name = "Missles", Level = 1}, 2)
                },
                Reflex = 11,
                Silver = 2,
                Skills = new List<SkillModel>
                {
                    new SkillModel
                    {
                        Name = "Riding",
                        KeyAbility = KeyAbilityEnum.Dex,
                        SkillModifier = 5
                    }
                },
                SpecialAbilities = new List<Tuple<string, string>>
                {
                    new Tuple<string, string>("Strong attack", "Hits strong")
                },
                State = new List<Tuple<string, string>>
                {
                    new Tuple<string, string>("Sleepy", "zzz")
                },
                Strength = 15,
                Weapons = new List<WeaponModel>
                {
                    new WeaponModel
                    {
                        Name = "Axe",
                        AttackBonus = 1,
                        Critical = "20",
                        DiceCount = 2,
                        DiceType = DiceTypeEnum.D8,
                        IsRanged = false,
                        NotesList = new List<string>
                        {
                            "Really beautiful"
                        }
                    }
                },
                Will = 8,
                Wisdom = 9
            };
        }
    }
}