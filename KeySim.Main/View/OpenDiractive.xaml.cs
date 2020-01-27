using KeyboardSim.ViewModel;
using KeySim.Common;
using System.Windows;

namespace KeyboardSim.View
{
    /// <summary>
    /// Interaction logic for OpenDiractive.xaml
    /// </summary>
    public partial class OpenDiractive : Window
    {
        private OpenDiractiveViewModel _viewModel;
        public OpenDiractive(ViewModelBase vm)
        {
            InitializeComponent();
            DataContext = _viewModel = vm as OpenDiractiveViewModel;
        }

        private void WebSourceGrid_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _viewModel.SourceType = Model.DiractiveSource.WEB;
        }
        private void FileSourceGrid_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _viewModel.SourceType = Model.DiractiveSource.FILE;
        }
    }
}
