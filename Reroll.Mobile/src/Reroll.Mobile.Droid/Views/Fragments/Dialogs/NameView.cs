using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Reroll.Mobile.Core.ViewModels.Dialogs;

namespace Reroll.Mobile.Droid.Views.Fragments.Dialogs
{
    public class NameView : MvxDialogFragment<NameViewModel>
    {
        public override Dialog OnCreateDialog(Bundle savedState)
        {
            this.EnsureBindingContextIsSet();

            var view = this.BindingInflate(Resource.Layout.name_dialog, null);

            var dialog = new AlertDialog.Builder(Activity);
            dialog.SetTitle("Name Dialog");
            dialog.SetView(view);
            dialog.SetNegativeButton("Cancel", (s, a) => { });
            dialog.SetPositiveButton("OK", (s, a) =>
                ViewModel.SaveCommand.Execute()
            );
            return dialog.Create();
        }
    }
}
