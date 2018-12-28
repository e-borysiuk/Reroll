using MvvmCross.Plugin.Messenger;
using Reroll.Models;

namespace Reroll.Mobile.Core.Models.MvxMessages
{
    public class UpdateMessage : MvxMessage
    {
        public UpdateMessage(object sender) : base(sender)
        {
        }

        public UpdateMessage(object sender, Player player, string message) : base(sender)
        {
            Player = player;
            Message = message;
        }

        public Player Player
        {
            get;
            private set;
        }

        public string Message { get; private set; }
    }
}
