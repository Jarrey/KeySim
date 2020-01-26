using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using KeySim.Common.Extension;
using System.Windows.Media;

namespace KeySim.Common
{
    public class AppSettings : IAppSettings
    {
        public static void InitializeSettings(IAppSettings settingInstance)
        {
            try
            {
                if (File.Exists(settingInstance.SettingFilePath))
                {
                    var xmlSetting = new XmlDocument();
                    xmlSetting.Load(settingInstance.SettingFilePath);

                    XmlNodeList xmlNodeList = xmlSetting.SelectNodes("/AppSettings/Setting");
                    if (xmlNodeList != null)
                    {
                        foreach (XmlElement element in xmlNodeList)
                        {
                            string keyName = element.GetAttribute("KeyName");
                            if (settingInstance.Settings.ContainsKey(keyName))
                            {
                                string typeName = element.GetAttribute("Type");
                                string attributeValue = element.GetAttribute("Value");
                                if (typeName == typeof(int).FullName)
                                    settingInstance.Settings[keyName] = attributeValue.StringToInt();
                                else if (typeName == typeof(uint).FullName)
                                    settingInstance.Settings[keyName] = attributeValue.StringToUInt();
                                else if (typeName == typeof(long).FullName)
                                    settingInstance.Settings[keyName] = attributeValue.StringToLong();
                                else if (typeName == typeof(ulong).FullName)
                                    settingInstance.Settings[keyName] = attributeValue.StringToULong();
                                else if (typeName == typeof(double).FullName)
                                    settingInstance.Settings[keyName] = attributeValue.StringToDouble();
                                else if (typeName == typeof(float).FullName)
                                    settingInstance.Settings[keyName] = attributeValue.StringToFloat();
                                else if (typeName == typeof(bool).FullName)
                                    settingInstance.Settings[keyName] = attributeValue.StringToBoolean();
                                else if (typeName == typeof(string).FullName)
                                    settingInstance.Settings[keyName] = attributeValue;
                                else if (typeName == typeof(Color).FullName)
                                    settingInstance.Settings[keyName] = attributeValue.StringToColor();
                            }
                        }
                    }
                }
                SaveSettings(settingInstance);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SaveSettings(IAppSettings settingInstance)
        {
            var xmlSetting = new XmlDocument();
            XmlDocumentFragment xmlDocumentFragment = xmlSetting.CreateDocumentFragment();
            XmlElement rootElement = xmlSetting.CreateElement("AppSettings");
            xmlDocumentFragment.AppendChild(rootElement);

            foreach (KeyValuePair<string, object> setting in settingInstance.Settings)
            {
                rootElement.AppendChild(CreateSettingsElement(xmlSetting, setting.Key, setting.Value));
            }

            xmlSetting.AppendChild(rootElement);
            xmlSetting.Save(settingInstance.SettingFilePath);
        }

        private static XmlElement CreateSettingsElement(XmlDocument xmlDoc, string keyName, object value)
        {
            XmlElement settingElement = xmlDoc.CreateElement("Setting");
            settingElement.SetAttribute("KeyName", keyName);
            settingElement.SetAttribute("Value", value.ToString());
            settingElement.SetAttribute("Type", value.GetType().FullName);
            return settingElement;
        }

        #region For instance

        private static AppSettings _instance;

        private AppSettings()
        {
            Settings = new ObservableDictionary<string, object>();

            // Create Settings for AppSettings
            Settings[LAUNCH_ON_SYSUP] = true;
            Settings[GLOBAL_SHORT_MODKEY] = 0u;
            Settings[GLOBAL_SHORT_KEY] = 0u;
        }

        #region Setting fields

        public const string LAUNCH_ON_SYSUP = nameof(LAUNCH_ON_SYSUP);
        public const string GLOBAL_SHORT_MODKEY = nameof(GLOBAL_SHORT_MODKEY);
        public const string GLOBAL_SHORT_KEY = nameof(GLOBAL_SHORT_KEY);
        
        #endregion

        public static AppSettings Instance
        {
            get { return _instance ?? (_instance = new AppSettings()); }
        }

        #region Indexer
        public object this[string keyName]
        {
            get { return Settings.ContainsKey(keyName) ? Settings[keyName] : null; }
            set { if (Settings.ContainsKey(keyName)) Settings[keyName] = value; }
        }
        #endregion

        public string SettingFilePath
        {
            get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "keysim.setting"); }
        }

        public ObservableDictionary<string, object> Settings { get; private set; }

        #endregion
    }

    public interface IAppSettings
    {
        string SettingFilePath { get; }
        ObservableDictionary<string, object> Settings { get; }
    }
}