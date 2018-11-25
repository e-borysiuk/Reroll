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
    [Register("reroll.mobile.droid.views.fragments.BaseStatsFragment")]
    public class BaseStatsFragment : MvxFragment<BaseStatsViewModel>
    {
        public BaseStatsFragment()
        {
            this.RetainInstance = true;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            var editText = View.FindViewById<EditText>(Resource.Id.et1);
            editText.TextChanged += EditText_TextChanged;
            editText = View.FindViewById<EditText>(Resource.Id.et2);
            editText.TextChanged += EditText_TextChanged;
            editText = View.FindViewById<EditText>(Resource.Id.et3);
            editText.TextChanged += EditText_TextChanged;
            editText = View.FindViewById<EditText>(Resource.Id.et4);
            editText.TextChanged += EditText_TextChanged;
            editText = View.FindViewById<EditText>(Resource.Id.et5);
            editText.TextChanged += EditText_TextChanged;
            editText = View.FindViewById<EditText>(Resource.Id.et6);
            editText.TextChanged += EditText_TextChanged;
            editText = View.FindViewById<EditText>(Resource.Id.et7);
            editText.TextChanged += EditText_TextChanged;
            editText = View.FindViewById<EditText>(Resource.Id.et8);
            editText.TextChanged += EditText_TextChanged;
            editText = View.FindViewById<EditText>(Resource.Id.et9);
            editText.TextChanged += EditText_TextChanged;
            editText = View.FindViewById<EditText>(Resource.Id.et10);
            editText.TextChanged += EditText_TextChanged;
            editText = View.FindViewById<EditText>(Resource.Id.et11);
            editText.TextChanged += EditText_TextChanged;
            editText = View.FindViewById<EditText>(Resource.Id.et12);
            editText.TextChanged += EditText_TextChanged;
            base.OnViewCreated(view, savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(Resource.Layout.base_stats_fragment, null);
        }

        private void EditText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (e.AfterCount == 0)
                return;
            EditText et = (EditText)sender;
            ViewModel.UpdateBaseStat(et.Tag.ToString(), e.Text.ToString());
        }
    }
}
