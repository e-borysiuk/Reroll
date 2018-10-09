using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Reroll.Mobile.Core.ViewModels;

namespace Reroll.Mobile.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<JoinRoomViewModel>();
        }
    }
}
