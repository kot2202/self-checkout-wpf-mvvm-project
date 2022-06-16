using Self_checkout.WpfApp.Commands.Base;
using Self_checkout.WpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_checkout.WpfApp.Commands.CheckoutView
{
    public class SetPopupSelectedIndexCommand : CommandBase
    {
        private readonly CheckoutViewModel _checkoutView;

        public SetPopupSelectedIndexCommand(CheckoutViewModel viewModel)
        {
            _checkoutView = viewModel;
        }

        public override void Execute(object parameter)
        {
            int indexToSet = Int32.Parse((string)parameter);
            _checkoutView.PopupMenuSelectedIndex = indexToSet;
        }
    }
}
