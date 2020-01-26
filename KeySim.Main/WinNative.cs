using GregsStack.InputSimulatorStandard.Native;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace KeyboardSim
{
    public static class WinNative
    {
        public const int WM_HOTKEY = 0x0312;
        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_NOACTIVATE = 0x08000000;

        private const string USER32_DLL = "User32.dll";

        [DllImport(USER32_DLL)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint mod, uint vk);

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

        public static IEnumerable<Tuple<VirtualKeyCode, string>> NormalKeys => new Tuple<VirtualKeyCode, string>[]
        {
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.SPACE, "SPACE"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.BACK, "BACK"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.TAB, "TAB"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.LEFT, "LEFT"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.UP, "UP"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.DOWN, "DOWN"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.RIGHT, "RIGHT"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_0, "0"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_1, "1"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_2, "2"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_3, "3"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_4, "4"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_5, "5"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_6, "6"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_7, "7"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_8, "8"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_9, "9"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_A, "A"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_B, "B"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_C, "C"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_D, "D"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_E, "E"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_F, "F"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_G, "G"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_H, "H"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_I, "I"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_J, "J"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_K, "K"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_L, "L"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_M, "M"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_N, "N"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_O, "O"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_P, "P"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_Q, "Q"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_R, "R"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_S, "S"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_T, "T"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_U, "U"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_V, "V"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_W, "W"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_X, "X"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_Y, "Y"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.VK_Z, "Z"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.F1, "F1"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.F2, "F2"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.F3, "F3"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.F4, "F4"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.F5, "F5"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.F6, "F6"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.F7, "F7"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.F8, "F8"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.F9, "F9"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.F10, "F10"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.F11, "F11"),
            new Tuple<VirtualKeyCode, string>(VirtualKeyCode.F12, "F12"),
        };
    }
}
