using System.Collections.Generic;
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

        public JoinResponseMessage(object sender, ResponseStatusEnum response, List<string> playerNames) : base(sender)
        {
            Response = response;
            PlayerNames = playerNames;
        }

        public ResponseStatusEnum Response
        {
            get;
            private set;
        }

        public List<string> PlayerNames
        {
            get;
            private set;
        }
    }
}
