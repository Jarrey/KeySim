using GregsStack.InputSimulatorStandard.Native;
using KeySim.Common;
using KeySim.Common.Command;
using System.Windows;
using static KeyboardSim.WinNative;

namespace KeyboardSim.ViewModel
{
    public class SettingViewModel : ViewModelBase
    {
        private readonly AppSettings Setting = AppSettings.Instance;

        public SettingViewModel()
        {
            ReadSettings();
        }

        #region Properties
        private bool _launchOnSysUp;
        public bool LaunchOnSysUp
        {
            get { return _launchOnSysUp; }
            set { SetProperty(ref _launchOnSysUp, value); }
        }

        private ModKeys _globalShortModKey;
        public ModKeys GlobalShortModKey
        {
            get { return _globalShortModKey; }
            set { SetProperty(ref _globalShortModKey, value); }
        }

        private VirtualKeyCode _globalShortKey;
        public VirtualKeyCode GlobalShortKey
        {
            get { return _globalShortKey; }
            set { SetProperty(ref _globalShortKey, value); }
        }
        #endregion

        #region Methods

        private void ReadSettings()
        {
            LaunchOnSysUp = (bool)Setting[AppSettings.LAUNCH_ON_SYSUP];
            GlobalShortModKey = (uint)Setting[AppSettings.GLOBAL_SHORT_MODKEY] == 0 ? 0 : (ModKeys)(uint)Setting[AppSettings.GLOBAL_SHORT_MODKEY];
            GlobalShortKey = (uint)Setting[AppSettings.GLOBAL_SHORT_KEY] == 0 ? 0 : (VirtualKeyCode)(uint)Setting[AppSettings.GLOBAL_SHORT_KEY];
        }

        private void WriteSettings()
        {
            Setting[AppSettings.LAUNCH_ON_SYSUP] = LaunchOnSysUp;
            Setting[AppSettings.GLOBAL_SHORT_MODKEY] = (uint)GlobalShortModKey;
            Setting[AppSettings.GLOBAL_SHORT_KEY] = (uint)GlobalShortKey;

            AppSettings.SaveSettings(Setting);
        }

        #endregion

        #region Commands
        public RelayCommand<Window> OkayCommand
        {
            get
            {
                return new RelayCommand<Window>(p =>
                {
                    if (p is Window window)
                    {
                        WriteSettings();
                        window.DialogResult = true;
                        window.Close();
                    }
                });
            }
        }

        #endregion
    }
}
