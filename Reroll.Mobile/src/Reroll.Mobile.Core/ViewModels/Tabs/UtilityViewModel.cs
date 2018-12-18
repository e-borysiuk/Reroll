using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Reroll.Mobile.Core.Models;
using Reroll.Models;

namespace Reroll.Mobile.Core.ViewModels.Tabs
{
    public class UtilityViewModel : BaseViewModel
    {
        public MvxObservableCollection<Roll> DiceRolls => this._dataRepository.DiceRolls;
        public string SelectedItem = "D8";

        public UtilityViewModel(string name = "3") : base(name)
        {

        }

        public MvxCommand RollD20Command =>
            new MvxCommand(() =>
            {
                var rnd = new Random();
                var d20 = rnd.Next(1, 20);
                this._signalrService.SendDiceRoll(d20, "D20");
            });
        public MvxCommand RollDiceCommand =>
            new MvxCommand(() =>
            {
                var rnd = new Random();
                int rollValue = 0;
                switch (SelectedItem)
                {
                    case "D4":
                        rollValue = rnd.Next(1, 4);
                        break;
                    case "D6":
                        rollValue = rnd.Next(1, 6);
                        break;
                    case "D8":
                        rollValue = rnd.Next(1, 8);
                        break;
                    case "D10":
                        rollValue = rnd.Next(1, 10);
                        break;
                    case "D12":
                        rollValue = rnd.Next(1, 12);
                        break;
                    case "D20":
                        rollValue = rnd.Next(1, 20);
                        break;
                }
                this._signalrService.SendDiceRoll(rollValue, SelectedItem);
            });
    }

    public class Dice
    {
        public Dice(string caption)
        {
            Caption = caption;
        }

        public string Caption { get; private set; }

        public override string ToString()
        {
            return Caption;
        }

        public override bool Equals(object obj)
        {
            var rhs = obj as Dice;
            if (rhs == null)
                return false;
            return rhs.Caption == Caption;
        }

        public override int GetHashCode()
        {
            if (Caption == null)
                return 0;
            return Caption.GetHashCode();
        }
    }
}
