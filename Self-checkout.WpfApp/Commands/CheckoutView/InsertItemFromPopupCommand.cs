using Self_checkout.WpfApp.Commands.Base;
using Self_checkout.WpfApp.Models;
using Self_checkout.WpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_checkout.WpfApp.Commands.CheckoutView
{
    public class InsertItemFromPopupCommand : CommandBase
    {
        private readonly CheckoutViewModel _viewModel;

        public InsertItemFromPopupCommand(CheckoutViewModel viewmodel)
        {
            _viewModel = viewmodel;
        }

        // TODO Check for a better solution here
        public override void Execute(object parameter)
        {
            ProductModel productToAdd = parameter as ProductModel;
            long productCode = productToAdd.product_id;

            if (_viewModel.AddProductCommand.TryToAddRepeatedProduct(productCode)) { }
            else _viewModel.AddProductCommand.TryToAddNewProduct(productCode);
            _viewModel.RecalculatePriceSum();
            // TODO add quantity select for non weighted products
            _viewModel.TogglePopupMenuCommand.Execute(null);

        }
    }
}
