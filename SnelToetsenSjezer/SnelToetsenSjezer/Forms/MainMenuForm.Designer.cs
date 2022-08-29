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
            this.SuspendLayout();
            // 
            // button1
            // 
            this.btn_startgame.Font = new System.Drawing.Font("Segoe UI", 16.30189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_startgame.Location = new System.Drawing.Point(12, 353);
            this.btn_startgame.Name = "button1";
            this.btn_startgame.Size = new System.Drawing.Size(776, 85);
            this.btn_startgame.TabIndex = 0;
            this.btn_startgame.Text = "Start!";
            this.btn_startgame.UseVisualStyleBackColor = true;
            this.btn_startgame.Click += new System.EventHandler(this.btn_startgame_Click);
            // 
            // MainMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_startgame);
            this.Name = "MainMenuForm";
            this.Text = "SnelToetsenSjezer - Main Menu";
            this.ResumeLayout(false);

        }

        #endregion

        private Button btn_startgame;
    }
}