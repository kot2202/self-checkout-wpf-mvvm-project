using Self_checkout.WpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_checkout.WpfApp.Commands
{
    public class ClearCommand : CommandBase
    {
        private readonly CheckoutViewModel _viewModel;

        public ClearCommand(CheckoutViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public override void Execute(object parameter)
        {
            _viewModel.ScreenValue = "";
        }
    }
}
