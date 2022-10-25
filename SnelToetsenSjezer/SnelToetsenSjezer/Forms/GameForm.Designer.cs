namespace SnelToetsenSjezer.WinForms.Forms
{
    partial class GameForm
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
            this.lbl_category_key = new System.Windows.Forms.Label();
            this.lbl_category_val = new System.Windows.Forms.Label();
            this.lbl_currhotkey = new System.Windows.Forms.Label();
            this.pnl_header_devider = new System.Windows.Forms.Panel();
            this.lbl_description_key = new System.Windows.Forms.Label();
            this.lbl_description_val = new System.Windows.Forms.Label();
            this.lbl_timer = new System.Windows.Forms.Label();
            this.lbl_userinputsteps_val = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_category_key
            // 
            this.lbl_category_key.Location = new System.Drawing.Point(12, 49);
            this.lbl_category_key.Name = "lbl_category_key";
            this.lbl_category_key.Size = new System.Drawing.Size(125, 20);
            this.lbl_category_key.TabIndex = 0;
            this.lbl_category_key.Text = "Category:";
            this.lbl_category_key.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_category_val
            // 
            this.lbl_category_val.Location = new System.Drawing.Point(143, 49);
            this.lbl_category_val.Name = "lbl_category_val";
            this.lbl_category_val.Size = new System.Drawing.Size(475, 20);
            this.lbl_category_val.TabIndex = 0;
            this.lbl_category_val.Text = "My amazing hotkey category";
            this.lbl_category_val.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_currhotkey
            // 
            this.lbl_currhotkey.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbl_currhotkey.Location = new System.Drawing.Point(12, 9);
            this.lbl_currhotkey.Name = "lbl_currhotkey";
            this.lbl_currhotkey.Size = new System.Drawing.Size(300, 30);
            this.lbl_currhotkey.TabIndex = 2;
            this.lbl_currhotkey.Text = "Hotkey 0 of 0";
            this.lbl_currhotkey.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnl_header_devider
            // 
            this.pnl_header_devider.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnl_header_devider.Location = new System.Drawing.Point(12, 42);
            this.pnl_header_devider.Name = "pnl_header_devider";
            this.pnl_header_devider.Size = new System.Drawing.Size(606, 4);
            this.pnl_header_devider.TabIndex = 3;
            // 
            // lbl_description_key
            // 
            this.lbl_description_key.Location = new System.Drawing.Point(12, 69);
            this.lbl_description_key.Name = "lbl_description_key";
            this.lbl_description_key.Size = new System.Drawing.Size(125, 20);
            this.lbl_description_key.TabIndex = 0;
            this.lbl_description_key.Text = "Description:";
            this.lbl_description_key.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_description_val
            // 
            this.lbl_description_val.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbl_description_val.Location = new System.Drawing.Point(12, 89);
            this.lbl_description_val.Name = "lbl_description_val";
            this.lbl_description_val.Size = new System.Drawing.Size(606, 110);
            this.lbl_description_val.TabIndex = 0;
            this.lbl_description_val.Text = "Press some buttons that do something do\'h";
            this.lbl_description_val.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_timer
            // 
            this.lbl_timer.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbl_timer.Location = new System.Drawing.Point(318, 9);
            this.lbl_timer.Name = "lbl_timer";
            this.lbl_timer.Size = new System.Drawing.Size(300, 30);
            this.lbl_timer.TabIndex = 2;
            this.lbl_timer.Text = "00:00";
            this.lbl_timer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_userinputsteps_val
            // 
            this.lbl_userinputsteps_val.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lbl_userinputsteps_val.Location = new System.Drawing.Point(12, 207);
            this.lbl_userinputsteps_val.Name = "lbl_userinputsteps_val";
            this.lbl_userinputsteps_val.Size = new System.Drawing.Size(606, 25);
            this.lbl_userinputsteps_val.TabIndex = 4;
            this.lbl_userinputsteps_val.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 241);
            this.Controls.Add(this.lbl_userinputsteps_val);
            this.Controls.Add(this.pnl_header_devider);
            this.Controls.Add(this.lbl_timer);
            this.Controls.Add(this.lbl_currhotkey);
            this.Controls.Add(this.lbl_description_val);
            this.Controls.Add(this.lbl_category_val);
            this.Controls.Add(this.lbl_description_key);
            this.Controls.Add(this.lbl_category_key);
            this.Name = "GameForm";
            this.Text = "SnelToetsenSjezer - Game";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private Label lbl_category_key;
        private Label lbl_category_val;
        private Label lbl_currhotkey;
        private Panel pnl_header_devider;
        private Label lbl_description_key;
        private Label lbl_description_val;
        private Label lbl_timer;
        private Label lbl_userinputsteps_val;
    }
}