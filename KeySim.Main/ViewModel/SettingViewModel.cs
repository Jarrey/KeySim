using GregsStack.InputSimulatorStandard.Native;
using KeySim.Common;
using KeySim.Common.Command;
using System.Windows;
using static KeyboardSim.WinNative;

namespace KeyboardSim.ViewModel
{
    public class SettingViewModel : ViewModelBase
    {
        private readonly IAppSettings Setting = KeySimSetting.Instance;

        public SettingViewModel()
        {
            ReadSettings();
        }

        #region Properties
        private bool _launchOnSysUp;
        public bool LaunchOnSysUp
        {
            get { return _launchOnSysUp; }
            set
            {
                SetProperty(ref _launchOnSysUp, value);
                Utils.RegisterAutoStartupKey(value, App.AppName);
            }
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
            LaunchOnSysUp = (bool)Setting[KeySimSetting.LAUNCH_ON_SYSUP];
            GlobalShortModKey = (uint)Setting[KeySimSetting.GLOBAL_SHORT_MODKEY] == 0 ? 0 : (ModKeys)(uint)Setting[KeySimSetting.GLOBAL_SHORT_MODKEY];
            GlobalShortKey = (uint)Setting[KeySimSetting.GLOBAL_SHORT_KEY] == 0 ? 0 : (VirtualKeyCode)(uint)Setting[KeySimSetting.GLOBAL_SHORT_KEY];
        }

        private void WriteSettings()
        {
            Setting[KeySimSetting.LAUNCH_ON_SYSUP] = LaunchOnSysUp;
            Setting[KeySimSetting.GLOBAL_SHORT_MODKEY] = (uint)GlobalShortModKey;
            Setting[KeySimSetting.GLOBAL_SHORT_KEY] = (uint)GlobalShortKey;

            AppSettings.SaveSettings(Setting);

            // Register global hotkey
            HotKeyManager.RegisterMainwindowHotKey((uint)GlobalShortModKey, (uint)GlobalShortKey);
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
