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
    public class FinalizeOrderCommand : CommandBase
    {
        private readonly CheckoutViewModel _viewmodel;

        public FinalizeOrderCommand(CheckoutViewModel viewmodel)
        {
            _viewmodel = viewmodel;
        }
        public override void Execute(object parameter)
        {
            // TODO MAYBE ADD the payment handler
            try
            {
                using (var dbContext = new StoreEntities())
                {
                    // If any order exists then +1 the ID, if it does not then return 1
                    int newOrderId = dbContext.Order.Max(x => (int?)x.order_id) + 1 ?? 1;

                    DateTime currentTime = DateTime.Now;
                    foreach (ProductModel purchasedProduct in _viewmodel.ListOfProducts)
                    {
                        Order orderProduct = new Order()
                        {
                            order_id = newOrderId,
                            order_product_id = purchasedProduct.product_id,
                            order_product_quantity = purchasedProduct.CalculatedQuantity,
                            order_products_price = purchasedProduct.product_price,
                            order_time = currentTime
                        };
                        dbContext.Order.Add(orderProduct);
                    }
                    dbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Problems with db");
                throw new NotImplementedException(); // TODO add popup or some information about db problem
            }
            _viewmodel.NavigatePostPurchaseCommand.Execute(null);
        }

        public override bool CanExecute(object parameter)
        {
            // TODO make it so empty order cannot be finalized
            return true;
        }
    }
}
