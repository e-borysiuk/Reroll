using MvvmCross.Commands;
using Reroll.Mobile.Core.Services;
using Reroll.Models;

namespace Reroll.Mobile.Core.ViewModels.Dialogs
{
    public class LearnedSpellViewModel : ChildViewModel
    {
        public string SpellName { get; set; }
        public int Level { get; set; }

        public LearnedSpellViewModel()
        {

        }

        public MvxCommand SaveCommand =>
            new MvxCommand(() =>
            {
                if(string.IsNullOrEmpty(Name))
                    NotificationService.ReportError("Spell Name cannot be empty");
                if (Level <= 0)
                    NotificationService.ReportError("Level cannot be less than 0");

                Player updated = this.Player;
                updated.LearnedSpells.Add(new Spell
                {
                    Level = Level,
                    Name = SpellName
                });
                this._dataRepository.SendUpdate(updated);
            });
    }
}
