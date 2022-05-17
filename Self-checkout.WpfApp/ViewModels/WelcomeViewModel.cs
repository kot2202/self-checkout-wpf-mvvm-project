using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Self_checkout.WpfApp.ViewModels
{
    public class WelcomeViewModel : ViewModelBase
    {
        public ICommand NavigateCheckoutCommand { get; }
    }
}
