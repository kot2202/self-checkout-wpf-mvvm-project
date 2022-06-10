﻿using Self_checkout.WpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_checkout.WpfApp.Commands
{
    public class UndoNumberCommand : CommandBase
    {
        private readonly CheckoutViewModel _viewModel;

        public UndoNumberCommand(CheckoutViewModel viewModel)
        {
            _viewModel = viewModel;
            OnCanExecuteChanged();
        }
        public override void Execute(object parameter)
        {
            int currentLength = _viewModel.ScreenValue.Length;
            _viewModel.ScreenValue = _viewModel.ScreenValue.Remove(currentLength - 1);
        }

        public override bool CanExecute(object parameter)
        {
            if (!_viewModel.IsScreenEmpty()) return true;
            else return false;
        }
    }
}
