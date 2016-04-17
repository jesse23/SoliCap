using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SoliCap.Forms
{
    public partial class FormTimer : Form
    {
        #region Data Members

        //Hot Key Container
        private Keys iSysKey;
        private Keys iNormalKey;

        public event EventHandler EventTimerSettingRequest;

        #endregion

        #region Methods

        public FormTimer(TimerSettingMsg msg)
        {
            InitializeComponent();
            Label1.Text = "Timer Hot Key:";

            ComboSysKey_KeyDown(null, new KeyEventArgs(msg.SysKey));
            TextHotKey_KeyDown(null, new KeyEventArgs(msg.NormalKey));

            CheckIsLockBase.Text = "Set memory base when start timer";
            CheckIsLockBase.Checked = msg.bIsLockBaseWhenStart;

            CheckIsStopRefresh.Text = "Stop refresh memory when stop timer";
            CheckIsStopRefresh.Checked = msg.bIsStopRefreshWhenStop;

        }

        private void ComboSysKey_TextChanged(object sender, EventArgs e)
        {
            switch (ComboSysKey.Text)
            {
                case "Ctrl":
                    iSysKey = Keys.Control;
                    break;
                case "Alt":
                    iSysKey = Keys.Alt;
                    break;
                case "Shift":
                    iSysKey = Keys.Shift;
                    break;
                case "N/A":
                    iSysKey = Keys.None;
                    break;
            }
            this.SelectNextControl(ComboSysKey, true, true, false, true);
        }

        private void ComboSysKey_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Control:
                    ComboSysKey.Text = "Ctrl";
                    break;
                case Keys.Shift:
                    ComboSysKey.Text = "Shift";
                    break;
                case Keys.Alt:
                    ComboSysKey.Text = "Alt";
                    break;
                case Keys.None:
                    ComboSysKey.Text = "N/A";
                    break;
            }
            this.SelectNextControl(ComboSysKey, true, true, false, true);
        }

        private void TextHotKey_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode != Keys.ControlKey) && (e.KeyCode != Keys.ShiftKey) && (e.KeyCode != Keys.Menu))
            {
                iNormalKey = e.KeyData;
                //if ((e.KeyData.ToString().ToCharArray()[0] == 'D') && (e.KeyData.ToString().Length == 2))
                //    TextHotKey.Text = new string((e.KeyData.ToString().ToCharArray()[e.KeyData.ToString().Length - 1]),1);
                //else
                TextHotKey.Text = e.KeyCode.ToString();
            }
            this.SelectNextControl(TextHotKey, true, false, false, true);
        }

        #endregion

        private void FormTimer_FormClosing(object sender, FormClosingEventArgs e)
        {
            TimerSettingMsg msg = new TimerSettingMsg();
            msg.SetValue(iSysKey, iNormalKey, CheckIsLockBase.Checked, CheckIsStopRefresh.Checked);
            EventTimerSettingRequest(msg, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
