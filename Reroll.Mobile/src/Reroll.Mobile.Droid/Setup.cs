using System.Collections.Generic;
using System.Reflection;
using MvvmCross.Droid.Support.V7.AppCompat;
using Reroll.Mobile.Core;

namespace Reroll.Mobile.Droid
{
    public class Setup : MvxAppCompatSetup<App>
    {
        protected override IEnumerable<Assembly> AndroidViewAssemblies => new List<Assembly>(base.AndroidViewAssemblies)
        {
            typeof(Android.Support.V7.Widget.Toolbar).Assembly,
            typeof(Android.Support.V4.View.ViewPager).Assembly,
            typeof(Android.Support.Design.Widget.TabLayout).Assembly
        };

        protected override void InitializeLastChance()
        {
            var errorHandler = new ErrorDisplayer(this.ApplicationContext);
            base.InitializeLastChance();
        }
    }
}
