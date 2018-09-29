using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;

namespace Reroll.Mobile.Droid
{
    [Activity(Label = "Reroll.Mobile", MainLauncher = true, Icon = "@drawable/Icon", ClearTaskOnLaunch = true, NoHistory = true, Theme = "@style/RerollMobileTheme.SplashScreen", ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen() : base(Resource.Layout.page_splash_screen)
        {
        }
    }
}