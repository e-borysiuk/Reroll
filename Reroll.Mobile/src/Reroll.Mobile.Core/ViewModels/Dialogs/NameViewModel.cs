using MvvmCross.Commands;
using Reroll.Mobile.Core.Services;
using Reroll.Models;

namespace Reroll.Mobile.Core.ViewModels.Dialogs
{
    public class NameViewModel : BaseViewModelResult<string>
    {
        public string PlayerName { get; set; }

        public NameViewModel()
        {

        }

        public MvxAsyncCommand SaveCommand =>
            new MvxAsyncCommand(async () =>
            {
                if (string.IsNullOrEmpty(PlayerName))
                {
                    NotificationService.ReportError("Name cannot be empty");
                    return;
                }

                await _navigationService.Close(this, this.PlayerName);
            });
    }
}
