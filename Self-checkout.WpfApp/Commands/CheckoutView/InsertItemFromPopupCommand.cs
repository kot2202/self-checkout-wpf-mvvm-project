using Self_checkout.WpfApp.Commands.Base;
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

        public override void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
