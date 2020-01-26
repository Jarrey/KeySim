using KeyboardSim.Model;
using KeyboardSim.Parser;
using KeySim.Common;
using KeySim.Common.Command;
using Microsoft.Win32;
using System.Windows;

namespace KeyboardSim.ViewModel
{
    public class OpenDiractiveViewModel : ViewModelBase
    {
        public OpenDiractiveViewModel()
        {
            // default to use file source
            SourceType = DiractiveSource.FILE;
            ReadSettings();
            if (!string.IsNullOrEmpty(FilePath))
            {
                Diractive = DiractiveCache.Instance.ParseData(FilePath, SourceType);
            }

            SubmitCommand = new RelayCommand<Window>(p =>
            {
                if (p is Window window)
                {
                    KeySimSetting.Instance[KeySimSetting.LAST_DATASOURCE] = FilePath;
                    KeySimSetting.Instance[KeySimSetting.LAST_DATATYPE] = (uint)SourceType;
                    AppSettings.SaveSettings(KeySimSetting.Instance);
                    window.DialogResult = true;
                    window.Close();
                }
            }, p => Diractive?.Diractives?.Length > 0);
        }

        #region Properties
        private DiractiveSource _sourceType;
        public DiractiveSource SourceType
        {
            get { return _sourceType; }
            set { SetProperty(ref _sourceType, value); }
        }

        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set { SetProperty(ref _filePath, value); }
        }

        private IParser _parser;
        public IParser Parser
        {
            get { return _parser; }
            set { SetProperty(ref _parser, value); }
        }

        private Diractive _diractive;
        public Diractive Diractive
        {
            get { return _diractive; }
            set
            {
                SetProperty(ref _diractive, value);
                SubmitCommand?.RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region Commands
        public RelayCommand BrowseFileCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    var ofd = new OpenFileDialog
                    {
                        Filter = ParserRepository.Instance.SupportFormatFilter,
                        Multiselect = false
                    };
                    if (ofd.ShowDialog() == true && FilePath != ofd.FileName)
                    {
                        FilePath = ofd.FileName;
                        Diractive = DiractiveCache.Instance.ParseData(FilePath, SourceType);
                    }
                });
            }
        }

        public RelayCommand<Window> SubmitCommand { get; set; }
        #endregion

        #region Methods

        private void ReadSettings()
        {
            FilePath = KeySimSetting.Instance[KeySimSetting.LAST_DATASOURCE]?.ToString();
            SourceType = (uint)KeySimSetting.Instance[KeySimSetting.LAST_DATATYPE] == 0 ? 0 : (DiractiveSource)(uint)KeySimSetting.Instance[KeySimSetting.LAST_DATATYPE];
        }

        #endregion
    }
}
