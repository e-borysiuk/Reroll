using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using Reroll.Mobile.Core.ViewModels;
namespace Reroll.Mobile.Droid.Views
{
    [Activity(ScreenOrientation = ScreenOrientation.Portrait)]
    public class JoinRoomView : MvxAppCompatActivity<JoinRoomViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.join_room);
        }
    }
}
