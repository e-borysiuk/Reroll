using MvvmCross.Commands;
using Reroll.Mobile.Core.Services;
using Reroll.Models;

namespace Reroll.Mobile.Core.ViewModels.Dialogs
{
    public class LearnedSpellViewModel : BaseViewModel<Spell>
    {
        Spell parameter;
        public string SpellName { get; set; }
        public int Level { get; set; }

        public LearnedSpellViewModel()
        {

        }
        public override void Prepare(Spell parameter)
        {
            this.IsEditMode = true;
            this.parameter = parameter;
            this.SpellName = parameter.Name;
            this.Level = parameter.Level;
        }

        public MvxCommand SaveCommand =>
            new MvxCommand(() =>
            {
                if(string.IsNullOrEmpty(SpellName))
                    NotificationService.ReportError("Spell Name cannot be empty");
                if (Level <= 0)
                    NotificationService.ReportError("Level cannot be less than 0");

                Player updated = this.Player;
                if (IsEditMode)
                    SaveEdit(ref updated);
                else
                    SaveNew(ref updated);
                this._dataRepository.SendUpdate(updated);
            });

        void SaveNew(ref Player updated)
        {
            updated.LearnedSpells.Add(new Spell
            {
                Level = Level,
                Name = SpellName
            });
        }

        void SaveEdit(ref Player updated)
        {
            var index = updated.LearnedSpells.FindIndex(x => x == parameter);
            updated.LearnedSpells[index] = new Spell
            {
                Level = Level,
                Name = SpellName
            };
        }

        public MvxCommand DeleteCommand =>
            new MvxCommand(() =>
            {
                var updated = this.Player;
                var index = updated.LearnedSpells.FindIndex(x => x == parameter);
                updated.LearnedSpells.RemoveAt(index);
                this._dataRepository.SendUpdate(updated);
            });
    }
}
