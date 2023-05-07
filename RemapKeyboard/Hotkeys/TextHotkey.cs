using RemapKeyboard.Enums;
using WindowsInput;

namespace RemapKeyboard.Hotkeys
{
    internal class TextHotkey : BaseHotkey
    {
        static readonly InputSimulator sim = new();

        public TextHotkey(IList<VKeys> keys, string text) : base(keys, () => SpecifiedAction(text))
        { }

        private static void SpecifiedAction(string text)
        {
            sim.Keyboard.TextEntry(text);
        }
    }
}
