﻿using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Self_checkout.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeMainWindow();
        }

        private void InitializeMainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ShowTitleBar = false;
            ResizeMode = ResizeMode.NoResize;
            ShowMinButton = false;
            ShowMaxRestoreButton = false;
            ShowCloseButton = false;
            WindowStyle = WindowStyle.None;
#if DEBUG
            ResizeMode = ResizeMode.CanResizeWithGrip;
#else
            ShowTitleBar = false;
            IgnoreTaskbarOnMaximize = true;
            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;
            ResizeMode = ResizeMode.NoResize;
            Topmost = true;
            IsCloseButtonEnabled = false;
            ShowCloseButton = false;
#endif
        }
    }
}
