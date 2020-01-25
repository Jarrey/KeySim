using KeySim.Common;
using System.Windows;

namespace KeyboardSim_Demo.View
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
