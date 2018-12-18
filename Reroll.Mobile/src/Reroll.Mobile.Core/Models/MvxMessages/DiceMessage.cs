using MvvmCross.Plugin.Messenger;

namespace Reroll.Mobile.Core.Models.MvxMessages
{
    public class DiceMessage : MvxMessage
    {
        public DiceMessage(object sender) : base(sender)
        {

        }

        public DiceMessage(object sender, string message) : base(sender)
        {
            this.Message = message;
        }

        public string Message { get; set; }
    }
}
