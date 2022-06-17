using Self_checkout.WpfApp.Commands.Base;
using Self_checkout.WpfApp.DAL;
using Self_checkout.WpfApp.Models;
using Self_checkout.WpfApp.PLACEHOLDER;
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

        // TODO Check for a better solution here
        private bool TryToAddProduct(long productCode)
        {
            if (TryToAddRepeatedProduct(productCode)) return true;
            return TryToAddNewProduct(productCode);
        }

        /// <summary>
        /// Tries to increase the quantity of product in order if it already exists.
        /// </summary>
        /// <param name="productCode">ID of product</param>
        /// <returns>True if the product exists and false if it does not.</returns>
        public bool TryToAddRepeatedProduct(long productCode)
        {
            ProductModel productThatExists = _viewModel.ListOfProducts.Where(x => x.product_id == productCode).SingleOrDefault();
            // Removes and re-adds the product to update the list of products
            // It makes sense to do so as it will place the updated product at the bottom
            if (productThatExists != null)
            {
                _viewModel.ListOfProducts.Remove(productThatExists);
                
                // TODO REMOVE IF NEEDED
                // PLACEHOLDER weight randomization for weighted categories
                if(Properties.Settings.Default.DB_Weighted_products_category_ids.Contains(productThatExists.category_id))
                {
                    productThatExists.CalculatedQuantity += RandomWeightGenerator.Random();
                }
                else productThatExists.CalculatedQuantity++;

                productThatExists.PriceSum = productThatExists.CalculatedQuantity * productThatExists.product_price;
                _viewModel.ListOfProducts.Add(productThatExists);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Tries to add new product to order
        /// </summary>
        /// <param name="productCode">ID of product</param>
        /// <returns>True if product was succesfully added, False if not</returns>
        public bool TryToAddNewProduct(long productCode)
        {
            try
            {
                using (var dbContext = new StoreEntities())
                {
                    Product product = dbContext.Product.Where(x => x.product_id == productCode).SingleOrDefault(); // Db itself shouldn't let more products with same id appear
                    if (product != null)
                    {
                        ProductModel productToAdd = new ProductModel(product);

                        // TODO REMOVE IF NEEDED
                        // PLACEHOLDER weight randomization for weighted categories
                        if (Properties.Settings.Default.DB_Weighted_products_category_ids.Contains(productToAdd.category_id))
                        {
                            productToAdd.CalculatedQuantity += RandomWeightGenerator.Random();
                        }
                        productToAdd.PriceSum = decimal.Round(productToAdd.CalculatedQuantity * productToAdd.product_price,2,MidpointRounding.AwayFromZero);
                        
                        //productToAdd.PriceSum = productToAdd.product_price;


                        _viewModel.ListOfProducts.Add(productToAdd);
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problems with db");
                throw new NotImplementedException(); // TODO add popup or some information about db problem
            }
        }
    }
}
