using GregsStack.InputSimulatorStandard;
using GregsStack.InputSimulatorStandard.Native;
using KeyboardSim.Model;
using KeyboardSim.ViewModel;
using KeySim.Common.UI;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using static KeyboardSim.WinNative;

namespace KeyboardSim
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public static int MainWindowHeight = 400;
        public static int MainWindowWidth = 800;

        private HwndSource _source;
        private WindowInteropHelper _winHelper;
        private IntPtr _winHandle;
        private InputSimulator _simulator = new InputSimulator();
        private MainWindowViewModel _viewModel = null;
        private SearchBarViewModel _searchBarViewModel = null;
        private SearchBar _searchBar = null;
        private WindowMovingHelper _movingHelper = null;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            _viewModel = new MainWindowViewModel();
            DataContext = _viewModel;
            Visibility = Visibility.Collapsed;

            // config search bar
            _searchBarViewModel = new SearchBarViewModel();
            _searchBar = new SearchBar(_searchBarViewModel)
            {
                Visibility = Visibility.Collapsed,
                Left = (SystemParameters.WorkArea.Width - MainWindowWidth) / 2,
                Top = (SystemParameters.WorkArea.Height - MainWindowHeight) / 2
            };
            _searchBar.Show();

            Left = _searchBar.Left;
            Top = _searchBar.Top + _searchBar.Height;

            _movingHelper = new WindowMovingHelper(this);
            InitializeComponent();
            DockStatus = DockStatus.NONE;
        }

        #region Properties
        private DockStatus _dockStatus;
        public DockStatus DockStatus
        {
            get { return _dockStatus; }
            set
            {
                _dockStatus = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Override Methods

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            _winHelper = new WindowInteropHelper(this);
            _winHandle = _winHelper.Handle;
            _source = HwndSource.FromHwnd(_winHandle);
            _source.AddHook(HwndHook);

            // Register global hotkey
            uint modKey = (uint)KeySimSetting.Instance[KeySimSetting.GLOBAL_SHORT_MODKEY] == 0 ? 0 : (uint)KeySimSetting.Instance[KeySimSetting.GLOBAL_SHORT_MODKEY];
            uint key = (uint)KeySimSetting.Instance[KeySimSetting.GLOBAL_SHORT_KEY] == 0 ? 0 : (uint)KeySimSetting.Instance[KeySimSetting.GLOBAL_SHORT_KEY];
            HotKeyManager.RegisterMainWindowHotKey(_winHandle, modKey, key, ShowWindow);

            // Initialize last data
            DiractiveCache.Instance.ParseData();

            // Set window style to inactived
            SetWindowLong(_winHandle, GWL_EXSTYLE, GetWindowLong(_winHandle, GWL_EXSTYLE) | WS_EX_NOACTIVATE);
        }

        protected override void OnClosed(EventArgs e)
        {
            HotKeyManager.UnregisterHotKey((uint)ModKeys.CTL, (uint)VirtualKeyCode.F10);
            _source.RemoveHook(HwndHook);
            _source = null;
            _winHandle = IntPtr.Zero;
            _winHelper = null;
            base.OnClosed(e);
        }

        #endregion

        #region Window move methods
        private void Caption_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DockStatus == DockStatus.NONE)
            {
                _movingHelper.Caption_MouseDown(sender, e);
            }
        }
        private void Caption_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (DockStatus == DockStatus.NONE)
            {
                _movingHelper.Caption_MouseUp(sender, e);
            }
        }
        private void Caption_MouseMove(object sender, MouseEventArgs e)
        {
            if (DockStatus == DockStatus.NONE)
            {
                _movingHelper.Caption_MouseMove(e);
            }
        }
        #endregion

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WM_HOTKEY:
                    HotKeyManager.ExecuteHotKey(wParam.ToInt32());
                    handled = true;
                    break;
            }
            return IntPtr.Zero;
        }

        private void ShowWindow()
        {
            DockWindow(DockStatus.FLOW);
        }

        internal void DockWindow(DockStatus dock)
        {
            switch (dock)
            {
                case DockStatus.LEFT:
                case DockStatus.RIGHT:
                    this.Top = 0;
                    double width = SystemParameters.WorkArea.Width / 6;
                    this.Left = dock == DockStatus.LEFT ? 0 : SystemParameters.WorkArea.Width - width;
                    this.Width = width;
                    this.Height = SystemParameters.WorkArea.Height;
                    this.BorderThickness = dock == DockStatus.LEFT ? new Thickness(0, 0, 7, 0) : new Thickness(7, 0, 0, 0);
                    DockStatus = dock;
                    this.Visibility = Visibility.Visible;
                    break;
                case DockStatus.NONE:
                    if (DockStatus != DockStatus.NONE)
                    {
                        this.Height = MainWindowHeight;
                        this.Width = MainWindowWidth;
                        this.Left = _searchBar.Left;
                        this.Top = _searchBar.Top + _searchBar.Height;
                        this.BorderThickness = new Thickness(7);
                        DockStatus = dock;
                    }
                    this.Visibility = Visibility.Visible;
                    break;
                case DockStatus.FLOW:
                    System.Drawing.Point point = _simulator.Mouse.Position;
                    _searchBar.Left = point.X;
                    _searchBar.Top = point.Y;
                    this.Visibility = Visibility.Visible;
                    _searchBar.Visibility = Visibility.Visible;
                    _searchBar.ActiveByKey();
                    if (DockStatus == DockStatus.NONE)
                    {
                        this.Height = MainWindowHeight;
                        this.Width = MainWindowWidth;
                        this.Left = _searchBar.Left;
                        this.Top = _searchBar.Top + _searchBar.Height;
                        this.BorderThickness = new Thickness(7);
                    }
                    break;
            }
        }

        #region Event Handler
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
            _searchBar.Visibility = Visibility.Collapsed;
        }
        private void ExitMenu_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void ShowMenu_Click(object sender, RoutedEventArgs e)
        {
            DockWindow(DockStatus);
            _searchBar.Visibility = Visibility.Visible;
            _searchBar.ActiveByKey();
        }
        #endregion

        #region INotificationChanged implementation
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
