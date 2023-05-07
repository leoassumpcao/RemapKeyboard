using RemapKeyboard.Enums;

namespace RemapKeyboard.Hotkeys
{
    internal abstract class BaseHotkey : IHotkey
    {
        private readonly IList<VKeys> _keys;
        private readonly Action _action;

        public BaseHotkey(IList<VKeys> keys, Action action)
        {
            _keys = keys;
            _action = action;
        }

        public IList<VKeys> Keys
        { get { return _keys; } }

        public void PerformAction()
        {
            _action();
        }

        public string KeysToString()
        {
            return string.Join("+", Keys);
        }
    }
}
