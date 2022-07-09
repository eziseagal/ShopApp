using Library.Data;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using ShopApp.ViewModels;

namespace ShopApp
{
    public class MvxApp : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.IoCProvider.RegisterType<ShopContext>();

            RegisterAppStart<MenuViewModel>();
        }
    }
}
