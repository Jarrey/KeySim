using KeyboardSim.Parser;
using KeySim.Common;
using System.Windows;

namespace KeyboardSim
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // initialize application setting
            AppSettings.InitializeSettings(AppSettings.Instance);

            // initialize parsers
            ParserRepository.Instance.RegisterParser(new JsonDiractiveParser());

            base.OnStartup(e);
        }
    }
}
