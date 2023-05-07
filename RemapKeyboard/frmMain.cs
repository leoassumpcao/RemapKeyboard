using RemapKeyboard.Enums;
using RemapKeyboard.Hotkeys;
using System.Data;
using System.Windows.Forms;

namespace RemapKeyboard
{
    public partial class frmMain : Form
    {
        private readonly KeyboardRemapper _keyboardRemapper;

        private IList<VKeys> _pressedKeys;
        private bool _autoRefreshKeys = false;

        public frmMain()
        {
            InitializeComponent();

            _pressedKeys = new List<VKeys>();

            _keyboardRemapper = new KeyboardRemapper();
            _keyboardRemapper.Enable();
        }

        ~frmMain()
        {
            notifyIcon.Visible = false;
            _keyboardRemapper.Disable();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (File.Exists("hotkeys.xml"))
            {
                var hotkeys = XmlReader.GetHotkeysFromFile("hotkeys.xml");
                hotkeys.ForEach(hk => _keyboardRemapper.AddHotkey(hk));
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (_keyboardRemapper is not null)
            {
                var pressedKeys = _keyboardRemapper.PressedKeys;
                if (_autoRefreshKeys || pressedKeys.Count > _pressedKeys.Count)
                {
                    lblPressedKeys.Text = FormatKeys(pressedKeys);
                }
                _pressedKeys = pressedKeys;
            }
        }

        private void frmMain_KeyUp(object sender, KeyEventArgs e)
        {
            if (_keyboardRemapper is not null)
            {
                var pressedKeys = _keyboardRemapper.PressedKeys;
                if (_autoRefreshKeys)
                {
                    lblPressedKeys.Text = FormatKeys(pressedKeys);
                }
                _pressedKeys = pressedKeys;
            }
        }

        private void chkAutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            _autoRefreshKeys = chkAutoRefresh.Checked;
            if (_autoRefreshKeys && _keyboardRemapper is not null)
            {
                var pressedKeys = _keyboardRemapper.PressedKeys;
                lblPressedKeys.Text = FormatKeys(pressedKeys);
                _pressedKeys = pressedKeys;
            }
        }

        private string FormatKeys(IList<VKeys> keys)
        {
            return string.Join("+", keys.Select(a => a.ToString()));
        }

        private void bttCopyKeysToClipboard_Click(object sender, EventArgs e)
        {
            WindowsClipboard.SetText(lblPressedKeys.Text);
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            BringToFront();
            this.WindowState = FormWindowState.Normal;
            Activate();
            Focus();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            BringToFront();
            this.WindowState = FormWindowState.Normal;
            Activate();
            Focus();
        }
    }
}
