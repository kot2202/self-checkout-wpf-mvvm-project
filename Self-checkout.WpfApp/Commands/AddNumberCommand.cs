using Self_checkout.WpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_checkout.WpfApp.Commands
{
    public class AddNumberCommand : CommandBase
    {
        private readonly CheckoutViewModel _viewModel;

        public AddNumberCommand(CheckoutViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public override void Execute(object parameter)
        {
            if(_viewModel.ScreenValue.Length < _viewModel.MaxScreenLength)
                _viewModel.ScreenValue += parameter as string;
        }
    }
}
