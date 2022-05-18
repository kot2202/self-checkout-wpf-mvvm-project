using Self_checkout.WpfApp.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Self_checkout.WpfApp.ViewModels
{
    public class PostPurchaseViewModel : ViewModelBase
    {
        public ICommand NavigateWelcomeCommand { get; set; }

        public PostPurchaseViewModel(NavigationStore navigationStore)
        {
            // TODO May need edit if want to go back to welcome screen.
        }
    }
}
