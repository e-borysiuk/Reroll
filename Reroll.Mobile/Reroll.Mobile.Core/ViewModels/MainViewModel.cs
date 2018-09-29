using MvvmCross.Core.ViewModels;
using System.Collections.Generic;

namespace Reroll.Mobile.Core.ViewModels
{ 
    public class MainViewModel : MvxViewModel
    {
        public MainViewModel()
        {
            MyViewModels = new List<ChildViewModel>()
            {
                new ChildViewModel("First"),
                new ChildViewModel("Second"),
                new ChildViewModel("Third")
            };
        }

        private List<ChildViewModel> _myViewModels;
        public List<ChildViewModel> MyViewModels
        {
            get { return _myViewModels; }
            set
            {
                _myViewModels = value;
                RaisePropertyChanged(() => MyViewModels);
            }
        }
    }
}