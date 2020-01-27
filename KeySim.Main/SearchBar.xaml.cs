using KeySim.Common;
using KeySim.Common.UI;
using System.Windows;
using System.Windows.Input;

namespace KeyboardSim
{
    /// <summary>
    /// Interaction logic for SearchBar.xaml
    /// </summary>
    public partial class SearchBar : Window
    {
        private WindowMovingHelper _movingHelper = null;
        public SearchBar(ViewModelBase vm)
        {
            DataContext = vm;
            _movingHelper = new WindowMovingHelper(this);
            InitializeComponent();
        }

        #region Properties
        #endregion

        #region Window move methods
        private void Caption_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _movingHelper.Caption_MouseDown(sender, e);
        }
        private void Caption_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _movingHelper.Caption_MouseUp(sender, e);
        }
        private void Caption_MouseMove(object sender, MouseEventArgs e)
        {
            _movingHelper.Caption_MouseMove(e);
        }
        #endregion

        #region Event handlers
        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter && e.Key != Key.Escape) return;
            Visibility = Visibility.Collapsed;
        }

        private void Window_GotFocus(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus();
        }

        private void Window_Activated(object sender, System.EventArgs e)
        {
            txtSearch.Focus();
        }
        #endregion

        public void ActiveByKey()
        {
            Activate();
            Focus();
        }
    }
}
