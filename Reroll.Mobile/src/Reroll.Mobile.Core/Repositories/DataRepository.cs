using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MvvmCross;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using Reroll.Mobile.Core.Interfaces;
using Reroll.Mobile.Core.Models;
using Reroll.Mobile.Core.Models.MvxMessages;
using Reroll.Mobile.Core.Services;
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
            this._diceToken = this._messenger.Subscribe<DiceMessage>(ReceiveDiceMessage);
            this.DiceRolls = new MvxObservableCollection<Roll>();
        }

        public void ReceiveDiceMessage(DiceMessage obj)
        {
            this.DiceRolls.Insert(0, new Roll { Value = obj.Message});
        }

        private readonly IMvxMessenger _messenger;
        private readonly ISignalrService _signalrService;
        private readonly MvxSubscriptionToken _messageToken;
        private readonly MvxSubscriptionToken _diceToken;

        private Player _Player;

        public MvxObservableCollection<Roll> DiceRolls { get; set; }

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
            NotificationService.ReportSuccess($"GM updated your {obj.Message}");
        }
        
        public void RefreshUi()
        {
            this._messenger.Publish(new RefreshMessage(this));
        }

        public Player CreateSampleModel()
        {
            return new Player
            {
                Notes = "Notes",
                AmmunitionList = new List<Ammunition>
                {
                    new Ammunition
                    {
                        Name = "Arrows",
                        Quantity = 12
                    }
                },
                ArmorClass = 11,
                BaseAttackBonus = 2,
                Charisma = 12,
                Constitution = 13,
                ConnectionId = "",
                Copper = 3,
                Dexterity = 15,
                ExperiencePoints = 120,
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
                        Critical = "20",
                        Damage = "d8"
                    }
                },
                Will = 8,
                Wisdom = 9
            };
        }
    }
}
