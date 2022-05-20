using Self_checkout.WpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_checkout.WpfApp.Commands
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
            throw new NotImplementedException(); // TODO add sql query to add product to the product list on the left
        }
    }
}
