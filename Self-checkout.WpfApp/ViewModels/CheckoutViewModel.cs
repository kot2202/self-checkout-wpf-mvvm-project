using Self_checkout.WpfApp.Commands;
using Self_checkout.WpfApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                }
        }

        private List<ProductModel> _listOfProducts = new List<ProductModel>();
        public List<ProductModel> ListOfProducts
        {
            get {  return _listOfProducts; }
            set { _listOfProducts = value;}
        }

        public ICommand NavigatePostPurchaseCommand { get; set; }
        public ICommand AddNumberCommand { get; }
        public ICommand UndoNumberCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand AddProductCommand { get; }

        public CheckoutViewModel(Stores.NavigationStore navigationStore)
        {
            NavigatePostPurchaseCommand = new NavigateCommand<PostPurchaseViewModel>(navigationStore, () => new PostPurchaseViewModel(navigationStore));
            AddNumberCommand = new AddNumberCommand(this);
            UndoNumberCommand = new UndoNumberCommand(this);
            ClearCommand = new ClearCommand(this);
            AddProductCommand = new AddProductCommand(this);


            // TODO REMOVE THIS AFTER TESTING IS DONE
            List<ProductModel> testingProducts = new List<ProductModel>()
            {
                new ProductModel(){product_name = "name1", CalculatedQuantity = 3, product_price = 2.5m},
                new ProductModel(){product_name = "name2", CalculatedQuantity = 8, product_price = 99.99m},
                new ProductModel(){product_name = "name3", CalculatedQuantity = 1, product_price = 1.00m}
            };

            ListOfProducts = testingProducts;
        }
    }
}
