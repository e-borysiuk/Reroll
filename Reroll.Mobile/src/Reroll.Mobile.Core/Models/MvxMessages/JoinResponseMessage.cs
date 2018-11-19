using MvvmCross.Plugin.Messenger;
using Reroll.Models;
using Reroll.Models.Enums;

namespace Reroll.Mobile.Core.Models.MvxMessages
{
    public class JoinResponseMessage : MvxMessage 
    {
        public JoinResponseMessage(object sender) : base(sender)
        {
        }

        public JoinResponseMessage(object sender, ResponseStatusEnum response) : base(sender)
        {
            Response = response;
        }

        public ResponseStatusEnum Response
        {
            get;
            private set;
        }
    }
}
