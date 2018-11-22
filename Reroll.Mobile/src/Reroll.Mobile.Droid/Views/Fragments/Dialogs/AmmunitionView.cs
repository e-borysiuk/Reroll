using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Reroll.Mobile.Core.ViewModels.Dialogs;

namespace Reroll.Mobile.Droid.Views.Fragments.Dialogs
{
    public class AmmunitionView : MvxDialogFragment<AmmunitionViewModel>
    {
        public override Dialog OnCreateDialog(Bundle savedState)
        {
            this.EnsureBindingContextIsSet();

            var view = this.BindingInflate(Resource.Layout.ammunition_dialog, null);

            var numPicker = view.FindViewById<NumberPicker>(Resource.Id.numberPicker);
            if (ViewModel.Quantity != 0)
                numPicker.Value = ViewModel.Quantity;
            numPicker.ValueChanged += NumPicker_ValueChanged;

            var dialog = new AlertDialog.Builder(Activity);
            dialog.SetTitle("Ammunition Dialog");
            dialog.SetView(view);
            if(ViewModel.IsEditMode)
                dialog.SetNeutralButton("Delete",
                    (s, a) => { ViewModel.DeleteCommand.Execute(); });
            dialog.SetNegativeButton("Cancel", (s, a) => { });
            dialog.SetPositiveButton("OK", (s, a) =>
                ViewModel.SaveCommand.Execute()
            );
            return dialog.Create();
        }

        private void NumPicker_ValueChanged(object sender, NumberPicker.ValueChangeEventArgs e)
        {
            ViewModel.SetQuantityValue(e.NewVal);
        }
    }
}
