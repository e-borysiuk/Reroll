using MvvmCross.Plugin.Messenger;
using Reroll.Models;

namespace Reroll.Mobile.Core.Models.MvxMessages
{
    public class UpdateMessage : MvxMessage
    {
        public UpdateMessage(object sender) : base(sender)
        {
        }

        public UpdateMessage(object sender, PlayerModel playerModel) : base(sender)
        {
            PlayerModel = playerModel;
        }

        public PlayerModel PlayerModel
        {
            get;
            private set;
        }
    }
}
