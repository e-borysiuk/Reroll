using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Reroll.Mobile.Core.Models;
using Reroll.Models;

namespace Reroll.Mobile.Core.ViewModels.Tabs
{
    public class NotesViewModel : BaseViewModel
    {
        public NotesViewModel(string name = "4") : base(name)
        {

        }

        public override void ViewDisappearing()
        {
            var updated = this.Player;
            updated.Notes = this.Player.Notes;
            this._dataRepository.SendUpdate(updated);
            base.ViewDisappearing();
        }
    }
}
