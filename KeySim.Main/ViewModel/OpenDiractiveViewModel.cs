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
            Diractive = DiractiveCache.Instance.Diractive;

            SubmitCommand = new RelayCommand<Window>(p =>
            {
                if (p is Window window)
                {
                    KeySimSetting.Instance[KeySimSetting.LAST_DATASOURCE] = SourceType == DiractiveSource.FILE ? FilePath : (SourceType == DiractiveSource.WEB ? URL : string.Empty);
                    KeySimSetting.Instance[KeySimSetting.LAST_DATATYPE] = (uint)SourceType;
                    AppSettings.SaveSettings(KeySimSetting.Instance);
                    window.DialogResult = true;
                    window.Close();
                }
            }, p => DataStatus);
        }

        #region Properties
        private DiractiveSource _sourceType;
        public DiractiveSource SourceType
        {
            get { return _sourceType; }
            set
            {
                SetProperty(ref _sourceType, value);
                switch (SourceType)
                {
                    case DiractiveSource.FILE:
                        TryGetData(FilePath);
                        break;
                    case DiractiveSource.WEB:
                        TryGetData(URL);
                        break;
                    case DiractiveSource.DATABASE:
                    default:
                        break;
                }
            }
        }

        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set { SetProperty(ref _filePath, value); }
        }

        private string _url;
        public string URL
        {
            get { return _url; }
            set { SetProperty(ref _url, value); }
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
            set { SetProperty(ref _diractive, value); }
        }

        public bool DataStatus
        {
            get
            {
                bool hasDiractive = Diractive?.Diractives?.Length > 0;
                switch (SourceType)
                {
                    case DiractiveSource.FILE:
                        return hasDiractive && !string.IsNullOrEmpty(FilePath);
                    case DiractiveSource.WEB:
                        return hasDiractive && !string.IsNullOrEmpty(URL);
                    case DiractiveSource.DATABASE:
                    default:
                        break;
                }
                return false;
            }
        }
        #endregion

        #region Commands
        public RelayCommand BrowseFileCommand => new RelayCommand(() =>
        {
            var ofd = new OpenFileDialog
            {
                Filter = ParserRepository.Instance.SupportFormatFilter,
                Multiselect = false
            };
            if (ofd.ShowDialog() == true)
            {
                FilePath = ofd.FileName;
                TryGetData(FilePath);
            }
        });

        public RelayCommand TestWebApiCommand => new RelayCommand(() => TryGetData(URL));

        public RelayCommand<Window> SubmitCommand { get; set; }
        #endregion

        #region Methods

        private void ReadSettings()
        {
            SourceType = (uint)KeySimSetting.Instance[KeySimSetting.LAST_DATATYPE] == 0 ? 0 : (DiractiveSource)(uint)KeySimSetting.Instance[KeySimSetting.LAST_DATATYPE];
            switch (SourceType)
            {
                case DiractiveSource.FILE:
                    FilePath = KeySimSetting.Instance[KeySimSetting.LAST_DATASOURCE]?.ToString();
                    break;
                case DiractiveSource.WEB:
                    URL = KeySimSetting.Instance[KeySimSetting.LAST_DATASOURCE]?.ToString();
                    break;
                case DiractiveSource.DATABASE:
                default:
                    break;
            }
        }

        private async void TryGetData(string path)
        {
            Diractive = await DiractiveCache.Instance.ParseData(path, SourceType);
            SubmitCommand?.RaiseCanExecuteChanged();
            OnPropertyChanged(nameof(DataStatus));
        }
        #endregion
    }
}
