using Library.Data;
using Library.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.ViewModels
{
    public class AddSubcategoryViewModel : MvxViewModel
    {
        private readonly ShopContext _shopContext;
        private readonly IMvxNavigationService _navigationService;

        public IMvxCommand AddSubcategory { get; set; }
        public IMvxCommand Exit { get; set; }

        public AddSubcategoryViewModel(ShopContext shopContext, IMvxNavigationService navigationService)
        {
            _shopContext = shopContext;
            _navigationService = navigationService;
            Exit = new MvxCommand(GoBack);
            AddSubcategory = new MvxCommand(AddNewSubcategory);
            CategoryModels = new ObservableCollection<Category>(_shopContext.Categories);
        }
        private string subcategoryName;

        public string SubcategoryName
        {
            get { return subcategoryName; }
            set
            {
                subcategoryName = value;
                RaisePropertyChanged(() => CanAdd);
            }
        }

        private ObservableCollection<Category> categoryModels;

        public ObservableCollection<Category> CategoryModels
        {
            get { return categoryModels; }
            set 
            {
                SetProperty(ref categoryModels, value);
                RaisePropertyChanged(() => Categories);
            }
        }
        public ObservableCollection<string> Categories =>
            new ObservableCollection<string>(categoryModels.Select(x => x.Name));
        public int SelectedID { get; set; }
        public bool CanAdd => SelectedID != -1 && !string.IsNullOrEmpty(SubcategoryName);
        public void AddNewSubcategory()
        {
            _shopContext.Subcategories.Add(new Subcategory()
            {
                categoryId = CategoryModels[SelectedID].Id,
                Name = SubcategoryName
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
