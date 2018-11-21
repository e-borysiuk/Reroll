using MvvmCross.Commands;
using Reroll.Mobile.Core.Services;
using Reroll.Models;

namespace Reroll.Mobile.Core.ViewModels.Dialogs
{
    public class PreparedSpellViewModel : BaseViewModel<PreparedSpell>
    {
        public string SpellName { get; set; }
        public int CastQuantity { get; set; }

        public PreparedSpellViewModel()
        {

        }

        public override void Prepare(PreparedSpell parameter)
        {
            this.SpellName = parameter.Spell.Name;
            this.CastQuantity = parameter.CastQuantity;
        }

        public MvxCommand SaveCommand =>
            new MvxCommand(() =>
            {
                if(string.IsNullOrEmpty(SpellName))
                    NotificationService.ReportError("Spell Name cannot be empty");
                if (CastQuantity <= 0)
                    NotificationService.ReportError("Cast Quantity cannot be less than 0");

                Player updated = this.Player;
                updated.PreparedSpells.Add(new PreparedSpell
                {
                    CastQuantity = CastQuantity,
                    Spell = new Spell
                    {
                        Level = 1,
                        Name = SpellName
                    }
                });
                this._dataRepository.SendUpdate(updated);
            });

    }
}
