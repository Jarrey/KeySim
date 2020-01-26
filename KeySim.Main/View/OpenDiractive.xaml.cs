﻿using KeySim.Common;
using System.Windows;

namespace KeyboardSim.View
{
    /// <summary>
    /// Interaction logic for OpenDiractive.xaml
    /// </summary>
    public partial class OpenDiractive : Window
    {
        public OpenDiractive(ViewModelBase vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
