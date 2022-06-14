using Self_checkout.WpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Self_checkout.WpfApp.Commands
{
    public class TogglePopupMenuCommand : CommandBase
    {
        private readonly CheckoutViewModel _viewModel;

        public TogglePopupMenuCommand(CheckoutViewModel viewmodel)
        {
            _viewModel = viewmodel;
        }
        public override void Execute(object parameter)
        {
            if (_viewModel.IsPopupVisible == Visibility.Collapsed) _viewModel.IsPopupVisible = Visibility.Visible;
            else _viewModel.IsPopupVisible = Visibility.Collapsed;
        }
    }
}
