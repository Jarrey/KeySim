using KeyboardSim_Demo.Model;
using KeySim.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardSim_Demo.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Diractive _currentDiractive;
        public Diractive CurrentDiractive
        {
            get { return _currentDiractive; }
            set
            {
                SetProperty(ref _currentDiractive, value);
            }
        }
    }
}
