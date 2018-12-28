using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using MvvmCross.ViewModels;
using Reroll.Mobile.Core.ViewModels.Tabs;

namespace Reroll.Mobile.Core.ViewModels
{ 
    public class MainViewModel : BaseViewModel
    {

        public MainViewModel()
        {
            MyViewModels = new List<BaseViewModel>()
            {
                new BaseStatsViewModel("Base Stats"),
                new BelongingsViewModel("Items"),
                new SpellsViewModel("Spells"),
                new UtilityViewModel("Utility"),
                new NotesViewModel("Notes")
            };
        }

        private List<BaseViewModel> _myViewModels;
        public List<BaseViewModel> MyViewModels
        {
            get => _myViewModels;
            set
            {
                _myViewModels = value;
                RaisePropertyChanged(() => MyViewModels);
            }
        }
    }
}
