using Android.OS;
using Android.Runtime;
using Android.Views;
using Reroll.Mobile.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Reroll.Mobile.Core.ViewModels.Tabs;

namespace Reroll.Mobile.Droid.Views.Fragments
{
    [Register("reroll.mobile.droid.views.fragments.SpellsFragment")]
    public class SpellsFragment : MvxFragment<SpellsViewModel>
    {

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(Resource.Layout.spells_fragment, null);
        }
    }
}
