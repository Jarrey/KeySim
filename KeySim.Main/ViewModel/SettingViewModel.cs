using KeySim.Common;
using KeySim.Common.Command;
using System.Windows;

namespace KeyboardSim.ViewModel
{
    public class SettingViewModel : ViewModelBase
    {
        private readonly AppSettings Setting = AppSettings.Instance;

        public SettingViewModel()
        {
        }

        #region Properties

        #endregion

        #region Methods

        private void ReadSettings()
        {
            Language = (string)Setting[AppSettings.GLOBAL_LANGUAGE];
            TextSize = (double)Setting[AppSettings.APP_TEXT_SIZE];
            TextColor = (Color)Setting[AppSettings.APP_TEXT_COLOR];
            TextHighlightColor = (Color)Setting[AppSettings.APP_TEXT_HIGHLIGHT_COLOR];
            ValueTextColor = (Color)Setting[AppSettings.APP_VALUE_TEXT_COLOR];
            LineColor = (Color)Setting[AppSettings.APP_LINE_COLOR];
            SignalColor = (Color)Setting[AppSettings.APP_SIGNAL_LINE_COLOR];
            SignalHighlightColor = (Color)Setting[AppSettings.APP_SIGNAL_LINE_HIGHLIGHT_COLOR];
        }

        private void WriteSettings()
        {
            Setting[AppSettings.GLOBAL_LANGUAGE] = Language;
            Setting[AppSettings.APP_TEXT_SIZE] = TextSize;
            Setting[AppSettings.APP_TEXT_COLOR] = TextColor;
            Setting[AppSettings.APP_TEXT_HIGHLIGHT_COLOR] = TextHighlightColor;
            Setting[AppSettings.APP_VALUE_TEXT_COLOR] = ValueTextColor;
            Setting[AppSettings.APP_LINE_COLOR] = LineColor;
            Setting[AppSettings.APP_SIGNAL_LINE_COLOR] = SignalColor;
            Setting[AppSettings.APP_SIGNAL_LINE_HIGHLIGHT_COLOR] = SignalHighlightColor;

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
                        AppSettings.SaveSettings(AppSettings.Instance);
                        window.DialogResult = true;
                        window.Close();
                    }
                });
            }
        }
        #endregion
    }
}
