using MvvmCross.Commands;
using Reroll.Mobile.Core.Services;
using Reroll.Models;

namespace Reroll.Mobile.Core.ViewModels.Dialogs
{
    public class PreparedSpellViewModel : BaseViewModel<PreparedSpell>
    {
        PreparedSpell parameter;
        public string SpellName { get; set; }
        public int CastQuantity { get; set; }

        public PreparedSpellViewModel()
        {

        }

        public override void Prepare(PreparedSpell parameter)
        {
            this.IsEditMode = true;
            this.parameter = parameter;
            this.SpellName = parameter.Spell.Name;
            this.CastQuantity = parameter.CastQuantity;
        }

        public MvxCommand SaveCommand =>
            new MvxCommand(() =>
            {
                if (string.IsNullOrEmpty(SpellName))
                {
                    NotificationService.ReportError("Spell Name cannot be empty");
                    return;
                }

                if (CastQuantity <= 0)
                {
                    NotificationService.ReportError("Cast Quantity cannot be less than 0");
                    return;
                }

                Player updated = this.Player;
                if (IsEditMode)
                    SaveEdit(ref updated);
                else
                    SaveNew(ref updated);
                if(IsEditMode)
                    this._signalrService.SendLog($"Edited spell: {parameter.Spell.Name}");
                else
                    this._signalrService.SendLog($"Created spell: {SpellName}");
                this._dataRepository.SendUpdate(updated);
            });

        void SaveNew(ref Player updated)
        {
            updated.PreparedSpells.Add(new PreparedSpell
            {
                CastQuantity = CastQuantity,
                Spell = new Spell
                {
                    Level = 1,
                    Name = SpellName
                }
            });
        }

        void SaveEdit(ref Player updated)
        {
            var index = updated.PreparedSpells.FindIndex(x => x == parameter);
            updated.PreparedSpells[index] = new PreparedSpell()
            {
                CastQuantity = CastQuantity,
                Spell = new Spell
                {
                    Level = 1,
                    Name = SpellName
                }
            };
        }

        public void SetCastQuantityValue(int eNewVal)
        {
            CastQuantity = eNewVal;
        }

        public MvxCommand DeleteCommand =>
            new MvxCommand(() =>
            {
                var updated = this.Player;
                var index = updated.PreparedSpells .FindIndex(x => x == parameter);
                updated.PreparedSpells.RemoveAt(index);
                this._signalrService.SendLog($"Deleted spell: {parameter.Spell.Name}");
                this._dataRepository.SendUpdate(updated);
            });
    }
}
