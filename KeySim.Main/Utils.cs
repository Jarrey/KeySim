using GregsStack.InputSimulatorStandard.Native;
using System;
using static KeyboardSim_Demo.WinNative;

namespace KeyboardSim_Demo
{
    internal class Utils
    {
        public static void RegisterHotKey(IntPtr handle, uint modKey, uint key)
        {
            if (!WinNative.RegisterHotKey(handle, HOTKEY_ID, modKey, key))
            {
                // handle error
            }
        }

        public static void UnregisterHotKey(IntPtr handle)
        {
            WinNative.UnregisterHotKey(handle, HOTKEY_ID);
        }

        public static uint ConvertModKey(string modKey)
        {
            if (!string.IsNullOrEmpty(modKey))
            {
                if (Enum.TryParse(modKey, true, out ModKeys k))
                {
                    return (uint)k;
                }
            }
            return 0;
        }
        public static uint ConvertKey(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                VirtualKeyCode k;
                if (Enum.TryParse(key, true, out k) || Enum.TryParse("VK_" + key, true, out k))
                {
                    return (uint)k;
                }
            }
            return 0;
        }
    }
}
