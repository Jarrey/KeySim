using KeySim.Common;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using KeySim.Common.Extension;

namespace KeyboardSim.Model
{
    public class Diractive : ModelBase
    {
        private const int HotKeyCount = 9;

        public Diractive()
        {
            FilteredDiractives = new ObservableCollection<Action>();
        }
        public Diractive(DiractiveSource source, DiractiveFormat format, string uri = null)
        {
            Source = source;
            Format = format;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }
        public DiractiveSource Source { get; }
        public DiractiveFormat Format { get; }

        public Action[] Diractives { get; set; }

        private string _filterKeyword;
        [JsonIgnore]
        public string FilterKeyword
        {
            get { return _filterKeyword; }
            set
            {
                if (value != _filterKeyword)
                {
                    _filterKeyword = value;
                    FilterDiractives();
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<Action> _filteredDiractives;
        [JsonIgnore]
        public ObservableCollection<Action> FilteredDiractives
        {
            get { return _filteredDiractives; }
            set { SetProperty(ref _filteredDiractives, value); }
        }

        public void FilterDiractives()
        {
            // Unregister top N actions hotkey
            UnregisterHotKey();

            // Filter diractive
            FilteredDiractives = new ObservableCollection<Action>(Diractives.Where(a => a.Name.Contains(FilterKeyword, StringComparison.OrdinalIgnoreCase)));

            // Register top N actions hotkey
            RegisterHotKey();
        }

        private void UnregisterHotKey()
        {
            if (FilteredDiractives != null)
            {
                foreach (var action in FilteredDiractives.Take(HotKeyCount))
                {
                    action.UnregisterHotKey();
                }
            }
        }

        private void RegisterHotKey()
        {
            int keyNum = 1;
            if (FilteredDiractives != null)
            {
                foreach (var action in FilteredDiractives.Take(HotKeyCount))
                {
                    action.ShortKeyString = $"CTL|{keyNum++}";
                    action.RegisterHotKey();
                }
            }
        }
    }
}
