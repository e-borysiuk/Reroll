using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views.InputMethods;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using Reroll.Mobile.Core.ViewModels;
namespace Reroll.Mobile.Droid.Views
{
    [Activity(ScreenOrientation = ScreenOrientation.Portrait , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.KeyboardHidden)]
    public class JoinRoomView : MvxAppCompatActivity<JoinRoomViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.join_room);
            var button = FindViewById<Button>(Resource.Id.btJoin);
            button.Click += delegate
            {
                var imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(button.WindowToken, 0);
            };
        }
    }
}
