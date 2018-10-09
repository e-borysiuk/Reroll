using MvvmCross.Plugin.Messenger;

namespace Reroll.Mobile.Core.Models.MvxMessages
{
    public class NewMessage : MvxMessage
    {
        public NewMessage(object sender) : base(sender)
        {

        }

        public NewMessage(object sender, string user, string message) : base(sender)
        {
            this.Message = message;
            this.User = user;
        }

        public string Message { get; set; }
        public string User { get; set; }
    }
}
