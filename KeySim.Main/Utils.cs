using GregsStack.InputSimulatorStandard;
using GregsStack.InputSimulatorStandard.Native;
using KeyboardSim.Model;
using System;
using static KeyboardSim.WinNative;

namespace KeyboardSim
{
    internal class Utils
    {
        public static uint ConvertModKey(string modKey)
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

        public static uint ConvertKey(string key)
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

        private static InputSimulator simulator = new InputSimulator();
        public static void ExecuteKeyboardCommand(Tuple<ActionType, string>[] actions)
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
                        simulator.Keyboard.KeyDown((VirtualKeyCode)ConvertKey(action.Item2));
                    }
                }
            }
        }
    }
}
