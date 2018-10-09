using MvvmCross.Plugin.Messenger;
using Reroll.Web.Models.Enums;

namespace Reroll.Mobile.Core.Models.MvxMessages
{
    public class JoinResponseMessage : MvxMessage 
    {
        public JoinResponseMessage(object sender) : base(sender)
        {
        }

        public JoinResponseMessage(object sender, ResponseStatusEnum response) : base(sender)
        {
            this.Response = response;
        }

        public ResponseStatusEnum Response
        {
            get;
            private set;
        }
    }
}
