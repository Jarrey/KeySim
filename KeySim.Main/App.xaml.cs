using KeyboardSim.Parser;
using KeySim.Common;
using KeySim.Common.Core;
using System;
using System.Collections.Generic;
using System.Windows;

namespace KeyboardSim
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application, ISingleInstanceApp
    {
        public const string AppName = "KeySim";

        [STAThread]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public static void Main()
        {
            if (SingleInstance<App>.InitializeAsFirstInstance(AppName))
            {
                var application = new App();
                application.InitializeComponent();
                application.Run();
                // Allow single instance code to perform cleanup operations
                SingleInstance<App>.Cleanup();
            }
        }

        #region ISingleInstanceApp Members
        public bool SignalExternalCommandLineArgs(IList<string> args)
        {
            return true;
        }
        #endregion

        protected override void OnStartup(StartupEventArgs e)
        {
            ShutdownMode = ShutdownMode.OnMainWindowClose;

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
