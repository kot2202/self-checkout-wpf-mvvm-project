using Self_checkout.WpfApp.Commands.CheckoutView;
using Self_checkout.WpfApp.Commands.General;
using Self_checkout.WpfApp.DAL; // TODO REMOVE WHEN DONE TESTING
using Self_checkout.WpfApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Self_checkout.WpfApp.ViewModels
{
    public class CheckoutViewModel : ViewModelBase
    {
        private const int _maxScreenLength = 13; // 13 digits for EAN-13
        public int MaxScreenLength
        {
            get { return _maxScreenLength; }
        }

        private string _screenValue = "";
        public string ScreenValue
        {
            get { return _screenValue; }
            set {
                _screenValue = value;
                OnPropertyChanged();
                AddNumberCommand.OnCanExecuteChanged();
                UndoNumberCommand.OnCanExecuteChanged();
                ClearCommand.OnCanExecuteChanged();
                AddProductCommand.OnCanExecuteChanged();
                }
        }

        private bool _isPopupVisible = false;

        public bool IsPopupVisible
        {
            get { return _isPopupVisible; }
            set { _isPopupVisible = value;
                OnPropertyChanged();
            }
        }

        private int _popupMenuSelectedIndex = 0;

        public int PopupMenuSelectedIndex
        {
            get { return _popupMenuSelectedIndex; }
            set { _popupMenuSelectedIndex = value; OnPropertyChanged(); }
        }


        private decimal _priceSum = 0;

        public decimal PriceSum
        {
            get { return _priceSum; }
            set
            {
                _priceSum =  value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ProductModel> _listOfFruits = new ObservableCollection<ProductModel>();
        public ObservableCollection<ProductModel> ListOfFruits
        {
            get { return _listOfFruits; }
            set { _listOfFruits = value; }
        }
        private ObservableCollection<ProductModel> _listOfVegetables = new ObservableCollection<ProductModel>();
        public ObservableCollection<ProductModel> ListOfVegetables
        {
            get { return _listOfVegetables; }
            set { _listOfVegetables = value; }
        }
        private ObservableCollection<ProductModel> _listOfBread = new ObservableCollection<ProductModel>();
        public ObservableCollection<ProductModel> ListOfBread
        {
            get { return _listOfBread; }
            set { _listOfBread = value; }
        }
        private ObservableCollection<ProductModel> _listOfOther = new ObservableCollection<ProductModel>();
        public ObservableCollection<ProductModel> ListOfOther
        {
            get { return _listOfOther; }
            set { _listOfOther = value; }
        }

        private ObservableCollection<ProductModel> _listOfProducts = new ObservableCollection<ProductModel>();
        public ObservableCollection<ProductModel> ListOfProducts
        {
            get { return _listOfProducts; }
            set { _listOfProducts = value; }
        }

        public void RecalculatePriceSum()
        {
            decimal price = 0;
            foreach(ProductModel product in _listOfProducts)
            {
                price += product.CalculatedQuantity * product.product_price;
            }
            PriceSum = price;
        }

        // Items that need to update things need to be class type
        public ICommand NavigatePostPurchaseCommand { get;}
        public ICommand TogglePopupMenuCommand { get;}
        public ICommand InsertItemFromPopupCommand { get; }
        public ICommand SetPopupSelectedIndexCommand { get; }
        public ICommand FinalizeOrderCommand { get; }

        public AddNumberCommand AddNumberCommand { get;}
        public UndoNumberCommand UndoNumberCommand { get; }
        public ClearCommand ClearCommand { get; }
        public AddProductCommand AddProductCommand { get; }

        public CheckoutViewModel(Stores.NavigationStore navigationStore)
        {
            NavigatePostPurchaseCommand = new NavigateCommand<PostPurchaseViewModel>(navigationStore, () => new PostPurchaseViewModel(navigationStore));
            TogglePopupMenuCommand = new TogglePopupMenuCommand(this);
            InsertItemFromPopupCommand = new InsertItemFromPopupCommand(this);
            SetPopupSelectedIndexCommand = new SetPopupSelectedIndexCommand(this);
            FinalizeOrderCommand = new FinalizeOrderCommand(this);

            AddNumberCommand = new AddNumberCommand(this);
            UndoNumberCommand = new UndoNumberCommand(this);
            ClearCommand = new ClearCommand(this);
            AddProductCommand = new AddProductCommand(this);

            // Could be done only once on app starting but it depends on how often prices may get updated.
            LoadPopupProducts();
        }

        public async void LoadPopupProducts()
        {
            await Task.Factory.StartNew(() =>
            {
                using (var dbContext = new StoreEntities())
                {
                    var products = dbContext.Product.Where(
                        x => x.category_id == Properties.Settings.Default.DB_Fruits_category_id
                        || x.category_id == Properties.Settings.Default.DB_Vegetables_category_id
                        || x.category_id == Properties.Settings.Default.DB_Bread_category_id
                        || x.category_id == Properties.Settings.Default.DB_Other_category_id
                    ).ToList();
                    // https://stackoverflow.com/questions/18331723/this-type-of-collectionview-does-not-support-changes-to-its-sourcecollection-fro

                    App.Current.Dispatcher.Invoke((System.Action)delegate
                    {
                        foreach (var product in products)
                        {
                            var productToAdd = new ProductModel(product);

                            // TODO maybe add enums for better readability
                            if (productToAdd.category_id == Properties.Settings.Default.DB_Fruits_category_id)
                                ListOfFruits.Add(productToAdd);
                            else if (productToAdd.category_id == Properties.Settings.Default.DB_Vegetables_category_id)
                                ListOfVegetables.Add(productToAdd);
                            else if (productToAdd.category_id == Properties.Settings.Default.DB_Bread_category_id)
                                ListOfBread.Add(productToAdd);
                            else
                                ListOfOther.Add(productToAdd);
                        }
                    });
                }
            });
        }

        public bool IsScreenEmpty()
        {
            if (_screenValue.Length == 0) return true;
            else return false;
        }
    }
}
