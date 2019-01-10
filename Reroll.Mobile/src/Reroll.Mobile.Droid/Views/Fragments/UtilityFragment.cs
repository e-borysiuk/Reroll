using System;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Reroll.Mobile.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Reroll.Mobile.Core.ViewModels.Tabs;

namespace Reroll.Mobile.Droid.Views.Fragments
{
    [Register("reroll.mobile.droid.views.fragments.UtilityFragment")]
    public class UtilityFragment : MvxFragment<UtilityViewModel>
    {
        public UtilityFragment()
        {
            this.RetainInstance = true;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            return this.BindingInflate(Resource.Layout.utility_fragment, null);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            Spinner spinner = view.FindViewById<Spinner>(Resource.Id.spinner);

            spinner.ItemSelected += Spinner_ItemSelected;
            var adapter = ArrayAdapter.CreateFromResource(
                Context, Resource.Array.dice, Resource.Layout.spinner_item);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
            spinner.SetSelection(2);
            base.OnViewCreated(view, savedInstanceState);
        }
        
        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            ViewModel.SelectedItem = spinner.GetItemAtPosition(e.Position).ToString();
            if (ViewModel.FirstRun)
            {
                ViewModel.FirstRun= false;
                return;
            }
            ViewModel.RollDiceCommand.Execute();
        }
    }
}
