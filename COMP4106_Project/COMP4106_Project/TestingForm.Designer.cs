namespace COMP4106_Project
{
    partial class TestingForm
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
            this.txtGame = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // txtGame
            // 
            this.txtGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGame.Location = new System.Drawing.Point(0, 0);
            this.txtGame.Name = "txtGame";
            this.txtGame.Size = new System.Drawing.Size(790, 498);
            this.txtGame.TabIndex = 0;
            this.txtGame.Text = "";
            // 
            // TestingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 498);
            this.Controls.Add(this.txtGame);
            this.Name = "TestingForm";
            this.Text = "Bazinga";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtGame;
    }
}

