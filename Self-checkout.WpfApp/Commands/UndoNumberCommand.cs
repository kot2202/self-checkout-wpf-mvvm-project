using Self_checkout.WpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_checkout.WpfApp.Commands
{
    public class UndoNumberCommand : CommandBase
    {
        private readonly CheckoutViewModel _viewModel;

        public UndoNumberCommand(CheckoutViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public override void Execute(object parameter)
        {
            int currentLength = _viewModel.ScreenValue.Length;
            if (currentLength > 0)
                _viewModel.ScreenValue = _viewModel.ScreenValue.Remove(currentLength - 1);
        }
    }
}
