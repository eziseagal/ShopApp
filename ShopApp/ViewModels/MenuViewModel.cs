using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace ShopApp.ViewModels
{
    public class MenuViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        public IMvxCommand ViewProducts { get; set; }
        public IMvxCommand AddCategory { get; set; }
        public IMvxCommand AddSubcategory { get; set; }
        public IMvxCommand AddProduct { get; set; }

        public MenuViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            ViewProducts = new MvxCommand(ProductView);
            AddCategory = new MvxCommand(AddCategoryView);
            AddSubcategory = new MvxCommand(AddSubcategoryView);
            AddProduct = new MvxCommand(AddProductView);
        }
        public async void ProductView() =>
            await _navigationService.Navigate<ProductsViewModel>();
        public async void AddCategoryView() =>
            await _navigationService.Navigate<AddCategoryViewModel>();
        public async void AddSubcategoryView() =>
            await _navigationService.Navigate<AddSubcategoryViewModel>();
        public async void AddProductView() =>
            await _navigationService.Navigate<AddProductViewModel>();
    }
}
