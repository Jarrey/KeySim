using GregsStack.InputSimulatorStandard;
using GregsStack.InputSimulatorStandard.Native;
using KeyboardSim.Model;
using KeyboardSim.ViewModel;
using KeySim.Common;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using static KeyboardSim.WinNative;

namespace KeyboardSim
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private HwndSource _source;
        private WindowInteropHelper _winHelper;
        private IntPtr _winHandle;
        private MainWindowViewModel viewModel = new MainWindowViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        #region Override Methods

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            _winHelper = new WindowInteropHelper(this);
            _winHandle = _winHelper.Handle;
            _source = HwndSource.FromHwnd(_winHandle);
            _source.AddHook(HwndHook);

            // Register global hotkey
            uint modKey = (uint)AppSettings.Instance[AppSettings.GLOBAL_SHORT_MODKEY] == 0 ? 0 : (uint)AppSettings.Instance[AppSettings.GLOBAL_SHORT_MODKEY];
            uint key = (uint)AppSettings.Instance[AppSettings.GLOBAL_SHORT_KEY] == 0 ? 0 : (uint)AppSettings.Instance[AppSettings.GLOBAL_SHORT_KEY];
            HotKeyManager.RegisterMainwindowHotKey(_winHandle, modKey, key, ShowWindow);

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

        #region Window move methods
        private bool isMoving = false;
        private Point currentPosition = new Point();
        private void Caption_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMoving = true;
            (sender as Grid).CaptureMouse();
            currentPosition = e.GetPosition(this);

            currentPosition.Y = Convert.ToInt16(Top) + currentPosition.Y;
            currentPosition.X = Convert.ToInt16(Left) + currentPosition.X;
        }
        private void Caption_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isMoving = false;
            (sender as Grid).ReleaseMouseCapture();
        }

        private void Caption_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMoving)
            {
                Point p = e.GetPosition(this);
                Point MousePositionAbs = new Point
                {
                    X = Convert.ToInt16(Left) + p.X,
                    Y = Convert.ToInt16(Top) + p.Y
                };
                Left = Left + (MousePositionAbs.X - currentPosition.X);
                Top = Top + (MousePositionAbs.Y - currentPosition.Y);
                currentPosition = MousePositionAbs;
            }
        }
        #endregion
        
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
            viewModel.DockWindow(this, DockStatus.FLOW);
        }

        #region Event Handler
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }
        private void ExitMenu_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void ShowMenu_Click(object sender, RoutedEventArgs e)
        {
            viewModel.DockWindow(this, viewModel.DockStatus);
        }
        #endregion
    }
}
