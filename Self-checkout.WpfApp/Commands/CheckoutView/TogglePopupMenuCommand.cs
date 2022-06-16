using Self_checkout.WpfApp.Commands.Base;
using Self_checkout.WpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Self_checkout.WpfApp.Commands.CheckoutView
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
            if (parameter != null)
            {
                Int32.TryParse((string)parameter, out int indexToSet);
                _viewModel.PopupMenuSelectedIndex = indexToSet;
            }
            _viewModel.IsPopupVisible = !_viewModel.IsPopupVisible;
        }
    }
}
