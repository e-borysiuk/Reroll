using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using Reroll.Mobile.Core.ViewModels;
using Reroll.Mobile.Droid.Views.Fragments;
using System.Collections.Generic;

namespace Reroll.Mobile.Droid.Views
{
    [Activity(ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainView : MvxAppCompatActivity<MainViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.page_main);

            var toolbar = FindViewById<Toolbar>(Resource.Id.main_toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(false);
            SupportActionBar.Title = "Reroll.Mobile";

            var viewPager = FindViewById<ViewPager>(Resource.Id.main_view_pager);
            var fragments = InitializeFragments();

            //foreach (var myViewModel in ViewModel.MyViewModels)
            //    fragments.Add(new MvxViewPagerFragmentInfo(myViewModel.Name, typeof(ChildFragment), myViewModel));

            viewPager.Adapter = new MvxCachingFragmentStatePagerAdapter(this, SupportFragmentManager, fragments);

            //If you want to start at specific tab
            //viewPager.SetCurrentItem(ViewModel.CurrentPage, false);

            var tabLayout = FindViewById<TabLayout>(Resource.Id.main_tablayout);
            tabLayout.SetupWithViewPager(viewPager);
        }

        private List<MvxViewPagerFragmentInfo> InitializeFragments()
        {
            return new List<MvxViewPagerFragmentInfo>
            {
                new MvxViewPagerFragmentInfo(ViewModel.MyViewModels[0].Name, typeof(BaseStatsFragment),
                    ViewModel.MyViewModels[0]),
                new MvxViewPagerFragmentInfo(ViewModel.MyViewModels[1].Name, typeof(BelongingsFragment),
                    ViewModel.MyViewModels[1]),
                new MvxViewPagerFragmentInfo(ViewModel.MyViewModels[2].Name, typeof(SpellsFragment),
                    ViewModel.MyViewModels[2]),
                new MvxViewPagerFragmentInfo(ViewModel.MyViewModels[3].Name, typeof(DiceRollFragment),
                    ViewModel.MyViewModels[3]),
                new MvxViewPagerFragmentInfo(ViewModel.MyViewModels[4].Name, typeof(ImagesFragment),
                    ViewModel.MyViewModels[4]),
            };
        }
    }
}
