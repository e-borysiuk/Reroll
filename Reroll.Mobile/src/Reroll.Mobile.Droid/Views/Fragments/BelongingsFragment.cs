using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Reroll.Mobile.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Reroll.Mobile.Core.ViewModels.Tabs;

namespace Reroll.Mobile.Droid.Views.Fragments
{
    [Register("reroll.mobile.droid.views.fragments.BelongingsFragment")]
    public class BelongingsFragment : MvxFragment<BelongingsViewModel>
    {
        public BelongingsFragment()
        {
            this.RetainInstance = true;
        }

        private bool _changedFlag;

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            var editText = View.FindViewById<EditText>(Resource.Id.et1);
            editText.EditorAction += EditText_EditorAction;
            editText.FocusChange += EditText_FocusChange;
            editText.TextChanged += EditText_TextChanged;
            editText = View.FindViewById<EditText>(Resource.Id.et2);
            editText.EditorAction += EditText_EditorAction;
            editText.FocusChange += EditText_FocusChange;
            editText.TextChanged += EditText_TextChanged;
            editText = View.FindViewById<EditText>(Resource.Id.et3);
            editText.EditorAction += EditText_EditorAction;
            editText.FocusChange += EditText_FocusChange;
            editText.TextChanged += EditText_TextChanged;
            editText = View.FindViewById<EditText>(Resource.Id.et4);
            editText.EditorAction += EditText_EditorAction;
            editText.FocusChange += EditText_FocusChange;
            editText.TextChanged += EditText_TextChanged;
            base.OnViewCreated(view, savedInstanceState);
        }

        private void EditText_EditorAction(object sender, TextView.EditorActionEventArgs e)
        {
            if (e.ActionId == ImeAction.Done)
            {
                EditText et = (EditText)sender;
                et.ClearFocus();
                var imm = (InputMethodManager)Activity.GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(et.WindowToken, 0);
            }
        }

        private void EditText_FocusChange(object sender, View.FocusChangeEventArgs e)
        {
            if (!e.HasFocus)
            {
                if (_changedFlag)
                {
                    EditText et = (EditText)sender;
                    ViewModel.UpdateBaseStat(et.Tag.ToString(), et.Text);
                }
            }
            else
            {
                _changedFlag = false;
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(Resource.Layout.belongings_fragment, null);
        }

        private void EditText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (e.AfterCount == 0)
                return;
            this._changedFlag = true;
        }
    }
}
