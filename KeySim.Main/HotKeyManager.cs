using System;
using System.Collections.Generic;

namespace KeyboardSim
{
    public class HotKeyManager
    {
        #region Global members for application
        public static IntPtr MainWindowHandle { get; set; }
        #endregion

        private static IDictionary<int, Action> HotKeys = new Dictionary<int, Action>();

        public static void RegisterHotKey(uint modKey, uint key, Action callback)
        {
            int id = GetHotKeyID(modKey, key);
            if (WinNative.RegisterHotKey(MainWindowHandle, id, modKey, key))
            {
                HotKeys[id] = callback;
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
