﻿using KeyboardSim.Model;
using KeyboardSim.Parser;
using KeySim.Common;
using Microsoft.Win32;
using System.Windows;

namespace KeyboardSim
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public const string AppName = "KeySim";

        protected override void OnStartup(StartupEventArgs e)
        {
            App.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

            // initialize application setting
            AppSettings.InitializeSettings(KeySimSetting.Instance);
            // apply app seetings - launch on system up
            Utils.RegisterAutoStartupKey((bool)KeySimSetting.Instance[KeySimSetting.LAUNCH_ON_SYSUP], AppName);
            // initialize parsers
            ParserRepository.Instance.RegisterParser(new JsonDiractiveParser());

            base.OnStartup(e);
        }
    }
}
