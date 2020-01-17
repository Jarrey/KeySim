using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardSim_Demo
{
    internal class WinHelper
    {
        [DllImport("User32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("User32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("User32.dll")]
        public static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("User32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        public enum WinModKeys : uint
        {
            MOD_ALT = 0x0001,
            MOD_CONTROL = 0x0002,
            MOD_SHIFT = 0x0004,
            MOD_WIN = 0x0008
        }

        public enum WinKeys : uint
        {
            VK_SPACE = 0x20,    // SPACEBAR
            VK_PRIOR = 0x21,    // PAGE UP key
            VK_NEXT = 0x22,     // PAGE DOWN key
            VK_END = 0x23,      // END key
            VK_HOME = 0x24,     // HOME key
            VK_LEFT = 0x25,     // LEFT ARROW key
            VK_UP = 0x26,       // UP ARROW key
            VK_RIGHT = 0x27,    // RIGHT ARROW key
            VK_DOWN = 0x28,     // DOWN ARROW Key
            VK_INSERT = 0x2D,   // INS key
            VK_DELETE = 0x2E,   // DEL key
            VK_0 = 0x30,
            VK_1 = 0x31,
            VK_2 = 0x32,
            VK_3 = 0x33,
            VK_4 = 0x34,
            VK_5 = 0x35,
            VK_6 = 0x36,
            VK_7 = 0x37,
            VK_8 = 0x38,
            VK_9 = 0x39,
            VK_A = 0x41,
            VK_B = 0x42,
            VK_C = 0x43,
            VK_D = 0x44,
            VK_E = 0x45,
            VK_F = 0x46,
            VK_G = 0x47,
            VK_H = 0x48,
            VK_I = 0x49,
            VK_J = 0x4A,
            VK_K = 0x4B,
            VK_L = 0x4C,
            VK_M = 0x4D,
            VK_N = 0x4E,
            VK_O = 0x4F,
            VK_P = 0x50,
            VK_Q = 0x51,
            VK_R = 0x52,
            VK_S = 0x53,
            VK_T = 0x54,
            VK_U = 0x55,
            VK_V = 0x56,
            VK_W = 0x57,
            VK_X = 0x58,
            VK_Y = 0x59,
            VK_Z = 0x5A,
            VK_NUMPAD0 = 0x60,  // Numeric keypad 0 key
            VK_NUMPAD1 = 0x61,  // Numeric keypad 1 key
            VK_NUMPAD2 = 0x62,  // Numeric keypad 2 key
            VK_NUMPAD3 = 0x63,  // Numeric keypad 3 key
            VK_NUMPAD4 = 0x64,  // Numeric keypad 4 key
            VK_NUMPAD5 = 0x65,  // Numeric keypad 5 key
            VK_NUMPAD6 = 0x66,  // Numeric keypad 6 key
            VK_NUMPAD7 = 0x67,  // Numeric keypad 7 key
            VK_NUMPAD8 = 0x68,  // Numeric keypad 8 key
            VK_NUMPAD9 = 0x69,  // Numeric keypad 9 key
            VK_F1 = 0x70,
            VK_F2 = 0x71,
            VK_F3 = 0x72,
            VK_F4 = 0x73,
            VK_F5 = 0x74,
            VK_F6 = 0x75,
            VK_F7 = 0x76,
            VK_F8 = 0x77,
            VK_F9 = 0x78,
            VK_F10 = 0x79,
            VK_F11 = 0x7A,
            VK_F12 = 0x7B
        }
    }
}
