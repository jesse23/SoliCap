namespace SoliCap.Forms
{
    partial class FormTimer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TextHotKey = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.ComboSysKey = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.CheckIsLockBase = new System.Windows.Forms.CheckBox();
            this.CheckIsStopRefresh = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TextHotKey
            // 
            this.TextHotKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextHotKey.Location = new System.Drawing.Point(201, 13);
            this.TextHotKey.MaxLength = 4;
            this.TextHotKey.Name = "TextHotKey";
            this.TextHotKey.ReadOnly = true;
            this.TextHotKey.Size = new System.Drawing.Size(56, 21);
            this.TextHotKey.TabIndex = 47;
            this.TextHotKey.Text = "Z";
            this.TextHotKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextHotKey_KeyDown);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(184, 15);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(11, 12);
            this.Label2.TabIndex = 46;
            this.Label2.Text = "+";
            // 
            // ComboSysKey
            // 
            this.ComboSysKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboSysKey.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ComboSysKey.FormattingEnabled = true;
            this.ComboSysKey.Items.AddRange(new object[] {
            "Alt",
            "Ctrl",
            "Shift",
            "N/A"});
            this.ComboSysKey.Location = new System.Drawing.Point(118, 12);
            this.ComboSysKey.Name = "ComboSysKey";
            this.ComboSysKey.Size = new System.Drawing.Size(60, 20);
            this.ComboSysKey.TabIndex = 45;
            this.ComboSysKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ComboSysKey_KeyDown);
            this.ComboSysKey.TextChanged += new System.EventHandler(this.ComboSysKey_TextChanged);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(12, 15);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(11, 12);
            this.Label1.TabIndex = 44;
            this.Label1.Text = "a";
            // 
            // CheckIsLockBase
            // 
            this.CheckIsLockBase.AutoSize = true;
            this.CheckIsLockBase.Location = new System.Drawing.Point(14, 44);
            this.CheckIsLockBase.Name = "CheckIsLockBase";
            this.CheckIsLockBase.Size = new System.Drawing.Size(78, 16);
            this.CheckIsLockBase.TabIndex = 48;
            this.CheckIsLockBase.Text = "checkBox1";
            this.CheckIsLockBase.UseVisualStyleBackColor = true;
            // 
            // CheckIsStopRefresh
            // 
            this.CheckIsStopRefresh.AutoSize = true;
            this.CheckIsStopRefresh.Location = new System.Drawing.Point(14, 66);
            this.CheckIsStopRefresh.Name = "CheckIsStopRefresh";
            this.CheckIsStopRefresh.Size = new System.Drawing.Size(78, 16);
            this.CheckIsStopRefresh.TabIndex = 49;
            this.CheckIsStopRefresh.Text = "checkBox2";
            this.CheckIsStopRefresh.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(181, 101);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 50;
            this.button1.Text = "Done";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormTimer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(271, 132);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CheckIsStopRefresh);
            this.Controls.Add(this.CheckIsLockBase);
            this.Controls.Add(this.TextHotKey);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.ComboSysKey);
            this.Controls.Add(this.Label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormTimer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Timer Options";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTimer_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox TextHotKey;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.ComboBox ComboSysKey;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.CheckBox CheckIsLockBase;
        private System.Windows.Forms.CheckBox CheckIsStopRefresh;
        private System.Windows.Forms.Button button1;
    }
}