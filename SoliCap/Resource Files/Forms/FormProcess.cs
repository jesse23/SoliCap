using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace SoliCap.Forms
{
    public partial class FormProcess : Form
    {

        #region Events

        public event EventHandler EventRequestAddProcess;

        #endregion

        #region Properties

        #endregion

        #region Methods
        public FormProcess()
        {
            InitializeComponent();
            foreach (Process p in Process.GetProcesses())
            {
                ListSelect.Items.Add((p.ProcessName + " (ID:" + p.Id.ToString() + ")"));
            }
        }

        private void RadioID_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioID.Checked == true)
                TextPID.Enabled = true;
            else
            {
                TextPID.Text = null;
                TextPID.Enabled = false;
            }
        }

        private void RadioName_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioName.Checked == true)
            {
                TextName.Enabled = true;

            }
            else
            {
                TextName.Text = null;
                TextName.Enabled = false;
            }
        }

        private void RadioSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioSelect.Checked == true)
            {
                ListSelect.Enabled = true;
            }
            else
            {
                ListSelect.SelectedItems.Clear();
                ListSelect.Enabled = false;
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (TextName.Enabled == true)
            {
                if (TextName.Text != "")
                {
                    EventRequestAddProcess(TextName.Text, e);
                    TextName.SelectAll();
                    return;
                }
            }
            else if (TextPID.Enabled == true)
            {
                if (TextPID.Text != "")
                {
                    EventRequestAddProcess(TextPID.Text, e);
                    TextPID.Text = null;
                    return;
                }
            }
            else if (ListSelect.Enabled == true)
            {
                foreach (object st in ListSelect.SelectedItems)
                {
                    string s = (string)st;
                    int startIndex = s.IndexOf("(ID:") + 4;
                    int endIndex = s.LastIndexOf(")");
                    s = s.Substring(startIndex, endIndex - startIndex);
                    EventRequestAddProcess(s, e);
                }
                ListSelect.SelectedItems.Clear();
                
            }
 
            
        }
        #endregion

        private void TextPID_Leave(object sender, EventArgs e)
        {
            if (TextPID.Text != "")
            {
                try
                {
                    Convert.ToInt32(TextPID.Text);
                }
                catch
                {
                    MessageBox.Show("Illegal value, please input a number", "SoliCap");
                    TextPID.Text = null;
                }
            }
        }

        private void TextName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnAdd.PerformClick();
            }
        }

        private void TextPID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    Convert.ToInt32(TextPID.Text);
                    BtnAdd.PerformClick();
                }
                catch
                {
                    MessageBox.Show("Illegal value, please input a number", "SoliCap");
                    TextPID.Text = null;
                }
            }
        }

    }
}
