using RemapKeyboard.Enums;
using RemapKeyboard.Hooks;
using RemapKeyboard.Hotkeys;

namespace RemapKeyboard
{
    internal class KeyboardRemapper
    {
        private readonly object _lockHotkeys;
        private readonly IList<IHotkey> _hotkeys;
        private readonly KeyboardHook _keyboardHook;

        public KeyboardRemapper()
        {
            _lockHotkeys = new object();
            _hotkeys = new List<IHotkey>();
            _keyboardHook = new KeyboardHook();
            _keyboardHook.KeyDown += _keyboardHook_KeyDown;
        }

        private void _keyboardHook_KeyDown(Enums.VKeys key, IList<VKeys> alreadyPressedKeys)
        {
            var hotkeys = new List<IHotkey>(Hotkeys);
            foreach (var hotkey in hotkeys)
            {
                if ((hotkey.Keys.Count - 1) == alreadyPressedKeys.Count &&
                    hotkey.Keys.LastOrDefault() == key
                )
                {
                    int extraHotkeysCount = hotkey.Keys.Count - 1;
                    if (extraHotkeysCount > 0)
                    {
                        for (int i = 0; i < extraHotkeysCount; i++)
                        {
                            if (hotkey.Keys[i] != alreadyPressedKeys[i])
                                return;

                        }
                    }

                    hotkey.PerformAction();
                }
            }
        }

        public void Enable()
        {
            _keyboardHook.Install();
        }

        public void Disable()
        {
            _keyboardHook.Uninstall();

        }

        public void AddHotkey(IHotkey hotkey)
        {
            lock (_lockHotkeys)
                _hotkeys.Add(hotkey);
        }

        public void RemoveHotkey(IHotkey hotkey)
        {
            lock (_lockHotkeys)
                _hotkeys.Remove(hotkey);
        }

        public IList<IHotkey> Hotkeys
        { get { lock (_lockHotkeys) return _hotkeys; } }

        public IList<VKeys> PressedKeys
        { get { return _keyboardHook.PressedKeys; } }
    }
}
