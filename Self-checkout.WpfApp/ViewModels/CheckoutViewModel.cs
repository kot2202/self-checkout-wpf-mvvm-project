using Self_checkout.WpfApp.Commands;
using Self_checkout.WpfApp.DAL; // TODO REMOVE THIS AFTER TESTING IS DONE
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
            {
                StoreEntities dbContext = new StoreEntities();
                var x = dbContext.Product.ToList();

                List<ProductModel> newList = new List<ProductModel>();
                foreach (var y in x)
                {
                    ProductModel newModel = new ProductModel() { product_img = y.product_img, product_name = y.product_name, product_price = y.product_price };
                    newList.Add(newModel);
                }
                ListOfProducts = newList;
            }
        }
    }
}
