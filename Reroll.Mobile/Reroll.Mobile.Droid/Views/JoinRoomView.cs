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
    public class JoinRoomView : MvxCachingFragmentCompatActivity<JoinRoomViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.join_room);
        }
    }
}