using Library.Data;
using Library.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.ViewModels
{
    public class AddCategoryViewModel : MvxViewModel
    {
        private readonly ShopContext _shopContext;
        private readonly IMvxNavigationService _navigationService;

        public IMvxCommand AddCategory { get; set; }
        public IMvxCommand Exit { get; set; }

        public AddCategoryViewModel(ShopContext shopContext, IMvxNavigationService navigationService)
        {
            _shopContext = shopContext;
            _navigationService = navigationService;
            AddCategory = new MvxCommand(AddNewCategory);
            Exit = new MvxCommand(GoBack);
        }
        private string categoryName;

        public string CategoryName
        {
            get { return categoryName; }
            set 
            {
                categoryName = value;
                RaisePropertyChanged(() => CanAdd);
            }
        }
        public bool CanAdd => !string.IsNullOrEmpty(CategoryName);

        public void AddNewCategory()
        {
            _shopContext.Categories.Add(new Category()
            {
                Name = CategoryName
            });
            _shopContext.SaveChanges();
            GoBack();
        }
        public void GoBack()
        {
            _navigationService.Navigate<MenuViewModel>();
        }

    }
}
