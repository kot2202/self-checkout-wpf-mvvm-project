using Self_checkout.WpfApp.Commands.Base;
using Self_checkout.WpfApp.Stores;
using Self_checkout.WpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Self_checkout.WpfApp.Commands.General
{
    internal class NavigateCommand<TViewModel> : CommandBase
        where TViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public NavigateCommand(NavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }
        public override async void Execute(object parameter)
        {
            int time = 0;
            if (parameter != null)
            {
                Int32.TryParse((string)parameter, out time);
            }
            await Task.Factory.StartNew(() =>
            { 
                Thread.Sleep(time);
                _navigationStore.CurrentViewModel = _createViewModel();
            });
        }

    }
}
