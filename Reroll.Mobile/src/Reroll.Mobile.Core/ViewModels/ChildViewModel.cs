using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.AspNetCore.SignalR.Client;
using MvvmCross.Commands;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using Reroll.Mobile.Core.Models.MvxMessages;
using Reroll.Models;
using Reroll.Models.Enums;

namespace Reroll.Mobile.Core.ViewModels
{
    public class ChildViewModel : BaseViewModel
    {
        public ChildViewModel(string name = "default")
        {
            this.messageToken = this._messenger.Subscribe<NewMessage>(NewMessageArrived);
            this._signalrService.SendMessage("joined");
            Name = "";
        }

        void NewMessageArrived(NewMessage obj)
        {
            this.Name += obj.User + ": " + obj.Message + "\n";
        }

        public string Message { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }
        private IMvxCommand _goToChildCommand;
        MvxSubscriptionToken messageToken;
        bool flag;

        public IMvxCommand GoToChildCommand
        {
            get
            {
                _goToChildCommand = _goToChildCommand ?? new MvxCommand(() =>
                {
                    if(!flag)
                        this._signalrService.SendUpdate(CreateSampleModel());
                    else
                    {
                        var s = CreateSampleModel();
                        var rand = new Random();
                        s.Charisma = rand.Next(1, 66);
                        this._signalrService.SendUpdate(s);
                    }

                    flag = true;
                    //this._signalrService.SendMessage(this.Message);
                });
                return _goToChildCommand;
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
