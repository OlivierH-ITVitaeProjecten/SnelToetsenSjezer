namespace SnelToetsenSjezer.WinForms.Forms
{
    partial class GameOverForm
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
            this.NumHotKeysLabel = new System.Windows.Forms.Label();
            this.TimeSpentLabel = new System.Windows.Forms.Label();
            this.NumHotKeysValue = new System.Windows.Forms.Label();
            this.TimeSpentValue = new System.Windows.Forms.Label();
            this.HotKeyDetailsLabel = new System.Windows.Forms.Label();
            this.datagrid_hotkeydetails = new System.Windows.Forms.DataGridView();
            this.HotKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Attempts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Duration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.datagrid_hotkeydetails)).BeginInit();
            this.SuspendLayout();
            // 
            // NumHotKeysLabel
            // 
            this.NumHotKeysLabel.Location = new System.Drawing.Point(12, 9);
            this.NumHotKeysLabel.Name = "NumHotKeysLabel";
            this.NumHotKeysLabel.Size = new System.Drawing.Size(160, 25);
            this.NumHotKeysLabel.TabIndex = 0;
            this.NumHotKeysLabel.Text = "Number of Hotkeys:";
            this.NumHotKeysLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TimeSpentLabel
            // 
            this.TimeSpentLabel.Location = new System.Drawing.Point(12, 34);
            this.TimeSpentLabel.Name = "TimeSpentLabel";
            this.TimeSpentLabel.Size = new System.Drawing.Size(160, 25);
            this.TimeSpentLabel.TabIndex = 1;
            this.TimeSpentLabel.Text = "Total time spent:";
            this.TimeSpentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NumHotKeysValue
            // 
            this.NumHotKeysValue.Location = new System.Drawing.Point(178, 9);
            this.NumHotKeysValue.Name = "NumHotKeysValue";
            this.NumHotKeysValue.Size = new System.Drawing.Size(160, 25);
            this.NumHotKeysValue.TabIndex = 0;
            this.NumHotKeysValue.Text = "5";
            this.NumHotKeysValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TimeSpentValue
            // 
            this.TimeSpentValue.Location = new System.Drawing.Point(178, 34);
            this.TimeSpentValue.Name = "TimeSpentValue";
            this.TimeSpentValue.Size = new System.Drawing.Size(160, 25);
            this.TimeSpentValue.TabIndex = 0;
            this.TimeSpentValue.Text = "00:25";
            this.TimeSpentValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // HotKeyDetailsLabel
            // 
            this.HotKeyDetailsLabel.Location = new System.Drawing.Point(12, 59);
            this.HotKeyDetailsLabel.Name = "HotKeyDetailsLabel";
            this.HotKeyDetailsLabel.Size = new System.Drawing.Size(160, 25);
            this.HotKeyDetailsLabel.TabIndex = 1;
            this.HotKeyDetailsLabel.Text = "Hotkey details:";
            this.HotKeyDetailsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // datagrid_hotkeydetails
            // 
            this.datagrid_hotkeydetails.AllowUserToAddRows = false;
            this.datagrid_hotkeydetails.AllowUserToDeleteRows = false;
            this.datagrid_hotkeydetails.AllowUserToResizeColumns = false;
            this.datagrid_hotkeydetails.AllowUserToResizeRows = false;
            this.datagrid_hotkeydetails.BackgroundColor = System.Drawing.SystemColors.Control;
            this.datagrid_hotkeydetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagrid_hotkeydetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.HotKey,
            this.Attempts,
            this.Duration});
            this.datagrid_hotkeydetails.Location = new System.Drawing.Point(12, 87);
            this.datagrid_hotkeydetails.Name = "datagrid_hotkeydetails";
            this.datagrid_hotkeydetails.ReadOnly = true;
            this.datagrid_hotkeydetails.RowHeadersVisible = false;
            this.datagrid_hotkeydetails.RowHeadersWidth = 45;
            this.datagrid_hotkeydetails.RowTemplate.Height = 27;
            this.datagrid_hotkeydetails.RowTemplate.ReadOnly = true;
            this.datagrid_hotkeydetails.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.datagrid_hotkeydetails.Size = new System.Drawing.Size(660, 351);
            this.datagrid_hotkeydetails.TabIndex = 3;
            // 
            // HotKey
            // 
            this.HotKey.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.HotKey.HeaderText = "HotKey";
            this.HotKey.MinimumWidth = 370;
            this.HotKey.Name = "HotKey";
            this.HotKey.ReadOnly = true;
            this.HotKey.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Attempts
            // 
            this.Attempts.HeaderText = "Attempts";
            this.Attempts.MinimumWidth = 110;
            this.Attempts.Name = "Attempts";
            this.Attempts.ReadOnly = true;
            this.Attempts.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Attempts.Width = 128;
            // 
            // Duration
            // 
            this.Duration.HeaderText = "Duration";
            this.Duration.MinimumWidth = 110;
            this.Duration.Name = "Duration";
            this.Duration.ReadOnly = true;
            this.Duration.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Duration.Width = 128;
            // 
            // GameOverForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 450);
            this.Controls.Add(this.datagrid_hotkeydetails);
            this.Controls.Add(this.HotKeyDetailsLabel);
            this.Controls.Add(this.TimeSpentLabel);
            this.Controls.Add(this.TimeSpentValue);
            this.Controls.Add(this.NumHotKeysValue);
            this.Controls.Add(this.NumHotKeysLabel);
            this.Name = "GameOverForm";
            this.Text = "GameOver";
            ((System.ComponentModel.ISupportInitialize)(this.datagrid_hotkeydetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Label NumHotKeysLabel;
        private Label TimeSpentLabel;
        private Label NumHotKeysValue;
        private Label TimeSpentValue;
        private Label HotKeyDetailsLabel;
        private DataGridView datagrid_hotkeydetails;
        private DataGridViewTextBoxColumn HotKey;
        private DataGridViewTextBoxColumn Attempts;
        private DataGridViewTextBoxColumn Duration;
    }
}