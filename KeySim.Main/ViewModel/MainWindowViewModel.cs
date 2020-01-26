using GregsStack.InputSimulatorStandard;
using KeyboardSim.Model;
using KeyboardSim.View;
using KeySim.Common;
using KeySim.Common.Command;
using System.Windows;

namespace KeyboardSim.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private InputSimulator simulator = new InputSimulator();

        public MainWindowViewModel()
        {
            DockStatus = DockStatus.NONE;
        }

        #region Properties
        private Diractive _currentDiractive;
        public Diractive CurrentDiractive
        {
            get { return _currentDiractive; }
            set
            {
                SetProperty(ref _currentDiractive, value);
            }
        }

        private DockStatus _dockStatus;
        public DockStatus DockStatus
        {
            get { return _dockStatus; }
            set
            {
                SetProperty(ref _dockStatus, value);
            }
        }
        #endregion

        #region Commands
        public RelayCommand OpenCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    OpenDiractiveViewModel vm = new OpenDiractiveViewModel();
                    OpenDiractive openDir = new OpenDiractive(vm);
                    if (openDir.ShowDialog() == true)
                    {
                        CurrentDiractive = vm.Diractive;
                    }
                });
            }
        }

        public RelayCommand<Window> CloseCommand
        {
            get
            {
                return new RelayCommand<Window>(p =>
                {
                    if (p is Window window)
                    {
                        window.Visibility = Visibility.Collapsed;
                    }
                });
            }
        }

        public RelayCommand OpenSettingCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    SettingViewModel vm = new SettingViewModel();
                    Setting setting = new Setting(vm);
                    setting.ShowDialog();
                });
            }
        }

        public RelayCommand<Window> DockLeftCommand
        {
            get
            {
                return new RelayCommand<Window>(p =>
                {
                    if (p is Window window)
                    {
                        DockWindow(window, DockStatus.LEFT);
                    }
                });
            }
        }

        public RelayCommand<Window> DockRightCommand
        {
            get
            {
                return new RelayCommand<Window>(p =>
                {
                    if (p is Window window)
                    {
                        DockWindow(window, DockStatus.RIGHT);
                    }
                });
            }
        }

        public RelayCommand<Window> DockResetCommand
        {
            get
            {
                return new RelayCommand<Window>(p =>
                {
                    if (p is Window window)
                    {
                        DockWindow(window, DockStatus.NONE);
                    }
                });
            }
        }

        internal void DockWindow(Window window, DockStatus dock)
        {
            switch(dock)
            {
                case DockStatus.LEFT:
                case DockStatus.RIGHT:
                    window.Top = 0;
                    double width = SystemParameters.WorkArea.Width / 6;
                    window.Left = dock == DockStatus.LEFT ? 0 : SystemParameters.WorkArea.Width - width;
                    window.Width = width;
                    window.Height = SystemParameters.WorkArea.Height;
                    window.BorderThickness = dock == DockStatus.LEFT ? new Thickness(0, 0, 7, 0) : new Thickness(7, 0, 0, 0);
                    DockStatus = dock;
                    break;
                case DockStatus.NONE:
                    if (DockStatus != DockStatus.NONE)
                    {
                        window.Height = 600;
                        window.Width = 300;
                        window.Left = (SystemParameters.WorkArea.Width - window.Width) / 2;
                        window.Top = (SystemParameters.WorkArea.Height - window.Height) / 2;
                        window.BorderThickness = new Thickness(7);
                        DockStatus = dock;
                    }
                    break;
                case DockStatus.FLOW:
                    System.Drawing.Point point = simulator.Mouse.Position;
                    window.Height = 600;
                    window.Width = 300;
                    window.Left = point.X;
                    window.Top = point.Y;
                    window.BorderThickness = new Thickness(7);
                    break;
            }
            window.Visibility = Visibility.Visible;
        }
        #endregion
    }
}
