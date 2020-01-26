using System;
using System.Collections.Generic;

namespace KeyboardSim
{
    public class HotKeyManager
    {
        #region Global members for application
        private static IntPtr MainWindowHandle { get; set; }
        private static Action MainWindowShowAction { get; set; }
        private static int PreviousMainWindowKeyId { get; set; }
        #endregion

        private static IDictionary<int, Action> HotKeys = new Dictionary<int, Action>();

        public static void RegisterMainwindowHotKey(uint modKey, uint key)
        {
            UnregisterMainWindowHotKey();
            PreviousMainWindowKeyId = GetHotKeyID(modKey, key);
            RegisterHotKey(modKey, key, MainWindowShowAction);
        }

        public static void RegisterMainwindowHotKey(IntPtr handle, uint modKey, uint key, Action callback)
        {
            MainWindowHandle = handle;
            MainWindowShowAction = callback;
            UnregisterMainWindowHotKey();
            PreviousMainWindowKeyId = GetHotKeyID(modKey, key);
            RegisterHotKey(modKey, key, callback);
        }

        public static void RegisterHotKey(uint modKey, uint key, Action callback)
        {
            int id = GetHotKeyID(modKey, key);
            if (WinNative.RegisterHotKey(MainWindowHandle, id, modKey, key))
            {
                HotKeys[id] = callback;
            }
        }

        public static void UnregisterMainWindowHotKey()
        {
            if (PreviousMainWindowKeyId != 0)
            {
                WinNative.UnregisterHotKey(MainWindowHandle, PreviousMainWindowKeyId);
            }
        }

        public static void UnregisterHotKey(uint modKey, uint key)
        {
            WinNative.UnregisterHotKey(MainWindowHandle, GetHotKeyID(modKey, key));
        }

        public static void ExecuteHotKey(int id)
        {
            if (HotKeys.ContainsKey(id))
            {
                HotKeys[id].Invoke();
            }
        }

        private static int GetHotKeyID(uint modKey, uint key)
        {
            return (int)((modKey * 10000) + key);
        }
    }
}
