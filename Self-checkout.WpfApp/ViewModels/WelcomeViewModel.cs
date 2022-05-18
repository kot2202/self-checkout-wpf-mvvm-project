using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Self_checkout.WpfApp.Commands;
using Self_checkout.WpfApp.Stores;

namespace Self_checkout.WpfApp.ViewModels
{
    public class WelcomeViewModel : ViewModelBase
    {
        public ICommand NavigateCheckoutCommand { get; set; }

        public WelcomeViewModel(NavigationStore navigationStore)
        {
            NavigateCheckoutCommand = new NavigateCommand<CheckoutViewModel>(navigationStore, () => new CheckoutViewModel(navigationStore));
        }
    }
}
