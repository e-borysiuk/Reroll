using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using MvvmCross.ViewModels;

namespace Reroll.Mobile.Core.ViewModels
{ 
    public class MainViewModel : BaseViewModel
    {

        public MainViewModel()
        {
            List<string> messagesList;

            MyViewModels = new List<ChildViewModel>()
            {
                new ChildViewModel("First"),
                new ChildViewModel("Second"),
                new ChildViewModel("Third")
            };



            //messagesList = new List<string>();

            //this._connection.On<string, string>("sendToAll", (user, message) =>
            //{
            //    var newMessage = $"{user}: {message}";
            //    messagesList.Add(newMessage);
            //});

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
