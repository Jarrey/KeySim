﻿using KeyboardSim.Model;
using KeyboardSim.View;
using KeySim.Common;
using KeySim.Common.Command;
using System.Windows;

namespace KeyboardSim.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
        }

        #region Properties
        #endregion

        #region Commands
        public RelayCommand OpenCommand => new RelayCommand(() =>
        {
            OpenDiractiveViewModel vm = new OpenDiractiveViewModel();
            OpenDiractive openDir = new OpenDiractive(vm);
            openDir.ShowDialog();
        });

        public RelayCommand SyncCommand => new RelayCommand(DiractiveCache.Instance.ParseData);

        public RelayCommand<Window> HideCommand => new RelayCommand<Window>(p =>
        {
            if (p is Window window)
            {
                window.Visibility = Visibility.Collapsed;
            }
        });

        public RelayCommand OpenSettingCommand => new RelayCommand(() =>
        {
            SettingViewModel vm = new SettingViewModel();
            Setting setting = new Setting(vm);
            setting.ShowDialog();
        });

        public RelayCommand<Window> DockLeftCommand => new RelayCommand<Window>(p =>
        {
            if (p is MainWindow window)
            {
                window.DockWindow(DockStatus.LEFT);
            }
        });

        public RelayCommand<Window> DockRightCommand => new RelayCommand<Window>(p =>
        {
            if (p is MainWindow window)
            {
                window.DockWindow(DockStatus.RIGHT);
            }
        });

        public RelayCommand<Window> DockResetCommand => new RelayCommand<Window>(p =>
        {
            if (p is MainWindow window)
            {
                window.DockWindow(DockStatus.NONE);
            }
        });

        #endregion
    }
}
