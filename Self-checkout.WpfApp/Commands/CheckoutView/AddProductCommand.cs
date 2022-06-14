using Self_checkout.WpfApp.Commands.Base;
using Self_checkout.WpfApp.DAL;
using Self_checkout.WpfApp.Models;
using Self_checkout.WpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_checkout.WpfApp.Commands.CheckoutView
{
    public class AddProductCommand : CommandBase
    {
        private readonly CheckoutViewModel _viewModel;

        public AddProductCommand(CheckoutViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        // TODO Add if screenvalue empty can execute -> false
        public override void Execute(object parameter)
        {
            long.TryParse((string)parameter, out var productId);
            TryToAddProduct(productId);
            _viewModel.RecalculatePriceSum();
            _viewModel.ClearCommand.Execute(null);
        }

        public override bool CanExecute(object parameter)
        {
            if (!_viewModel.IsScreenEmpty()) return true;
            else return false;
        }

        private bool TryToAddProduct(long productCode)
        {
            ProductModel productThatExists = _viewModel.ListOfProducts.Where(x => x.product_id == productCode).SingleOrDefault();
            // Removes and re-adds the product to update the list of products
            // It makes sense to do so as it will place the updated product at the bottom
            if(productThatExists != null)
            {
                _viewModel.ListOfProducts.Remove(productThatExists);
                // TODO Add weight randomization for products that have it
                productThatExists.CalculatedQuantity++;
                productThatExists.PriceSum = productThatExists.CalculatedQuantity * productThatExists.product_price;
                _viewModel.ListOfProducts.Add(productThatExists);
                return true;
            }

            using(var dbContext = new StoreEntities())
            {
                Product product = dbContext.Product.Where(x => x.product_id == productCode).SingleOrDefault(); // Db itself shouldn't let more products with same id appear
                if(product != null)
                {
                    ProductModel productToAdd = new ProductModel(product);
                    productToAdd.PriceSum = productToAdd.product_price;
                    _viewModel.ListOfProducts.Add(productToAdd);
                    return true;
                }
                return false;
            }
        }
    }
}
