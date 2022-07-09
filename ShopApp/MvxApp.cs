using Library.Data;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace ShopApp
{
    public class MvxApp : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.IoCProvider.RegisterType<ShopContext>();
        }
    }
}
