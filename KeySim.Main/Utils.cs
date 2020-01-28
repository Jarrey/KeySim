using GregsStack.InputSimulatorStandard;
using GregsStack.InputSimulatorStandard.Native;
using KeyboardSim.Model;
using Microsoft.Win32;
using System;
using System.Windows;
using static KeyboardSim.WinNative;
using sd = System.Drawing;

namespace KeyboardSim
{
    public static class Utils
    {
        private static InputSimulator simulator = new InputSimulator();

        public static uint StringToModKey(this string modKey)
        {
            if (!string.IsNullOrEmpty(modKey))
            {
                if (Enum.TryParse(modKey.ToUpper(), true, out ModKeys k))
                {
                    return (uint)k;
                }
            }
            return 0;
        }

        public static uint StringToKey(this string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                VirtualKeyCode k;
                if (Enum.TryParse("VK_" + key.ToUpper(), true, out k) || Enum.TryParse(key.ToUpper(), true, out k))
                {
                    return (uint)k;
                }
            }
            return 0;
        }

        public static void ExecuteKeyboardCommand(this Tuple<ActionType, string>[] actions)
        {
            if (actions != null)
            {
                foreach (var action in actions)
                {
                    if (action.Item1 == ActionType.I)
                    {
                        simulator.Keyboard.TextEntry(action.Item2);
                    }
                    else if (action.Item1 == ActionType.M)
                    {
                        simulator.Keyboard.KeyDown((VirtualKeyCode)action.Item2.StringToKey());
                    }
                }
            }
        }

        public static void RegisterAutoStartupKey(bool isAutoStart, string keyName)
        {
            RegistryKey StartupPath = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            if (isAutoStart)
            {
                StartupPath.SetValue(keyName, System.Reflection.Assembly.GetEntryAssembly().Location, RegistryValueKind.String);
            }
            else
            {
                StartupPath.DeleteValue(keyName, false);
            }
        }

        public static sd.Point GetDisplayPosition(double marginRight, double marginBottom)
        {
            sd.Point point = simulator.Mouse.Position;
            sd.Point result = point;
            if (point.X >= SystemParameters.WorkArea.Width - marginRight)
            {
                result.X = (int)(result.X - marginRight);
            }

            if (point.Y >= SystemParameters.WorkArea.Height - marginBottom)
            {
                result.Y = (int)(result.Y - marginBottom);
            }

            return result;
        }

        public static string ParseMIMEType(string mime)
        {
            var types = mime?.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            if (types != null && types.Length>1)
            {
                return types[1];
            }
            return null;
        }
    }
}
