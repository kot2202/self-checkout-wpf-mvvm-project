using Self_checkout.WpfApp.Stores;
using Self_checkout.WpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Self_checkout.WpfApp.Commands
{
    internal class NavigateCheckoutCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;

        public NavigateCheckoutCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }
        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new CheckoutViewModel();
        }
    }
}
