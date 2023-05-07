using RemapKeyboard.Enums;
using WindowsInput;
using WindowsInput.Native;

namespace RemapKeyboard.Hotkeys
{
    internal class KeyHotkey : BaseHotkey
    {
        static readonly InputSimulator sim = new();
        static readonly IList<VirtualKeyCode> _specialKeys = new List<VirtualKeyCode>
        {
            VirtualKeyCode.CONTROL,
            VirtualKeyCode.LCONTROL,
            VirtualKeyCode.RCONTROL,

            VirtualKeyCode.SHIFT,
            VirtualKeyCode.LSHIFT,
            VirtualKeyCode.RSHIFT,

            VirtualKeyCode.MENU,
            VirtualKeyCode.LMENU,
            VirtualKeyCode.RMENU,
        };

        public KeyHotkey(IList<VKeys> keys, IList<VKeys> remapKeys) : base(keys, () => SpecifiedAction(remapKeys))
        { }

        private static void SpecifiedAction(IList<VKeys> remapKeys)
        {
            var vkeys = ConvertKeys(remapKeys);

            var replacedKeys = ReplaceModifierKeys(vkeys);
            var modifierKeys = GetModifierKeys(replacedKeys);

            if (modifierKeys.Any())
            {
                var commonKeys = GetCommonKeys(replacedKeys);

                sim.Keyboard.ModifiedKeyStroke(
                    modifierKeys,
                    commonKeys
                );
            }
            else
            {
                sim.Keyboard.KeyPress(vkeys.ToArray());
            }
        }

        private static IList<VirtualKeyCode> ConvertKeys(IList<VKeys> remapKeys)
        {
            return remapKeys.Select(x => (VirtualKeyCode)(int)x).ToList();
        }

        private static IList<VirtualKeyCode> ReplaceModifierKeys(IList<VirtualKeyCode> keys)
        {
            var replacedKeys = keys.ToList();

            var specialKeys = new Dictionary<VirtualKeyCode, VirtualKeyCode>
            {
                { VirtualKeyCode.LCONTROL, VirtualKeyCode.CONTROL },
                { VirtualKeyCode.RCONTROL, VirtualKeyCode.CONTROL },
                { VirtualKeyCode.LSHIFT, VirtualKeyCode.SHIFT },
                { VirtualKeyCode.RSHIFT, VirtualKeyCode.SHIFT },
                { VirtualKeyCode.LMENU, VirtualKeyCode.MENU },
                { VirtualKeyCode.RMENU, VirtualKeyCode.MENU },
            };

            for (int i = 0; i < replacedKeys.Count; i++)
            {
                if (specialKeys.TryGetValue(replacedKeys[i], out var replace))
                {
                    replacedKeys[i] = replace;
                }
            }

            return replacedKeys;
        }

        private static IEnumerable<VirtualKeyCode> GetModifierKeys(IEnumerable<VirtualKeyCode> vkeys)
        {
            return vkeys.Where(k =>
                k == VirtualKeyCode.CONTROL ||
                k == VirtualKeyCode.SHIFT ||
                k == VirtualKeyCode.MENU
            );
        }

        private static IEnumerable<VirtualKeyCode> GetCommonKeys(IEnumerable<VirtualKeyCode> vkeys)
        {
            return vkeys.Where(x => !_specialKeys.Contains(x));
        }
    }
}
