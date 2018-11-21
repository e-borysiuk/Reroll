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
