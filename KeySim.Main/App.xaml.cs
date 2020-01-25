using KeyboardSim_Demo.Parser;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace KeyboardSim_Demo
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // initialize parsers
            ParserRepository.Instance.RegisterParser(new JsonDiractiveParser());

            base.OnStartup(e);
        }
    }
}
