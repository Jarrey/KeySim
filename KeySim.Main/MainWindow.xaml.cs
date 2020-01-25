using GregsStack.InputSimulatorStandard;
using GregsStack.InputSimulatorStandard.Native;
using KeyboardSim_Demo.View;
using KeyboardSim_Demo.ViewModel;
using System;
using System.Windows;
using System.Windows.Interop;
using static KeyboardSim_Demo.Utils;
using static KeyboardSim_Demo.WinNative;

namespace KeyboardSim_Demo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private HwndSource _source;
        private WindowInteropHelper _winHelper;
        private IntPtr _winHandle;
        private InputSimulator simulator = new InputSimulator();
        private MainWindowViewModel viewModel = new MainWindowViewModel();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        #region Override Methods

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            _winHelper = new WindowInteropHelper(this);
            _winHandle = _winHelper.Handle;
            _source = HwndSource.FromHwnd(_winHandle);
            _source.AddHook(HwndHook);
            RegisterHotKey(_winHandle, (uint)ModKeys.CTL, (uint)VirtualKeyCode.F10);

            // Set window style to inactived
            SetWindowLong(_winHandle, GWL_EXSTYLE, GetWindowLong(_winHandle, GWL_EXSTYLE) | WS_EX_NOACTIVATE);
        }

        protected override void OnClosed(EventArgs e)
        {
            UnregisterHotKey(_winHandle);
            _source.RemoveHook(HwndHook);
            _source = null;
            _winHandle = IntPtr.Zero;
            _winHelper = null;
            base.OnClosed(e);
        }

        #endregion

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WM_HOTKEY:
                    switch (wParam.ToInt32())
                    {
                        case HOTKEY_ID:
                            OnHotKeyPressed();
                            handled = true;
                            break;
                    }
                    break;
            }
            return IntPtr.Zero;
        }

        private void OnHotKeyPressed()
        {
            System.Drawing.Point point = simulator.Mouse.Position;
            Left = point.X;
            Top = point.Y;
            Visibility = Visibility.Visible;
        }

        #region Event Handler

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            simulator.Keyboard.TextEntry("你好!");
            simulator.Keyboard.KeyDown(VirtualKeyCode.TAB);

            // this.Visibility = Visibility.Collapsed;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // this.Visibility = Visibility.Collapsed;
        }

        #endregion

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenDiractiveViewModel vm = new OpenDiractiveViewModel();
            OpenDiractive openDir = new OpenDiractive(vm);
            if (openDir.ShowDialog() == true)
            {
                viewModel.CurrentDiractive = vm.Diractive;
            }
        }
    }
}
