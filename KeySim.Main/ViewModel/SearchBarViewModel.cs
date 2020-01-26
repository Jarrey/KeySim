using KeySim.Common;
using KeySim.Common.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KeyboardSim.ViewModel
{
    public class SearchBarViewModel : ViewModelBase
    {
        public RelayCommand<Window> HideCommand
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

    }
}
