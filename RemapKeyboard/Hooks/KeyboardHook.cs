using RemapKeyboard.Enums;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace RemapKeyboard.Hooks
{
    /// <summary>
    /// Class for intercepting low level keyboard hooks
    /// </summary>
    public class KeyboardHook
    {
        /// <summary>
        /// Internal callback processing function
        /// </summary>
        private delegate IntPtr KeyboardHookHandler(int nCode, IntPtr wParam, IntPtr lParam);
        private KeyboardHookHandler hookHandler;
        private readonly object _lockPressedKeys;
        private readonly IList<VKeys> _pressedKeys;

        /// <summary>
        /// Function that will be called when defined events occur
        /// </summary>
        /// <param name="key">VKeys</param>
        public delegate void KeyboardHookCallback(VKeys key, IList<VKeys> alreadyPressedKeys);

        #region Events
        public event KeyboardHookCallback KeyDown;
        public event KeyboardHookCallback KeyUp;
        #endregion

        /// <summary>
        /// Hook ID
        /// </summary>
        private IntPtr hookID = IntPtr.Zero;

        public KeyboardHook()
        {
            _lockPressedKeys = new object();
            _pressedKeys = new List<VKeys>();
        }

        /// <summary>
        /// Install low level keyboard hook
        /// </summary>
        public void Install()
        {
            if (hookID != IntPtr.Zero)
                return;

            hookHandler = HookFunc;
            hookID = SetHook(hookHandler);
        }

        /// <summary>
        /// Remove low level keyboard hook
        /// </summary>
        public void Uninstall()
        {
            if (hookID == IntPtr.Zero)
                return;

            UnhookWindowsHookEx(hookID);
        }

        /// <summary>
        /// Registers hook with Windows API
        /// </summary>
        /// <param name="proc">Callback function</param>
        /// <returns>Hook ID</returns>
        private IntPtr SetHook(KeyboardHookHandler proc)
        {
            using (ProcessModule module = Process.GetCurrentProcess().MainModule)
                return SetWindowsHookEx(13, proc, GetModuleHandle(module.ModuleName), 0);
        }

        /// <summary>
        /// Default hook call, which analyses pressed keys
        /// </summary>
        private IntPtr HookFunc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                int iwParam = wParam.ToInt32();

                if (iwParam == WM_KEYDOWN || iwParam == WM_SYSKEYDOWN)
                {
                    var key = (VKeys)Marshal.ReadInt32(lParam);

                    KeyDown?.Invoke(key, PressedKeys);

                    AddPressedKey(key);
                }
                else if (iwParam == WM_KEYUP || iwParam == WM_SYSKEYUP)
                {
                    var key = (VKeys)Marshal.ReadInt32(lParam);

                    KeyUp?.Invoke(key, PressedKeys);

                    RemovePressedKey(key);
                }
            }

            return CallNextHookEx(hookID, nCode, wParam, lParam);
        }

        private void AddPressedKey(VKeys key)
        {
            lock (_lockPressedKeys)
            {
                if (!_pressedKeys.Contains(key))
                    _pressedKeys.Add(key);
            }
        }

        private void RemovePressedKey(VKeys key)
        {
            lock (_lockPressedKeys)
            {
                _pressedKeys.Remove(key);
            }
        }

        public IList<VKeys> PressedKeys
        {
            get { lock (_lockPressedKeys) return _pressedKeys.ToList(); }
        }

        /// <summary>
        /// Destructor. Unhook current hook
        /// </summary>
        ~KeyboardHook()
        {
            Uninstall();
        }

        /// <summary>
        /// Low-Level function declarations
        /// </summary>
        #region WinAPI
        private const int WM_KEYDOWN = 0x100;
        private const int WM_SYSKEYDOWN = 0x104;
        private const int WM_KEYUP = 0x101;
        private const int WM_SYSKEYUP = 0x105;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, KeyboardHookHandler lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
        #endregion
    }
}