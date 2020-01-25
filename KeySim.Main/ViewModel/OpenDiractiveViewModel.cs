using KeyboardSim_Demo.Model;
using KeyboardSim_Demo.Parser;
using KeySim.Common;
using KeySim.Common.Command;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace KeyboardSim_Demo.ViewModel
{
    public class OpenDiractiveViewModel : ViewModelBase
    {
        public OpenDiractiveViewModel()
        {
            // default to use file source
            IsFileSource = true;

            SubmitCommand = new RelayCommand<Window>(p =>
            {
                if (p is Window window)
                {
                    window.DialogResult = true;
                    window.Close();
                }
            }, p => Diractive?.Diractives?.Length > 0);
        }

        #region Properties
        private bool _isFileSource;
        public bool IsFileSource
        {
            get { return _isFileSource; }
            set { SetProperty(ref _isFileSource, value); }
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
                SubmitCommand.RaiseCanExecuteChanged();
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
                    if (ofd.ShowDialog() == true)
                    {
                        Parser = ParserRepository.Instance.GetParser(Path.GetExtension(ofd.FileName));
                        FilePath = ofd.FileName;
                        string data = File.ReadAllText(FilePath);

                        if (Parser != null)
                        {
                            Diractive = Parser.Parse(data);
                        }
                    }
                });
            }
        }

        public RelayCommand<Window> SubmitCommand { get; set; }
        #endregion

    }
}
