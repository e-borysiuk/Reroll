using MvvmCross.Plugin.Messenger;
using Reroll.Models;

namespace Reroll.Mobile.Core.Models.MvxMessages
{
    public class UpdateMessage : MvxMessage
    {
        public UpdateMessage(object sender) : base(sender)
        {
        }

        public UpdateMessage(object sender, Player player) : base(sender)
        {
            Player = player;
        }

        public Player Player
        {
            get;
            private set;
        }
    }
}
