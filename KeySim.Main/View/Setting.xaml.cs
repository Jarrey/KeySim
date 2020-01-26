using KeySim.Common;
using System.Windows;

namespace KeyboardSim.View
{
    /// <summary>
    /// Interaction logic for Setting.xaml
    /// </summary>
    public partial class Setting : Window
    {
        public Setting(ViewModelBase vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
