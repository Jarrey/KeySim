using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardSim_Demo
{
    internal static class WinNative
    {
        public const int HOTKEY_ID = 9000;
        public const int WM_HOTKEY = 0x0312;
        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_NOACTIVATE = 0x08000000;

        private const string USER32_DLL = "User32.dll";

        [DllImport(USER32_DLL)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport(USER32_DLL)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport(USER32_DLL)]
        public static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport(USER32_DLL)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        public enum ModKeys : uint
        {
            ALT = 0x0001,
            CTL = 0x0002,            
            SHIFT = 0x0004,
            WIN = 0x0008
        }
    }
}
