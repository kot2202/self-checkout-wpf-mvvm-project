using Self_checkout.WpfApp.Commands;
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

        private Visibility _isPopupVisible = Visibility.Collapsed;

        public Visibility IsPopupVisible
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

        public ICommand NavigatePostPurchaseCommand { get; set; }
        public ICommand TogglePopupMenuCommand { get; set; }

        public AddNumberCommand AddNumberCommand { get; set; }
        public UndoNumberCommand UndoNumberCommand { get; }
        public ClearCommand ClearCommand { get; }
        public AddProductCommand AddProductCommand { get; }

        public CheckoutViewModel(Stores.NavigationStore navigationStore)
        {
            NavigatePostPurchaseCommand = new NavigateCommand<PostPurchaseViewModel>(navigationStore, () => new PostPurchaseViewModel(navigationStore));
            TogglePopupMenuCommand = new TogglePopupMenuCommand(this);
            AddNumberCommand = new AddNumberCommand(this);
            UndoNumberCommand = new UndoNumberCommand(this);
            ClearCommand = new ClearCommand(this);
            AddProductCommand = new AddProductCommand(this);
        }

        public bool IsScreenEmpty()
        {
            if (_screenValue.Length == 0) return true;
            else return false;
        }
    }
}
