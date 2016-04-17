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
    public partial class FormColumn : Form
    {
        #region Events

        public event EventHandler EventResetColumn;

        #endregion

        public FormColumn()
        {
            InitializeComponent();
        }

        public FormColumn(SoliCap.ColumnSettingMsg msg)
        {
            InitializeComponent();

            //Set String
            CheckVQ.Text = ResSoli.ColumnVQ;
            CheckPeakVQ.Text = ResSoli.ColumnPeakVQ;
            CheckWS.Text = ResSoli.ColumnWS;
            CheckPeakWS.Text = ResSoli.ColumnPeakWS;
            CheckPF.Text = ResSoli.ColumnPF;
            CheckPeakPF.Text = ResSoli.ColumnPeakPF;
            CheckCommit.Text = ResSoli.ColumnCommitVQ;
            CheckReversed.Text = ResSoli.ColumnReverseVQ;
            CheckWaste.Text = ResSoli.ColumnWastedVQ;
            CheckFree.Text = ResSoli.ColumnFreeVQ;
            CheckStartTime.Text = ResSoli.ColumnStartTime;
            CheckExitTime.Text = ResSoli.ColumnExitTime;
            CheckCPU.Text = ResSoli.ColumnCPU_Usage;
            CheckFileName.Text = ResSoli.ColumnFile;

            //Set Status
            CheckVQ.Checked = msg.bIsVQCurrent;
            CheckPeakVQ.Checked = msg.bIsVQPeak;
            CheckWS.Checked = msg.bIsWSCurrent;
            CheckPeakWS.Checked = msg.bIsWSPeak;
            CheckPF.Checked = msg.bIsPFCurrent;
            CheckPeakPF.Checked = msg.bIsPFPeak;
            CheckCommit.Checked = msg.bIsVQCommitted;
            CheckReversed.Checked = msg.bIsVQReversed;
            CheckWaste.Checked = msg.bIsVQWasted;
            CheckFree.Checked = msg.bIsVQFree;
            CheckStartTime.Checked = msg.bIsStartTime;
            CheckExitTime.Checked = msg.bIsExitTime;
            CheckCPU.Checked = msg.bIsCPUUsage;
            CheckFileName.Checked = msg.bIsFileName;

        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            SoliCap.ColumnSettingMsg msg = new ColumnSettingMsg();
            msg.setValue(CheckCPU.Checked,CheckVQ.Checked, CheckPeakVQ.Checked, CheckWS.Checked, CheckPeakWS.Checked,
                        CheckPF.Checked, CheckPeakPF.Checked, CheckCommit.Checked, CheckReversed.Checked,
                        CheckWaste.Checked, CheckFree.Checked,CheckStartTime.Checked,CheckExitTime.Checked,CheckFileName.Checked);
            EventResetColumn(msg, e);
            this.Dispose();
        }

    }
}
