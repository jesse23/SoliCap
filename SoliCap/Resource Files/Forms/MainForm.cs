using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using SoliCap.Forms; 
using KeyHookLib;
using System.Xml;

namespace SoliCap
{
    public partial class MainForm : Form
    {
        #region Data Members

        //Two sub forms.
        private FormProcess oFormProcess;
        private FormColumn oFormColumn;
        private FormHelp oFormHelp;
        private FormAbout oFormAbout;
        private FormTimer oFormTimer;

        //KeyHook
        private ExternalKeyHook oHook;

        //MemDetector
        private List<MemProbe> oProbes;

        private StringDataTable oTable;

        //Memory Unit Type
        private MemUnitType eUnitType;

        //Column Setting Message
        private ColumnSettingMsg ColSetMsg;

        /// <summary>
        /// When runing a long process, we will set this variable as true to prevent update request from other thead.
        /// </summary>
        private bool bIsProtected = false;

        /// <summary>
        /// The tag for auto renew.
        /// </summary>
        private bool bIsAutoRenew = false;

        /// <summary>
        /// The tag for auto add.
        /// </summary>
        private bool bIsAutoAdd = false;

        /// <summary>
        /// Tag for user timer.
        /// </summary>
        private bool bIsTiming = false;

        /// <summary>
        /// The time base for auto add.
        /// </summary>
        private DateTime TimeAddBase;

        /// <summary>
        /// The time base for test process.
        /// </summary>
        private DateTime TimeTestBase;

        //Timer 
        private Timer oTimer;

        //Timer Setting Msg
        private TimerSettingMsg TimerSetMsg;

        //System info
        APIUtls.MEMORYSTATUSEX mem_info;

        #endregion

        #region Methods

        #region Sub Dialog Handler

        /// <summary>
        /// When Click "Process..." button in menu.
        /// </summary>
        private void addProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bIsProtected = true;
            oFormProcess = new FormProcess();
            oFormProcess.EventRequestAddProcess += AddMemberHandler;
            if (MenuTopMost.Checked == true)
            {
                oFormProcess.TopMost = true;
            }
            oFormProcess.ShowDialog();
            oFormProcess.Dispose();
            oFormProcess = null;
            bIsProtected = false;
        }

        /// <summary>
        /// When Click "Column..." button in menu.
        /// </summary>
        private void columnsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bIsProtected = true;
            oFormColumn = new FormColumn(ColSetMsg);
            oFormColumn.EventResetColumn += ResetTableColumn;
            if (MenuTopMost.Checked == true)
            {
                oFormColumn.TopMost = true;
            }
            oFormColumn.ShowDialog();
            oFormColumn.Dispose();
            oFormColumn = null;
            bIsProtected = false;
        }

        /// <summary>
        /// Help handler
        /// </summary>
        private void MenuHelp_Click(object sender, EventArgs e)
        {
            bIsProtected = true;
            MenuTopMost.Checked = false;
            oFormHelp = new FormHelp();
            if (MenuTopMost.Checked == true)
            {
                oFormHelp.TopMost = true;
            }
            oFormHelp.ShowDialog();
            oFormHelp.Dispose();
            oFormHelp = null;
            bIsProtected = false;
        }

        /// <summary>
        /// About Message
        /// </summary>
        private void MenuAbout_Click(object sender, EventArgs e)
        {
            bIsProtected = true;
            MenuTopMost.Checked = false;
            oFormAbout = new FormAbout();
            if (MenuTopMost.Checked == true)
            {
                oFormAbout.TopMost = true;
            }
            oFormAbout.ShowDialog();
            oFormAbout.Dispose();
            oFormAbout = null;
            bIsProtected = false;
        }

        private void MenuTimerOption_Click(object sender, EventArgs e)
        {
            bIsProtected = true;
            MenuTopMost.Checked = false;
            oFormTimer = new FormTimer(TimerSetMsg);
            oFormTimer.EventTimerSettingRequest += new EventHandler(oFormTimer_EventTimerSettingRequest);
            if (MenuTopMost.Checked == true)
            {
                oFormAbout.TopMost = true;
            }
            oFormTimer.ShowDialog();
            oFormTimer.Dispose();
            oFormTimer = null;
            bIsProtected = false;
        }

        private void oFormTimer_EventTimerSettingRequest(object sender, EventArgs e)
        {
            TimerSetMsg = (TimerSettingMsg)sender;
        }

        /// <summary>
        /// Handler for Add member command from Add Process dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddMemberHandler(object sender, EventArgs e)
        {
            bIsProtected = true;
            string s = (string)sender;
            DataRow row;
            MemProbe op;
            int id = -1;
            try
            {
                id = Convert.ToInt32(s);
            }
            catch
            {
                //If cannot convert to integer, Add it as name.
                op = new MemProbe(s);
                row = oTable.NewRow();
                oTable.Rows.Add(row);
                op.CatchNew(ref row, ColSetMsg);
                oProbes.Add(op);
                bIsProtected = false;
                return;
            }

            //Can get ID. Find process by ID.
            foreach (int i in MemProbe.ProbeIDs)
            {
                if (id == i)
                {
                    MessageBox.Show("Process is exist in the list.", "SoliCap");
                    bIsProtected = false;
                    return;
                }
            }

            op = new MemProbe(id);
            row = oTable.NewRow();
            oTable.Rows.Add(row);
            op.Update(ref row, ColSetMsg);
            oProbes.Add(op);
            bIsProtected = false;
        }

        /// <summary>
        /// Steps that reset the column.
        /// </summary>
        /// <param name="sender">Column Setting Message.</param>
        /// <param name="e"></param>
        private void ResetTableColumn(object sender, EventArgs e)
        {
            bIsProtected = true;
            ColumnSettingMsg msg = (ColumnSettingMsg)sender;
            ColSetMsg = msg;

            SetGridView(ColSetMsg);
            RefreshTable();
            bIsProtected = false;
        }

        #endregion

        #region Menu Command Handler

        /// <summary>
        /// Refresh Memory when click refresh in menu
        /// </summary>
        private void refreshNowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshCmd();
        }

        /// <summary>
        /// Refresh all dead items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void renewAllExitProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < oProbes.Count; i++)
            {
                if (oProbes[i].ProbeStatus == ProbeStatus.Dead)
                {
                    oTable.Rows.RemoveAt(i);
                    oTable.Rows.InsertAt(oTable.NewRow(), i);
                    DataRow row = oTable.Rows[i];
                    oProbes[i].Reborn(ref row, ColSetMsg);
                }
            }
        }

        /// <summary>
        /// Handler that cleart all processes command
        /// </summary>
        private void MenuClearAllProcess_Click(object sender, EventArgs e)
        {
            bIsProtected = true;
            for (int i = oProbes.Count - 1; (i >= 0) && (oProbes.Count != 0); i--)
            {
                oProbes[i].Dispose();
                oProbes.RemoveAt(i);
                GridView.Rows.RemoveAt(i);
            }
            bIsProtected = false;
        }

        /// <summary>
        /// Lock Memory Base
        /// </summary>
        private void MenuLockBase_Click(object sender, EventArgs e)
        {
            MenuLockBase.Checked = !(MenuLockBase.Checked);
            MemProbe.isLockBase = MenuLockBase.Checked;
            if (MenuLockBase.Checked)
            {
                bIsProtected = true;
                MemProbe.isLockBase = true;
                foreach (MemProbe op in oProbes)
                {
                    op.SetBase();
                }
                
                bIsProtected = false;
            }
        }

        /// <summary>
        /// Menu Timer Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuTimer_Click(object sender, EventArgs e)
        {
            bIsTiming = !bIsTiming;
            if (bIsTiming)
            {
                TimeTestBase = DateTime.Now;
                if (TimerSetMsg.bIsLockBaseWhenStart)
                {
                    if (MenuLockBase.Checked)
                    {
                        MenuLockBase.Checked = false;
                        MenuLockBase_Click(null, null);
                    }
                    else
                        MenuLockBase_Click(null, null);
                }
                this.Text += " (Timing)";
                MenuTimer.Text = "End Timer";
                MenuTimerOption.Enabled = false;
            }
            else
            {
                MessageBox.Show((DateTime.Now - TimeTestBase).ToString(), "SoliCap");
                if (TimerSetMsg.bIsStopRefreshWhenStop)
                {
                    SetTimer(MenuSpeedManual, null);
                }
                this.Text = "SoliCap";
                MenuTimer.Text = "Start Timer";
                MenuTimerOption.Enabled = true;
            }
        }

        /// <summary>
        /// Always on the top option
        /// </summary>
        private void MenuTopMost_Click_1(object sender, EventArgs e)
        {
            MenuTopMost.Checked = !(MenuTopMost.Checked);
            this.TopMost = MenuTopMost.Checked;
        }

        /// <summary>
        /// When Checked, Auto Renew.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void renewExitWhenRefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuAutoRenew.Checked = !(MenuAutoRenew.Checked);
            bIsAutoRenew = MenuAutoRenew.Checked;
        }

        /// <summary>
        /// If Checked, New process will be added to the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void autoAddNewProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuAutoAdd.Checked = !(MenuAutoAdd.Checked);
            bIsAutoAdd = MenuAutoAdd.Checked;
            if (bIsAutoAdd == true)
                TimeAddBase = DateTime.Now;
        }

        /// <summary>
        /// Set memory unit when change it in menu.
        /// </summary>
        private void SetMemUnit(object sender, EventArgs e)
        {
            //Set Menu Status
            MenuUnitB.Checked = false;
            MenuUnitKB.Checked = false;
            MenuUnitMB.Checked = false;
            MenuUnitGB.Checked = false;
            MenuUnitAuto.Checked = false;
            ((ToolStripMenuItem)sender).Checked = true;

            //Set Memory Unit Type
            MemUnitType eType = new MemUnitType();
            if (MenuUnitAuto.Checked == true)
                eType = MemUnitType.Smart;
            else if (MenuUnitGB.Checked == true)
                eType = MemUnitType.GB;
            else if (MenuUnitMB.Checked == true)
                eType = MemUnitType.MB;
            else if (MenuUnitKB.Checked == true)
                eType = MemUnitType.KB;
            else if (MenuUnitB.Checked == true)
                eType = MemUnitType.B;

            bIsProtected = true;
            MemProbe.eUnitType = eType;
            eUnitType = eType;
            RefreshTable();
            RefreshSysMem(true);
            bIsProtected = false;
        }

        /// <summary>
        /// Hander for refresh speed check menu.
        /// </summary>
        private void SetTimer(object sender, EventArgs e)
        {
            MenuSpeedHigh.Checked = false;
            MenuSpeedNormal.Checked = false;
            MenuSpeedLow.Checked = false;
            MenuSpeedVeryLow.Checked = false;
            MenuSpeedManual.Checked = false;
            ((ToolStripMenuItem)sender).Checked = true;

            //Set Timer by menu setting
            if (MenuSpeedManual.Checked == true)
            {
                oTimer.Stop();
            }
            else if (MenuSpeedHigh.Checked == true)
            {
                oTimer.Interval = 500;
                if (oTimer.Enabled == false)
                    oTimer.Start();
            }
            else if (MenuSpeedNormal.Checked == true)
            {
                oTimer.Interval = 1000;
                if (oTimer.Enabled == false)
                    oTimer.Start();
            }
            else if (MenuSpeedLow.Checked == true)
            {
                oTimer.Interval = 5000;
                if (oTimer.Enabled == false)
                    oTimer.Start();
            }
            else if (MenuSpeedVeryLow.Checked == true)
            {
                oTimer.Interval = 30000;
                if (oTimer.Enabled == false)
                    oTimer.Start();
            }
        }

        /// <summary>
        /// Hot Key Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void oHook_KeyDown(object sender, KeyEventArgs e)
        {
            if (!bIsProtected)
            {
                Keys iHotKey;

                if (TimerSetMsg.SysKey == Keys.None)
                    iHotKey = TimerSetMsg.NormalKey;
                else
                    iHotKey = TimerSetMsg.SysKey | TimerSetMsg.NormalKey;
                if (e.KeyData == iHotKey)
                {
                    MenuTimer_Click(null, null);
                }
            }
        }

        #endregion

        #region Handler in Table Grid
        /// <summary>
        /// Handler when delete rows in grid view.
        /// </summary>
        private void GridView_UserDeletingRow_1(object sender, DataGridViewRowCancelEventArgs e)
        {
            int i = oTable.Rows.IndexOf(((DataRowView)e.Row.DataBoundItem).Row);
            oProbes[i].Dispose();
            oProbes.RemoveAt(i);
        }

        /// <summary>
        /// Handler for delete command in context menu
        /// </summary>
        private void CtxMenuDelete_Click(object sender, EventArgs e)
        {
            bIsProtected = true;
            foreach (DataGridViewRow es in GridView.SelectedRows)
            {
                int i = oTable.Rows.IndexOf(((DataRowView)es.DataBoundItem).Row);
                oProbes[i].Dispose();
                oProbes.RemoveAt(i);
                GridView.Rows.Remove(es);
            }
            bIsProtected = false;
        }

        /// <summary>
        /// Hander for kill process command in context menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CtxMenuKill_Click(object sender, EventArgs e)
        {
            bIsProtected = true;
            foreach (DataGridViewRow es in GridView.SelectedRows)
            {
                int i = oTable.Rows.IndexOf(((DataRowView)es.DataBoundItem).Row);
                try
                {
                    oProbes[i].Kill();
                }
                catch
                {
                    continue;
                }
                oProbes[i].Dispose();
                oProbes.RemoveAt(i);
                GridView.Rows.Remove(es);
            }
            bIsProtected = false;
        }

        /// <summary>
        /// Invert Select function
        /// </summary>
        private void CtxInvSelect_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection es = GridView.SelectedRows;
            GridView.SelectAll();
            foreach (DataGridViewRow p in es)
            {
                p.Selected = false;
            }
        }

        /// <summary>
        /// When Click Renew command in context menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CtxMenuRenew_Click(object sender, EventArgs e)
        {
            bIsProtected = true;
            DataGridViewSelectedRowCollection es = GridView.SelectedRows;
            foreach (DataGridViewRow p in es)
            {
                DataRow row = ((DataRowView)p.DataBoundItem).Row;
                int i = oTable.Rows.IndexOf(row);
                if (oProbes[i].ProbeStatus == ProbeStatus.Dead)
                {
                    oTable.Rows.RemoveAt(i);
                    oTable.Rows.InsertAt(oTable.NewRow(), i);
                    row = oTable.Rows[i];
                    oProbes[i].Reborn(ref row, ColSetMsg);
                }
                p.Selected = false;
            }
            
            bIsProtected = false;
        }
        #endregion

        #region MainForm Handler

        /// <summary>
        /// Instructor
        /// </summary>
        public MainForm()
        {

            #region New Components

            InitializeComponent();

            //Load Settings from XML file
            #region Load Settings from XML file
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            try
            {
                doc.Load("Settings.xml");

                //Load Column Settings from XML
                XmlNode root = doc.SelectSingleNode("Settings");
                ColSetMsg.bIsCPUUsage = Convert.ToBoolean(root.SelectSingleNode("bIsCPUUsage").InnerText);
                ColSetMsg.bIsVQCurrent = Convert.ToBoolean(root.SelectSingleNode("bIsVQCurrent").InnerText);
                ColSetMsg.bIsVQPeak = Convert.ToBoolean(root.SelectSingleNode("bIsVQPeak").InnerText);
                ColSetMsg.bIsWSCurrent = Convert.ToBoolean(root.SelectSingleNode("bIsWSCurrent").InnerText);
                ColSetMsg.bIsWSPeak = Convert.ToBoolean(root.SelectSingleNode("bIsWSPeak").InnerText);
                ColSetMsg.bIsPFCurrent = Convert.ToBoolean(root.SelectSingleNode("bIsPFCurrent").InnerText);
                ColSetMsg.bIsPFPeak = Convert.ToBoolean(root.SelectSingleNode("bIsPFPeak").InnerText);
                ColSetMsg.bIsVQCommitted = Convert.ToBoolean(root.SelectSingleNode("bIsVQCommitted").InnerText);
                ColSetMsg.bIsVQReversed = Convert.ToBoolean(root.SelectSingleNode("bIsVQReversed").InnerText);
                ColSetMsg.bIsVQWasted = Convert.ToBoolean(root.SelectSingleNode("bIsVQWasted").InnerText);
                ColSetMsg.bIsVQFree = Convert.ToBoolean(root.SelectSingleNode("bIsVQFree").InnerText);
                ColSetMsg.bIsStartTime = Convert.ToBoolean(root.SelectSingleNode("bIsStartTime").InnerText);
                ColSetMsg.bIsExitTime = Convert.ToBoolean(root.SelectSingleNode("bIsExitTime").InnerText);
                ColSetMsg.bIsFileName = Convert.ToBoolean(root.SelectSingleNode("bIsFileName").InnerText);
                
                //Load Timer Settings from XML
                TimerSetMsg.SysKey = (Keys)(Convert.ToInt32(root.SelectSingleNode("SysKey").InnerText));
                TimerSetMsg.NormalKey = (Keys)(Convert.ToInt32(root.SelectSingleNode("NormalKey").InnerText));
                TimerSetMsg.bIsLockBaseWhenStart = Convert.ToBoolean(root.SelectSingleNode("bIsLockBaseWhenStart").InnerText);
                TimerSetMsg.bIsStopRefreshWhenStop = Convert.ToBoolean(root.SelectSingleNode("bIsStopRefreshWhenStop").InnerText);

                //Load Memory Unit Setting from XML
                switch (root.SelectSingleNode("eUnitType").InnerText)
                {
                    case "Smart":
                        eUnitType = MemUnitType.Smart;
                        break;
                    case "GB":
                        eUnitType = MemUnitType.GB;
                        break;
                    case "MB":
                        eUnitType = MemUnitType.MB;
                        break;
                    case "KB":
                        eUnitType = MemUnitType.KB;
                        break;
                    case "B":
                        eUnitType = MemUnitType.B;
                        break;
                }


            }
            catch
            {

            }
            #endregion

            if (TimerSetMsg.NormalKey == Keys.None)
            {
                //Initial Memory Data Table.
                ColSetMsg.setValue(true, true, false, true, true, false, false, false, false, false, false, false, false, false);

                //Initial Timer Setting Message
                TimerSetMsg.SetValue(Keys.None, Keys.F3, true, false);

                eUnitType = MemUnitType.MB;
            }

            oTable = new StringDataTable();
            GridView.DataSource = oTable;

            //Initialize key hook
            oHook = new ExternalKeyHook(false);
            oHook.KeyDown += new KeyEventHandler(oHook_KeyDown);
            

            //Set Grid View based on Column Setting
            SetGridView(ColSetMsg);

            //Timer
            oTimer = new Timer();
            oTimer.Interval = 1000;
            oTimer.Tick += refreshNowToolStripMenuItem_Click;

            

            //Initial MemProbe
            
            MemProbe.Initialize();
            oProbes = new List<MemProbe>();

            #endregion

            #region Initialize MainForm Control

            //Set Icon
            this.Icon = new System.Drawing.Icon(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("SoliCap.Resource_Files.Icons.IconSoliCap.ico"));

            //Menu Set Unit
            MenuUnitAuto.Click += SetMemUnit;
            MenuUnitGB.Click += SetMemUnit;
            MenuUnitMB.Click += SetMemUnit;
            MenuUnitKB.Click += SetMemUnit;
            MenuUnitB.Click += SetMemUnit;

            //Menu Set Timer Speed
            MenuSpeedManual.Click += SetTimer;
            MenuSpeedHigh.Click += SetTimer;
            MenuSpeedNormal.Click += SetTimer;
            MenuSpeedLow.Click += SetTimer;
            MenuSpeedVeryLow.Click += SetTimer;

            //New system memory info struct
            mem_info = new APIUtls.MEMORYSTATUSEX();

            GridView.Columns[0].FillWeight = 150;

            #endregion

        }

        /// <summary>
        /// Invoke Timer when main is loaded.
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            RefreshSysMem(true);
            oTimer.Start();
            oHook.Start();
            this.AutoSize = false;
        }

        /// <summary>
        /// Dispose all components
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Timer
            if (oTimer.Enabled == true)
                oTimer.Stop();
            oTimer.Dispose();
            oTimer = null;

            //KeyHook
            oHook.StopMe();
            oHook = null;

            oProbes.Clear();
            oProbes = null;

            oTable.Dispose();
            oTable = null;

            MemProbe.Finish();

            mem_info = null;

            #region Write XML File

            //Writing Setting to XML
            XmlTextWriter textWriter = new XmlTextWriter("Settings.xml", null);
            textWriter.Formatting = Formatting.Indented;

            // Start Writing. Writing start document
            textWriter.WriteStartDocument();

            // Write comments
            textWriter.WriteComment("SoliCap Setting Files");

            //Writing column setting nodes.
            textWriter.WriteComment("Column Settings");
            textWriter.WriteStartElement("Settings");
            textWriter.WriteElementString("bIsCPUUsage", ColSetMsg.bIsCPUUsage.ToString());
            textWriter.WriteElementString("bIsVQCurrent", ColSetMsg.bIsVQCurrent.ToString());
            textWriter.WriteElementString("bIsVQPeak", ColSetMsg.bIsVQPeak.ToString());
            textWriter.WriteElementString("bIsWSCurrent", ColSetMsg.bIsWSCurrent.ToString());
            textWriter.WriteElementString("bIsWSPeak", ColSetMsg.bIsWSPeak.ToString());
            textWriter.WriteElementString("bIsPFCurrent", ColSetMsg.bIsPFCurrent.ToString());
            textWriter.WriteElementString("bIsPFPeak", ColSetMsg.bIsPFPeak.ToString());
            textWriter.WriteElementString("bIsVQCommitted", ColSetMsg.bIsVQCommitted.ToString());
            textWriter.WriteElementString("bIsVQReversed", ColSetMsg.bIsVQReversed.ToString());
            textWriter.WriteElementString("bIsVQWasted", ColSetMsg.bIsVQWasted.ToString());
            textWriter.WriteElementString("bIsVQFree", ColSetMsg.bIsVQFree.ToString());
            textWriter.WriteElementString("bIsStartTime", ColSetMsg.bIsStartTime.ToString());
            textWriter.WriteElementString("bIsExitTime", ColSetMsg.bIsExitTime.ToString());
            textWriter.WriteElementString("bIsFileName", ColSetMsg.bIsFileName.ToString());

            //Writing timer setting nodes.
            textWriter.WriteComment("Timer Settings");
            textWriter.WriteElementString("SysKey", ((Int32)TimerSetMsg.SysKey).ToString());
            textWriter.WriteElementString("NormalKey", ((Int32)TimerSetMsg.NormalKey).ToString());
            textWriter.WriteElementString("bIsLockBaseWhenStart", TimerSetMsg.bIsLockBaseWhenStart.ToString());
            textWriter.WriteElementString("bIsStopRefreshWhenStop", TimerSetMsg.bIsStopRefreshWhenStop.ToString());

            //Writing timer setting nodes.
            textWriter.WriteComment("Memory Unit Settings");
            textWriter.WriteElementString("eUnitType", eUnitType.ToString());
            //Writing end element.
            textWriter.WriteEndElement();

            // Writing end document.
            textWriter.WriteEndDocument();

            // Close text writer.
            textWriter.Close();

            #endregion
        }

        #endregion

        #region Custom Method

        private void RefreshCmd()
        {
            if (bIsProtected == false)
            {
                // Set Protect Tag.
                //bIsProtected = true;

                //Refresh Data
                RefreshTable();

                //Auto Add new probe if checked.
                if (bIsAutoAdd == true)
                {
                    foreach (Process p in Process.GetProcesses())
                    {
                        try
                        {
                            if ((p.StartTime.CompareTo(TimeAddBase) > 0) && (!MemProbe.ProbeIDs.Contains(p.Id)))
                            {
                                MemProbe op = new MemProbe(p);
                                oProbes.Add(op);
                                DataRow row = oTable.NewRow();
                                op.Update(ref row, ColSetMsg);
                                oTable.Rows.Add(row);     
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    TimeAddBase = DateTime.Now;
                }
                RefreshSysMem(false);
                //bIsProtected = false;
            }
        }

        /// <summary>
        /// Set Column view
        /// </summary>
        /// <param name="msg">Column Setting Msg</param>
        private void SetGridView(ColumnSettingMsg msg)
        {
            //CPU Usage Column
            GridView.Columns[ResSoli.ColumnCPU_Usage].Visible = msg.bIsCPUUsage;

            //VQ Column
            GridView.Columns[ResSoli.ColumnVQ].Visible = msg.bIsVQCurrent;

            //VQ Peak Column
            GridView.Columns[ResSoli.ColumnPeakVQ].Visible = msg.bIsVQPeak;

            //WS Column
            GridView.Columns[ResSoli.ColumnWS].Visible = msg.bIsWSCurrent;

            //WS Peak Column
            GridView.Columns[ResSoli.ColumnPeakWS].Visible = msg.bIsWSPeak;

            //PF Column
            GridView.Columns[ResSoli.ColumnPF].Visible = msg.bIsPFCurrent;

            //PF Peak Column
            GridView.Columns[ResSoli.ColumnPeakPF].Visible = msg.bIsPFPeak;

            //VQ Commit Column
            GridView.Columns[ResSoli.ColumnCommitVQ].Visible = msg.bIsVQCommitted;

            //VQ Reversed Column
            GridView.Columns[ResSoli.ColumnReverseVQ].Visible = msg.bIsVQReversed;

            //VQ Wasted Column
            GridView.Columns[ResSoli.ColumnWastedVQ].Visible = msg.bIsVQWasted;

            //VQ Free Column
            GridView.Columns[ResSoli.ColumnFreeVQ].Visible = msg.bIsVQFree;

            //Start Time Column
            GridView.Columns[ResSoli.ColumnStartTime].Visible = msg.bIsStartTime;

            //Exit Time Column
            GridView.Columns[ResSoli.ColumnExitTime].Visible = msg.bIsExitTime;

            //File Name Column
            GridView.Columns[ResSoli.ColumnFile].Visible = msg.bIsFileName;
        }

        /// <summary>
        /// Convent memory value to string based on current unit set.
        /// </summary>
        /// <param name="lValue">Memory value</param>
        /// <returns></returns>
        private string ConvertToStrData(long lValue)
        {
            switch (eUnitType)
            {
                case MemUnitType.B:
                    return (Convert.ToString(lValue) + "byte");
                case MemUnitType.KB:
                    return (Convert.ToString(Math.Round((decimal)lValue / 1024, 1)) + "Kb");
                case MemUnitType.MB:
                    return (Convert.ToString(Math.Round((decimal)lValue / (long)Math.Pow(1024, 2), 1)) + "Mb");
                case MemUnitType.GB:
                    return (Convert.ToString(Math.Round((decimal)lValue / (long)Math.Pow(1024, 3), 2)) + "Gb");
                case MemUnitType.Smart:
                    {
                        if (lValue < 1024 * 2)
                            return (Convert.ToString(lValue) + "b");
                        else if ((lValue >= 1024 * 2) && (lValue < 2097152)) //1024*2 = 2KB, 1024*1024*2 = 2MB
                            return (Convert.ToString(Math.Round((decimal)lValue / 1024, 1)) + "Kb");
                        else if ((lValue >= 2097152) && (lValue < 1342177280))   //1024*1024*2 = 2MB,  1024*1024*1.25 = 1.25GB
                            return (Convert.ToString(Math.Round((decimal)lValue / (long)Math.Pow(1024, 2), 1)) + "Mb");
                        else if (lValue > 1342177280)             //1024*1024*1.25 = 1.25GB
                            return (Convert.ToString(Math.Round((decimal)lValue / (long)Math.Pow(1024, 3), 2)) + "Gb");
                        return "";
                    }
                default:
                    return "";
            }
        }

        /// <summary>
        /// Refresh System Memory Infomation
        /// </summary>
        private void RefreshSysMem(bool t)
        {
            bool tag = (mem_info.ullTotalPhys == 0);
            APIUtls.GlobalMemoryStatusEx(mem_info);
            StatusWS.Text = "Avail WS: " + ConvertToStrData((long)mem_info.ullAvailPhys) + "/" + ConvertToStrData((long)mem_info.ullTotalPhys) + "    ";
            StatusAvailPF.Text = "Avail PF: " + ConvertToStrData((long)mem_info.ullAvailPageFile) + "/" + ConvertToStrData((long)mem_info.ullTotalPageFile);
            if(t)
            {
                StatusVQ.Text = "Total VQ: " + ConvertToStrData((long)MemProbe.sys_info.lpMaximumApplicationAddress) + "    ";
            }
        }

        /// <summary>
        /// Refresh table
        /// </summary>
        private void RefreshTable()
        {
            for (int i = 0; i < oProbes.Count; i++)
            {
                DataRow row = oTable.Rows[i];
                if (oProbes[i].ProbeStatus == ProbeStatus.Newborn)
                {
                    oProbes[i].CatchNew(ref row, ColSetMsg);
                }
                else if (oProbes[i].ProbeStatus == ProbeStatus.Catching)
                {
                    oProbes[i].Update(ref row, ColSetMsg);
                }
                else if ((oProbes[i].ProbeStatus == ProbeStatus.Dead) && (bIsAutoRenew))
                {
                    oProbes[i].Reborn(ref row, ColSetMsg);
                }
            }
        }

        #endregion

        #endregion
    }
}

