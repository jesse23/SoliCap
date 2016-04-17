namespace SoliCap
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.CtxMenuGridView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CtxInvSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.CtxMenuRenew = new System.Windows.Forms.ToolStripMenuItem();
            this.CtxMenuKill = new System.Windows.Forms.ToolStripMenuItem();
            this.CtxMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusVQ = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusWS = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusAvailPF = new System.Windows.Forms.ToolStripStatusLabel();
            this.GridView = new System.Windows.Forms.DataGridView();
            this.processToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuAddProcess = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuClearAllProcess = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuRenewAll = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuLockBase = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuTimer = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOption = new System.Windows.Forms.ToolStripMenuItem();
            this.columnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuTimerOption = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuUnit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuUnitAuto = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuUnitB = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuUnitKB = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuUnitMB = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuUnitGB = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSpeed = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSpeedManual = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuSpeedHigh = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSpeedNormal = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSpeedLow = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSpeedVeryLow = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshNowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuAutoAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuAutoRenew = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuTopMost = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.oMainMenu = new System.Windows.Forms.MenuStrip();
            this.CtxMenuGridView.SuspendLayout();
            this.StatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            this.oMainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // CtxMenuGridView
            // 
            this.CtxMenuGridView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CtxInvSelect,
            this.CtxMenuRenew,
            this.CtxMenuKill,
            this.CtxMenuDelete});
            this.CtxMenuGridView.Name = "CtxMenuGridView";
            this.CtxMenuGridView.Size = new System.Drawing.Size(169, 92);
            // 
            // CtxInvSelect
            // 
            this.CtxInvSelect.Name = "CtxInvSelect";
            this.CtxInvSelect.Size = new System.Drawing.Size(168, 22);
            this.CtxInvSelect.Text = "Invert Select";
            this.CtxInvSelect.Click += new System.EventHandler(this.CtxInvSelect_Click);
            // 
            // CtxMenuRenew
            // 
            this.CtxMenuRenew.Name = "CtxMenuRenew";
            this.CtxMenuRenew.Size = new System.Drawing.Size(168, 22);
            this.CtxMenuRenew.Text = "Renew Exit Item";
            this.CtxMenuRenew.Click += new System.EventHandler(this.CtxMenuRenew_Click);
            // 
            // CtxMenuKill
            // 
            this.CtxMenuKill.Name = "CtxMenuKill";
            this.CtxMenuKill.Size = new System.Drawing.Size(168, 22);
            this.CtxMenuKill.Text = "Kill Process";
            this.CtxMenuKill.Click += new System.EventHandler(this.CtxMenuKill_Click);
            // 
            // CtxMenuDelete
            // 
            this.CtxMenuDelete.Name = "CtxMenuDelete";
            this.CtxMenuDelete.Size = new System.Drawing.Size(168, 22);
            this.CtxMenuDelete.Text = "Delete";
            this.CtxMenuDelete.Click += new System.EventHandler(this.CtxMenuDelete_Click);
            // 
            // StatusStrip
            // 
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusVQ,
            this.StatusWS,
            this.StatusAvailPF});
            this.StatusStrip.Location = new System.Drawing.Point(0, 240);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(412, 22);
            this.StatusStrip.TabIndex = 3;
            this.StatusStrip.Text = "statusStrip1";
            // 
            // StatusVQ
            // 
            this.StatusVQ.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.StatusVQ.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.StatusVQ.Name = "StatusVQ";
            this.StatusVQ.Size = new System.Drawing.Size(10, 17);
            this.StatusVQ.Text = "a";
            this.StatusVQ.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatusWS
            // 
            this.StatusWS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.StatusWS.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.StatusWS.Name = "StatusWS";
            this.StatusWS.Size = new System.Drawing.Size(10, 17);
            this.StatusWS.Text = "a";
            this.StatusWS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatusAvailPF
            // 
            this.StatusAvailPF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.StatusAvailPF.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.StatusAvailPF.Name = "StatusAvailPF";
            this.StatusAvailPF.Size = new System.Drawing.Size(10, 17);
            this.StatusAvailPF.Text = "a";
            this.StatusAvailPF.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GridView
            // 
            this.GridView.AllowUserToAddRows = false;
            this.GridView.AllowUserToOrderColumns = true;
            this.GridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.GridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.GridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.GridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.GridView.GridColor = System.Drawing.SystemColors.ActiveBorder;
            this.GridView.Location = new System.Drawing.Point(0, 22);
            this.GridView.Margin = new System.Windows.Forms.Padding(0);
            this.GridView.Name = "GridView";
            this.GridView.ReadOnly = true;
            this.GridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.GridView.RowHeadersVisible = false;
            this.GridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridView.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.GridView.RowTemplate.ContextMenuStrip = this.CtxMenuGridView;
            this.GridView.RowTemplate.Height = 20;
            this.GridView.RowTemplate.ReadOnly = true;
            this.GridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.GridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridView.Size = new System.Drawing.Size(412, 218);
            this.GridView.TabIndex = 4;
            this.GridView.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.GridView_UserDeletingRow_1);
            // 
            // processToolStripMenuItem
            // 
            this.processToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuAddProcess,
            this.MenuClearAllProcess,
            this.MenuRenewAll,
            this.MenuLockBase,
            this.MenuTimer});
            this.processToolStripMenuItem.Name = "processToolStripMenuItem";
            this.processToolStripMenuItem.Size = new System.Drawing.Size(65, 21);
            this.processToolStripMenuItem.Text = "Process";
            // 
            // MenuAddProcess
            // 
            this.MenuAddProcess.Name = "MenuAddProcess";
            this.MenuAddProcess.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.MenuAddProcess.Size = new System.Drawing.Size(213, 22);
            this.MenuAddProcess.Text = "Add Process...";
            this.MenuAddProcess.Click += new System.EventHandler(this.addProcessToolStripMenuItem_Click);
            // 
            // MenuClearAllProcess
            // 
            this.MenuClearAllProcess.Name = "MenuClearAllProcess";
            this.MenuClearAllProcess.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.MenuClearAllProcess.Size = new System.Drawing.Size(213, 22);
            this.MenuClearAllProcess.Text = "Clear All Process";
            this.MenuClearAllProcess.Click += new System.EventHandler(this.MenuClearAllProcess_Click);
            // 
            // MenuRenewAll
            // 
            this.MenuRenewAll.Name = "MenuRenewAll";
            this.MenuRenewAll.Size = new System.Drawing.Size(213, 22);
            this.MenuRenewAll.Text = "Renew All Exit Process";
            this.MenuRenewAll.Click += new System.EventHandler(this.renewAllExitProcessToolStripMenuItem_Click);
            // 
            // MenuLockBase
            // 
            this.MenuLockBase.Name = "MenuLockBase";
            this.MenuLockBase.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.MenuLockBase.Size = new System.Drawing.Size(213, 22);
            this.MenuLockBase.Text = "Lock Memory Base";
            this.MenuLockBase.Click += new System.EventHandler(this.MenuLockBase_Click);
            // 
            // MenuTimer
            // 
            this.MenuTimer.Name = "MenuTimer";
            this.MenuTimer.Size = new System.Drawing.Size(213, 22);
            this.MenuTimer.Text = "Start Timer";
            this.MenuTimer.Click += new System.EventHandler(this.MenuTimer_Click);
            // 
            // MenuOption
            // 
            this.MenuOption.Checked = true;
            this.MenuOption.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MenuOption.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.columnsToolStripMenuItem,
            this.MenuTimerOption,
            this.MenuUnit,
            this.MenuSpeed,
            this.refreshNowToolStripMenuItem,
            this.MenuAutoAdd,
            this.MenuAutoRenew,
            this.MenuTopMost});
            this.MenuOption.Name = "MenuOption";
            this.MenuOption.Size = new System.Drawing.Size(60, 21);
            this.MenuOption.Text = "Option";
            // 
            // columnsToolStripMenuItem
            // 
            this.columnsToolStripMenuItem.Name = "columnsToolStripMenuItem";
            this.columnsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
            this.columnsToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.columnsToolStripMenuItem.Text = "Set Columns...";
            this.columnsToolStripMenuItem.Click += new System.EventHandler(this.columnsToolStripMenuItem_Click);
            // 
            // MenuTimerOption
            // 
            this.MenuTimerOption.Name = "MenuTimerOption";
            this.MenuTimerOption.Size = new System.Drawing.Size(210, 22);
            this.MenuTimerOption.Text = "Timer Options...";
            this.MenuTimerOption.Click += new System.EventHandler(this.MenuTimerOption_Click);
            // 
            // MenuUnit
            // 
            this.MenuUnit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuUnitAuto,
            this.toolStripSeparator1,
            this.MenuUnitB,
            this.MenuUnitKB,
            this.MenuUnitMB,
            this.MenuUnitGB});
            this.MenuUnit.Name = "MenuUnit";
            this.MenuUnit.Size = new System.Drawing.Size(210, 22);
            this.MenuUnit.Text = "Memory Unit";
            // 
            // MenuUnitAuto
            // 
            this.MenuUnitAuto.Name = "MenuUnitAuto";
            this.MenuUnitAuto.Size = new System.Drawing.Size(110, 22);
            this.MenuUnitAuto.Text = "Smart";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(107, 6);
            // 
            // MenuUnitB
            // 
            this.MenuUnitB.Name = "MenuUnitB";
            this.MenuUnitB.Size = new System.Drawing.Size(110, 22);
            this.MenuUnitB.Text = "Byte";
            // 
            // MenuUnitKB
            // 
            this.MenuUnitKB.Name = "MenuUnitKB";
            this.MenuUnitKB.Size = new System.Drawing.Size(110, 22);
            this.MenuUnitKB.Text = "KB";
            // 
            // MenuUnitMB
            // 
            this.MenuUnitMB.Checked = true;
            this.MenuUnitMB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MenuUnitMB.Name = "MenuUnitMB";
            this.MenuUnitMB.Size = new System.Drawing.Size(110, 22);
            this.MenuUnitMB.Text = "MB";
            // 
            // MenuUnitGB
            // 
            this.MenuUnitGB.Name = "MenuUnitGB";
            this.MenuUnitGB.Size = new System.Drawing.Size(110, 22);
            this.MenuUnitGB.Text = "GB";
            // 
            // MenuSpeed
            // 
            this.MenuSpeed.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuSpeedManual,
            this.toolStripSeparator2,
            this.MenuSpeedHigh,
            this.MenuSpeedNormal,
            this.MenuSpeedLow,
            this.MenuSpeedVeryLow});
            this.MenuSpeed.Name = "MenuSpeed";
            this.MenuSpeed.Size = new System.Drawing.Size(210, 22);
            this.MenuSpeed.Text = "Refresh Speed";
            // 
            // MenuSpeedManual
            // 
            this.MenuSpeedManual.CheckOnClick = true;
            this.MenuSpeedManual.Name = "MenuSpeedManual";
            this.MenuSpeedManual.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.MenuSpeedManual.Size = new System.Drawing.Size(166, 22);
            this.MenuSpeedManual.Text = "Manual";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(163, 6);
            // 
            // MenuSpeedHigh
            // 
            this.MenuSpeedHigh.CheckOnClick = true;
            this.MenuSpeedHigh.Name = "MenuSpeedHigh";
            this.MenuSpeedHigh.Size = new System.Drawing.Size(166, 22);
            this.MenuSpeedHigh.Text = "High (0.5s)";
            // 
            // MenuSpeedNormal
            // 
            this.MenuSpeedNormal.Checked = true;
            this.MenuSpeedNormal.CheckOnClick = true;
            this.MenuSpeedNormal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MenuSpeedNormal.Name = "MenuSpeedNormal";
            this.MenuSpeedNormal.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.MenuSpeedNormal.Size = new System.Drawing.Size(166, 22);
            this.MenuSpeedNormal.Text = "Normal (1s)";
            // 
            // MenuSpeedLow
            // 
            this.MenuSpeedLow.CheckOnClick = true;
            this.MenuSpeedLow.Name = "MenuSpeedLow";
            this.MenuSpeedLow.Size = new System.Drawing.Size(166, 22);
            this.MenuSpeedLow.Text = "Low (5s)";
            // 
            // MenuSpeedVeryLow
            // 
            this.MenuSpeedVeryLow.CheckOnClick = true;
            this.MenuSpeedVeryLow.Name = "MenuSpeedVeryLow";
            this.MenuSpeedVeryLow.Size = new System.Drawing.Size(166, 22);
            this.MenuSpeedVeryLow.Text = "Very Low (30s)";
            // 
            // refreshNowToolStripMenuItem
            // 
            this.refreshNowToolStripMenuItem.Name = "refreshNowToolStripMenuItem";
            this.refreshNowToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.refreshNowToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.refreshNowToolStripMenuItem.Text = "Refresh Now";
            this.refreshNowToolStripMenuItem.Click += new System.EventHandler(this.refreshNowToolStripMenuItem_Click);
            // 
            // MenuAutoAdd
            // 
            this.MenuAutoAdd.Name = "MenuAutoAdd";
            this.MenuAutoAdd.Size = new System.Drawing.Size(210, 22);
            this.MenuAutoAdd.Text = "Auto Add New Process";
            this.MenuAutoAdd.Click += new System.EventHandler(this.autoAddNewProcessToolStripMenuItem_Click);
            // 
            // MenuAutoRenew
            // 
            this.MenuAutoRenew.Name = "MenuAutoRenew";
            this.MenuAutoRenew.Size = new System.Drawing.Size(210, 22);
            this.MenuAutoRenew.Text = "Auto Renew When Exit";
            this.MenuAutoRenew.Click += new System.EventHandler(this.renewExitWhenRefreshToolStripMenuItem_Click);
            // 
            // MenuTopMost
            // 
            this.MenuTopMost.Name = "MenuTopMost";
            this.MenuTopMost.Size = new System.Drawing.Size(210, 22);
            this.MenuTopMost.Text = "Always on top";
            this.MenuTopMost.Click += new System.EventHandler(this.MenuTopMost_Click_1);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuHelp,
            this.MenuAbout});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(47, 21);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // MenuHelp
            // 
            this.MenuHelp.Name = "MenuHelp";
            this.MenuHelp.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.MenuHelp.Size = new System.Drawing.Size(133, 22);
            this.MenuHelp.Text = "Help...";
            this.MenuHelp.Click += new System.EventHandler(this.MenuHelp_Click);
            // 
            // MenuAbout
            // 
            this.MenuAbout.Name = "MenuAbout";
            this.MenuAbout.Size = new System.Drawing.Size(133, 22);
            this.MenuAbout.Text = "About...";
            this.MenuAbout.Click += new System.EventHandler(this.MenuAbout_Click);
            // 
            // oMainMenu
            // 
            this.oMainMenu.AllowDrop = true;
            this.oMainMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.oMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.processToolStripMenuItem,
            this.MenuOption,
            this.helpToolStripMenuItem});
            this.oMainMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.oMainMenu.Location = new System.Drawing.Point(0, 0);
            this.oMainMenu.Name = "oMainMenu";
            this.oMainMenu.Size = new System.Drawing.Size(412, 25);
            this.oMainMenu.TabIndex = 0;
            this.oMainMenu.Text = "menuStrip1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(412, 262);
            this.Controls.Add(this.GridView);
            this.Controls.Add(this.oMainMenu);
            this.Controls.Add(this.StatusStrip);
            this.MainMenuStrip = this.oMainMenu;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "SoliCap";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.CtxMenuGridView.ResumeLayout(false);
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
            this.oMainMenu.ResumeLayout(false);
            this.oMainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip CtxMenuGridView;
        private System.Windows.Forms.ToolStripMenuItem CtxMenuDelete;
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel StatusVQ;
        private System.Windows.Forms.ToolStripStatusLabel StatusWS;
        private System.Windows.Forms.ToolStripMenuItem CtxInvSelect;
        private System.Windows.Forms.ToolStripMenuItem CtxMenuRenew;
        private System.Windows.Forms.DataGridView GridView;
        private System.Windows.Forms.ToolStripMenuItem processToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuAddProcess;
        private System.Windows.Forms.ToolStripMenuItem MenuClearAllProcess;
        private System.Windows.Forms.ToolStripMenuItem MenuRenewAll;
        private System.Windows.Forms.ToolStripMenuItem MenuLockBase;
        private System.Windows.Forms.ToolStripMenuItem MenuOption;
        private System.Windows.Forms.ToolStripMenuItem columnsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuUnit;
        private System.Windows.Forms.ToolStripMenuItem MenuUnitAuto;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MenuUnitB;
        private System.Windows.Forms.ToolStripMenuItem MenuUnitKB;
        private System.Windows.Forms.ToolStripMenuItem MenuUnitMB;
        private System.Windows.Forms.ToolStripMenuItem MenuUnitGB;
        private System.Windows.Forms.ToolStripMenuItem MenuSpeed;
        private System.Windows.Forms.ToolStripMenuItem MenuSpeedManual;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem MenuSpeedHigh;
        private System.Windows.Forms.ToolStripMenuItem MenuSpeedNormal;
        private System.Windows.Forms.ToolStripMenuItem MenuSpeedLow;
        private System.Windows.Forms.ToolStripMenuItem MenuSpeedVeryLow;
        private System.Windows.Forms.ToolStripMenuItem refreshNowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuAutoAdd;
        private System.Windows.Forms.ToolStripMenuItem MenuAutoRenew;
        private System.Windows.Forms.ToolStripMenuItem MenuTopMost;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuHelp;
        private System.Windows.Forms.ToolStripMenuItem MenuAbout;
        private System.Windows.Forms.MenuStrip oMainMenu;
        private System.Windows.Forms.ToolStripStatusLabel StatusAvailPF;
        private System.Windows.Forms.ToolStripMenuItem MenuTimer;
        private System.Windows.Forms.ToolStripMenuItem MenuTimerOption;
        private System.Windows.Forms.ToolStripMenuItem CtxMenuKill;
    }
}

