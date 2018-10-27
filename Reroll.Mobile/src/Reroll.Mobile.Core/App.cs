using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Reroll.Mobile.Core.Models;
using Reroll.Mobile.Core.Services.Interfaces;
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

            var errorApplicationObject = new ErrorApplicationObject();
            Mvx.RegisterSingleton<IErrorReporter>(errorApplicationObject);
            Mvx.RegisterSingleton<IErrorSource>(errorApplicationObject);

            RegisterAppStart<JoinRoomViewModel>();
        }
    }
}
