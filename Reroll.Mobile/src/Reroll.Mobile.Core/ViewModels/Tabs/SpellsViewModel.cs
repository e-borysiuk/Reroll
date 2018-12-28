using System.Collections.ObjectModel;
using MvvmCross.Commands;
using Reroll.Models;

namespace Reroll.Mobile.Core.ViewModels.Tabs
{
    public class SpellsViewModel : BaseViewModel
    {
        public SpellsViewModel(string name = "5") : base(name)
        {
        }

        public ObservableCollection<PreparedSpell> PreparedSpells =>
            new ObservableCollection<PreparedSpell>(this.Player.PreparedSpells);
        public ObservableCollection<Spell> LearnedSpells =>
            new ObservableCollection<Spell>(this.Player.LearnedSpells);
        
        public MvxCommand<PreparedSpell> DecreasePreparedSpellCommand =>
            new MvxCommand<PreparedSpell>((p) =>
            {
                if (p.CastQuantity > 0)
                {
                    Player updated = this.Player;
                    var index = updated.PreparedSpells.FindIndex(x => x == p);
                    updated.PreparedSpells[index] = new PreparedSpell()
                    {
                        CastQuantity = --p.CastQuantity,
                        Spell = new Spell
                        {
                            Level = 1,
                            Name = p.Spell.Name
                        }
                    };
                    this._signalrService.SendLog($"Edited decreased cast quantity on: {p.Spell.Name}");
                    this._dataRepository.SendUpdate(updated);
                }
            });

        public MvxCommand AddPreparedSpellCommand =>
            new MvxCommand(() =>
            {
                this._navigationService.Navigate<Dialogs.PreparedSpellViewModel>();
            });
        public MvxCommand AddLearnedSpellCommand =>
            new MvxCommand(() =>
            {
                this._navigationService.Navigate<Dialogs.LearnedSpellViewModel>();
            });

        public MvxCommand<PreparedSpell> EditPreparedSpellCommand =>
            new MvxCommand<PreparedSpell>((p) =>
            {
                this._navigationService.Navigate<Dialogs.PreparedSpellViewModel, PreparedSpell>(p);
            });
        public MvxCommand<Spell> EditLearnedSpellCommand =>
            new MvxCommand<Spell>((s) =>
            {
                this._navigationService.Navigate<Dialogs.LearnedSpellViewModel, Spell>(s);
            });
    }
}
