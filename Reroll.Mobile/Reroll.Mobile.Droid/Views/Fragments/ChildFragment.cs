using Android.OS;
using Android.Runtime;
using Android.Views;
using Reroll.Mobile.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Binding.Droid.BindingContext;

namespace Reroll.Mobile.Droid.Views.Fragments
{
    [Register("reroll.mobile.droid.views.fragments.ChildFragment")]
    public class ChildFragment : MvxFragment<ChildViewModel>
    {
        public ChildFragment()
        {
            this.RetainInstance = true;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(Resource.Layout.fragment_child, null);
        }
    }
}