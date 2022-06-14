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

        public AddNumberCommand AddNumberCommand { get;}
        public UndoNumberCommand UndoNumberCommand { get; }
        public ClearCommand ClearCommand { get; }
        public AddProductCommand AddProductCommand { get; }

        public CheckoutViewModel(Stores.NavigationStore navigationStore)
        {
            NavigatePostPurchaseCommand = new NavigateCommand<PostPurchaseViewModel>(navigationStore, () => new PostPurchaseViewModel(navigationStore));
            TogglePopupMenuCommand = new TogglePopupMenuCommand(this);
            InsertItemFromPopupCommand = new InsertItemFromPopupCommand(this);

            AddNumberCommand = new AddNumberCommand(this);
            UndoNumberCommand = new UndoNumberCommand(this);
            ClearCommand = new ClearCommand(this);
            AddProductCommand = new AddProductCommand(this);

            // TODO REMOVE WHEN DONE TESTING
            using (var dbContext = new StoreEntities())
            {
                foreach(var product in dbContext.Product)
                {
                    ListOfFruits.Add(new ProductModel(product));
                }
            }
        }

        public bool IsScreenEmpty()
        {
            if (_screenValue.Length == 0) return true;
            else return false;
        }
    }
}
