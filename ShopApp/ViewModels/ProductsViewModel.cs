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
    public class ProductsViewModel : MvxViewModel
    {
        private readonly ShopContext _shopContext;
        private readonly IMvxNavigationService _navigationService;

        public IMvxCommand AddCategory { get; set; }
        public IMvxCommand Exit { get; set; }

        public ProductsViewModel(ShopContext shopContext, IMvxNavigationService navigationService)
        {
            _shopContext = shopContext;
            _navigationService = navigationService;
            Exit = new MvxCommand(GoBack);
            Products = new ObservableCollection<Product>(_shopContext.Products);
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
        get { return selectedCategoryID; }
        set
        {
            selectedCategoryID = value;
            RaisePropertyChanged(() => CanSelectSubcategories);
                if (selectedCategoryID >= 0)
                {
                    SubcategoryModels = new ObservableCollection<Subcategory>(_shopContext.Subcategories.Where(x => x.categoryId == CategoryModels[selectedCategoryID].Id));
                    Products = new ObservableCollection<Product>(_shopContext.Products.AsEnumerable().Where(x => SubcategoryModels.Select(z => z.Id).Contains(x.SubcategoryId)));
                    RaisePropertyChanged(() => Products);
                }
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
            if (selectedSubcategoryID >= 0)
            {
                Products = new ObservableCollection<Product>(_shopContext.Products.Where(x => x.SubcategoryId == SubcategoryModels[SelectedSubcategoryID].Id));
                RaisePropertyChanged(() => Products);
            }
        }
    }
    public bool CanSelectSubcategories => SelectedCategoryID >= 0;
    #endregion
    private ObservableCollection<Product> products;
        public ObservableCollection<Product> Products
        {
            get { return products; }
            set 
            {
                products = value; 
                RaisePropertyChanged(() => Products);
            }
        }

        public void GoBack()
        {
            _navigationService.Navigate<MenuViewModel>();
        }
    }
}
