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
    public class AddProductViewModel : MvxViewModel
    {
        private readonly ShopContext _shopContext;
        private readonly IMvxNavigationService _navigationService;

        public IMvxCommand AddProduct { get; set; }
        public IMvxCommand Exit { get; set; }

        public AddProductViewModel(ShopContext shopContext, IMvxNavigationService navigationService)
        {
            _shopContext = shopContext;
            _navigationService = navigationService;
            Exit = new MvxCommand(GoBack);
            AddProduct = new MvxCommand(AddNewProduct);
            CategoryModels = new ObservableCollection<Category>(_shopContext.Categories);
        }
        #region category
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
        private int selectedCategoryID;
        public int SelectedCategoryID 
        {
            get { return selectedCategoryID;}
            set
            {
                selectedCategoryID = value;
                RaisePropertyChanged(() => CanSelectSubcategories);
                if (selectedCategoryID >= 0)
                    SubcategoryModels = new ObservableCollection<Subcategory>(_shopContext.Subcategories.Where(x => x.categoryId == CategoryModels[selectedCategoryID].Id));
            }
        }
        #endregion
        #region subcategory
        private ObservableCollection<Subcategory> subcategoryModels;

        public ObservableCollection<Subcategory> SubcategoryModels
        {
            get { return subcategoryModels; }
            set
            {
                SetProperty(ref subcategoryModels, value);
                Subcategories = new ObservableCollection<string>(SubcategoryModels.Select(x => x.Name));
                RaisePropertyChanged(() => Subcategories);
            }
        }
        public ObservableCollection<string> Subcategories { get; set; }
        private int selectedSubcategoryID;
        public int SelectedSubcategoryID 
        {
            get
            {
                return selectedSubcategoryID;
            }
            set
            {
                selectedSubcategoryID = value;
            }
        }
        public bool CanSelectSubcategories => SelectedCategoryID >= 0;
        #endregion
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                this.RaisePropertyChanged(() => CanAdd);
            }
        }
        private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                this.RaisePropertyChanged(() => CanAdd);
            }
        }
        private decimal price;

        public decimal Price
        {
            get { return price; }
            set
            {
                price = value;
                this.RaisePropertyChanged(() => CanAdd);
            }
        }
        public bool CanAdd => !string.IsNullOrEmpty(Name) && selectedSubcategoryID != -1;
        public void AddNewProduct()
        {
            _shopContext.Products.Add(new Product()
            {
                Name = Name,
                Price = this.Price,
                Quantity = this.Quantity,
                SubcategoryId = this.SubcategoryModels[selectedSubcategoryID].Id
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
