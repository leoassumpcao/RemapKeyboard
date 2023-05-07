using RemapKeyboard.Enums;

namespace RemapKeyboard.Hotkeys
{
    public interface IHotkey
    {
        public IList<VKeys> Keys { get; }
        public void PerformAction();

        public string KeysToString();
    }
}
