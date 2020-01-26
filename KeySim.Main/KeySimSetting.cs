using KeySim.Common;
using System;
using System.IO;

namespace KeyboardSim
{
    public class KeySimSetting : IAppSettings
    {
        public string SettingFileName => "keysim.setting";
        public string SettingFilePath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), SettingFileName);
        public ObservableDictionary<string, object> Settings { get; private set; }
        public event EventHandler SettingChanged;

        private KeySimSetting()
        {
            Settings = new ObservableDictionary<string, object>
            {
                // initialize setting values
                [LAUNCH_ON_SYSUP] = true,
                [GLOBAL_SHORT_MODKEY] = 0u,
                [GLOBAL_SHORT_KEY] = 0u,
                [LAST_DATATYPE] = 0u,
                [LAST_DATASOURCE] = string.Empty
            };

            Settings.PropertyChanged += Settings_PropertyChanged;
        }

        #region Setting fields

        public const string LAUNCH_ON_SYSUP = nameof(LAUNCH_ON_SYSUP);
        public const string GLOBAL_SHORT_MODKEY = nameof(GLOBAL_SHORT_MODKEY);
        public const string GLOBAL_SHORT_KEY = nameof(GLOBAL_SHORT_KEY);

        public const string LAST_DATATYPE = nameof(LAST_DATATYPE);
        public const string LAST_DATASOURCE = nameof(LAST_DATASOURCE);

        #endregion

        #region For singleton instance

        private static KeySimSetting _instance;
        public static KeySimSetting Instance
        {
            get { return _instance ?? (_instance = new KeySimSetting()); }
        }

        #endregion

        #region Indexer
        public object this[string keyName]
        {
            get { return Settings.ContainsKey(keyName) ? Settings[keyName] : null; }
            set { if (Settings.ContainsKey(keyName)) Settings[keyName] = value; }
        }
        #endregion

        private void Settings_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Values" && SettingChanged != null)
            {
                SettingChanged(this, EventArgs.Empty);
            }
        }
    }
}
