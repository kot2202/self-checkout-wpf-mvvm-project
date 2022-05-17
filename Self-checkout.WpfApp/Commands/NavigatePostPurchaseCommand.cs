using Self_checkout.WpfApp.Stores;
using Self_checkout.WpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_checkout.WpfApp.Commands
{
    internal class NavigatePostPurchaseCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;

        public NavigatePostPurchaseCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }
        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new PostPurchaseViewModel();
        }
    }
}
