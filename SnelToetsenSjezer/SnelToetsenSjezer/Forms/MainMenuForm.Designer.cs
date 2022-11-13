namespace SnelToetsenSjezer
{
    partial class MainMenuForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_startgame = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_categories = new System.Windows.Forms.CheckedListBox();
            this.lbl_howmanyquestions = new System.Windows.Forms.Label();
            this.num_howmanyquestions = new System.Windows.Forms.NumericUpDown();
            this.cmbbox_continue = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_howmanyquestions_5 = new System.Windows.Forms.Button();
            this.btn_howmanyquestions_max = new System.Windows.Forms.Button();
            this.btn_howmanyquestions_10 = new System.Windows.Forms.Button();
            this.btn_howmanyquestions_25 = new System.Windows.Forms.Button();
            this.pnl_header_devider = new System.Windows.Forms.Panel();
            this.lbl_gametitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.num_howmanyquestions)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_startgame
            // 
            this.btn_startgame.Font = new System.Drawing.Font("Segoe UI", 16.30189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_startgame.Location = new System.Drawing.Point(12, 403);
            this.btn_startgame.Name = "btn_startgame";
            this.btn_startgame.Size = new System.Drawing.Size(512, 70);
            this.btn_startgame.TabIndex = 0;
            this.btn_startgame.Text = "Start!";
            this.btn_startgame.UseVisualStyleBackColor = true;
            this.btn_startgame.Click += new System.EventHandler(this.StartGame_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Pick one or multiple categories:";
            // 
            // lb_categories
            // 
            this.lb_categories.CheckOnClick = true;
            this.lb_categories.FormattingEnabled = true;
            this.lb_categories.Location = new System.Drawing.Point(12, 88);
            this.lb_categories.Name = "lb_categories";
            this.lb_categories.Size = new System.Drawing.Size(512, 224);
            this.lb_categories.TabIndex = 2;
            this.lb_categories.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CategoriesItemCheckedChanged);
            this.lb_categories.SelectedIndexChanged += new System.EventHandler(this.CategoriesSelectedIndexChanged);
            // 
            // lbl_howmanyquestions
            // 
            this.lbl_howmanyquestions.Location = new System.Drawing.Point(12, 320);
            this.lbl_howmanyquestions.Name = "lbl_howmanyquestions";
            this.lbl_howmanyquestions.Size = new System.Drawing.Size(192, 26);
            this.lbl_howmanyquestions.TabIndex = 3;
            this.lbl_howmanyquestions.Text = "How many questions? (1-1)";
            this.lbl_howmanyquestions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // num_howmanyquestions
            // 
            this.num_howmanyquestions.Location = new System.Drawing.Point(230, 323);
            this.num_howmanyquestions.Name = "num_howmanyquestions";
            this.num_howmanyquestions.Size = new System.Drawing.Size(120, 25);
            this.num_howmanyquestions.TabIndex = 5;
            this.num_howmanyquestions.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_howmanyquestions.ValueChanged += new System.EventHandler(this.HowManyQuestions_ValueChanged);
            // 
            // cmbbox_continue
            // 
            this.cmbbox_continue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbox_continue.FormattingEnabled = true;
            this.cmbbox_continue.Items.AddRange(new object[] {
            "Automatically",
            "When i press Return",
            "Automatically -OR- when i press Return"});
            this.cmbbox_continue.Location = new System.Drawing.Point(230, 354);
            this.cmbbox_continue.Name = "cmbbox_continue";
            this.cmbbox_continue.Size = new System.Drawing.Size(294, 25);
            this.cmbbox_continue.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 357);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Continue to next / results:";
            // 
            // btn_howmanyquestions_5
            // 
            this.btn_howmanyquestions_5.Location = new System.Drawing.Point(356, 323);
            this.btn_howmanyquestions_5.Name = "btn_howmanyquestions_5";
            this.btn_howmanyquestions_5.Size = new System.Drawing.Size(35, 25);
            this.btn_howmanyquestions_5.TabIndex = 9;
            this.btn_howmanyquestions_5.Text = "5";
            this.btn_howmanyquestions_5.UseVisualStyleBackColor = true;
            this.btn_howmanyquestions_5.Click += new System.EventHandler(this.btn_howmanyquestions_Click);
            // 
            // btn_howmanyquestions_max
            // 
            this.btn_howmanyquestions_max.Location = new System.Drawing.Point(479, 323);
            this.btn_howmanyquestions_max.Name = "btn_howmanyquestions_max";
            this.btn_howmanyquestions_max.Size = new System.Drawing.Size(45, 25);
            this.btn_howmanyquestions_max.TabIndex = 9;
            this.btn_howmanyquestions_max.Text = "MAX";
            this.btn_howmanyquestions_max.UseVisualStyleBackColor = true;
            this.btn_howmanyquestions_max.Click += new System.EventHandler(this.btn_howmanyquestions_Click);
            // 
            // btn_howmanyquestions_10
            // 
            this.btn_howmanyquestions_10.Location = new System.Drawing.Point(397, 323);
            this.btn_howmanyquestions_10.Name = "btn_howmanyquestions_10";
            this.btn_howmanyquestions_10.Size = new System.Drawing.Size(35, 25);
            this.btn_howmanyquestions_10.TabIndex = 9;
            this.btn_howmanyquestions_10.Text = "10";
            this.btn_howmanyquestions_10.UseVisualStyleBackColor = true;
            this.btn_howmanyquestions_10.Click += new System.EventHandler(this.btn_howmanyquestions_Click);
            // 
            // btn_howmanyquestions_25
            // 
            this.btn_howmanyquestions_25.Location = new System.Drawing.Point(438, 323);
            this.btn_howmanyquestions_25.Name = "btn_howmanyquestions_25";
            this.btn_howmanyquestions_25.Size = new System.Drawing.Size(35, 25);
            this.btn_howmanyquestions_25.TabIndex = 9;
            this.btn_howmanyquestions_25.Text = "25";
            this.btn_howmanyquestions_25.UseVisualStyleBackColor = true;
            this.btn_howmanyquestions_25.Click += new System.EventHandler(this.btn_howmanyquestions_Click);
            // 
            // pnl_header_devider
            // 
            this.pnl_header_devider.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnl_header_devider.Location = new System.Drawing.Point(12, 48);
            this.pnl_header_devider.Name = "pnl_header_devider";
            this.pnl_header_devider.Size = new System.Drawing.Size(512, 4);
            this.pnl_header_devider.TabIndex = 10;
            // 
            // lbl_gametitle
            // 
            this.lbl_gametitle.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbl_gametitle.Location = new System.Drawing.Point(12, 10);
            this.lbl_gametitle.Name = "lbl_gametitle";
            this.lbl_gametitle.Size = new System.Drawing.Size(512, 34);
            this.lbl_gametitle.TabIndex = 11;
            this.lbl_gametitle.Text = "SnelToetsenSjezer";
            this.lbl_gametitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(12, 390);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(512, 4);
            this.panel1.TabIndex = 11;
            // 
            // MainMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 485);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lbl_gametitle);
            this.Controls.Add(this.pnl_header_devider);
            this.Controls.Add(this.btn_howmanyquestions_max);
            this.Controls.Add(this.btn_howmanyquestions_25);
            this.Controls.Add(this.btn_howmanyquestions_10);
            this.Controls.Add(this.btn_howmanyquestions_5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbbox_continue);
            this.Controls.Add(this.num_howmanyquestions);
            this.Controls.Add(this.lbl_howmanyquestions);
            this.Controls.Add(this.lb_categories);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_startgame);
            this.Name = "MainMenuForm";
            this.Text = "SnelToetsenSjezer - Main Menu";
            ((System.ComponentModel.ISupportInitialize)(this.num_howmanyquestions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btn_startgame;
        private Label label1;
        private CheckedListBox lb_categories;
        private Label lbl_howmanyquestions;
        private NumericUpDown num_howmanyquestions;
        private ComboBox cmbbox_continue;
        private Label label2;
        private Button btn_howmanyquestions_5;
        private Button btn_howmanyquestions_max;
        private Button btn_howmanyquestions_10;
        private Button btn_howmanyquestions_25;
        private Panel pnl_header_devider;
        private Label lbl_gametitle;
        private Panel panel1;
    }
}