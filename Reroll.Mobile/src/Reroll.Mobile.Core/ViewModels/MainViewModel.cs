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
            MyViewModels = new List<ChildViewModel>()
            {
                new BaseStatsViewModel("Base Stats"),
                new BelongingsViewModel("Items"),
                new SpellsViewModel("Spells"),
                new UtilityViewModel("Utility")
            };
        }

        private List<ChildViewModel> _myViewModels;
        public List<ChildViewModel> MyViewModels
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
