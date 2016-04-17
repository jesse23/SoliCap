namespace SoliCap.Forms
{
    partial class FormProcess
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
            this.RadioName = new System.Windows.Forms.RadioButton();
            this.RadioID = new System.Windows.Forms.RadioButton();
            this.RadioSelect = new System.Windows.Forms.RadioButton();
            this.TextPID = new System.Windows.Forms.TextBox();
            this.TextName = new System.Windows.Forms.TextBox();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.ListSelect = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // RadioName
            // 
            this.RadioName.AutoSize = true;
            this.RadioName.Location = new System.Drawing.Point(12, 42);
            this.RadioName.Name = "RadioName";
            this.RadioName.Size = new System.Drawing.Size(68, 17);
            this.RadioName.TabIndex = 0;
            this.RadioName.Text = "By Name";
            this.RadioName.UseVisualStyleBackColor = true;
            this.RadioName.CheckedChanged += new System.EventHandler(this.RadioName_CheckedChanged);
            // 
            // RadioID
            // 
            this.RadioID.AutoSize = true;
            this.RadioID.Location = new System.Drawing.Point(12, 13);
            this.RadioID.Name = "RadioID";
            this.RadioID.Size = new System.Drawing.Size(58, 17);
            this.RadioID.TabIndex = 1;
            this.RadioID.Text = "By PID";
            this.RadioID.UseVisualStyleBackColor = true;
            this.RadioID.CheckedChanged += new System.EventHandler(this.RadioID_CheckedChanged);
            // 
            // RadioSelect
            // 
            this.RadioSelect.AutoSize = true;
            this.RadioSelect.Checked = true;
            this.RadioSelect.Location = new System.Drawing.Point(12, 70);
            this.RadioSelect.Name = "RadioSelect";
            this.RadioSelect.Size = new System.Drawing.Size(99, 17);
            this.RadioSelect.TabIndex = 2;
            this.RadioSelect.TabStop = true;
            this.RadioSelect.Text = "Select in the list";
            this.RadioSelect.UseVisualStyleBackColor = true;
            this.RadioSelect.CheckedChanged += new System.EventHandler(this.RadioSelect_CheckedChanged);
            // 
            // TextPID
            // 
            this.TextPID.Enabled = false;
            this.TextPID.Location = new System.Drawing.Point(83, 12);
            this.TextPID.Name = "TextPID";
            this.TextPID.Size = new System.Drawing.Size(106, 20);
            this.TextPID.TabIndex = 4;
            this.TextPID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextPID_KeyDown);
            this.TextPID.Leave += new System.EventHandler(this.TextPID_Leave);
            // 
            // TextName
            // 
            this.TextName.Enabled = false;
            this.TextName.Location = new System.Drawing.Point(83, 41);
            this.TextName.Name = "TextName";
            this.TextName.Size = new System.Drawing.Size(106, 20);
            this.TextName.TabIndex = 5;
            this.TextName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextName_KeyDown);
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(114, 196);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(75, 25);
            this.BtnAdd.TabIndex = 7;
            this.BtnAdd.Text = "Add";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // ListSelect
            // 
            this.ListSelect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ListSelect.FormattingEnabled = true;
            this.ListSelect.Location = new System.Drawing.Point(12, 94);
            this.ListSelect.Name = "ListSelect";
            this.ListSelect.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ListSelect.Size = new System.Drawing.Size(177, 93);
            this.ListSelect.Sorted = true;
            this.ListSelect.TabIndex = 9;
            // 
            // FormProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(201, 229);
            this.Controls.Add(this.ListSelect);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.TextName);
            this.Controls.Add(this.TextPID);
            this.Controls.Add(this.RadioSelect);
            this.Controls.Add(this.RadioID);
            this.Controls.Add(this.RadioName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormProcess";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Set Process";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton RadioName;
        private System.Windows.Forms.RadioButton RadioID;
        private System.Windows.Forms.RadioButton RadioSelect;
        private System.Windows.Forms.TextBox TextPID;
        private System.Windows.Forms.TextBox TextName;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.ListBox ListSelect;
    }
}