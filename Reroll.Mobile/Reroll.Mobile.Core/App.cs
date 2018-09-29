using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.IoC;
using Reroll.Mobile.Core.ViewModels;

namespace Reroll.Mobile.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            this.CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            this.RegisterAppStart<JoinRoomViewModel>();
        }
    }
}