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
            this.pnControl = new System.Windows.Forms.Panel();
            this.radUp = new System.Windows.Forms.RadioButton();
            this.btnExecute = new System.Windows.Forms.Button();
            this.lblInfo2 = new System.Windows.Forms.Label();
            this.btnBlock = new System.Windows.Forms.Button();
            this.btnMove = new System.Windows.Forms.Button();
            this.btnAttack = new System.Windows.Forms.Button();
            this.lstMoveQueue = new System.Windows.Forms.ListBox();
            this.lblInfo1 = new System.Windows.Forms.Label();
            this.pnlGame = new System.Windows.Forms.TableLayoutPanel();
            this.radLeft = new System.Windows.Forms.RadioButton();
            this.radRight = new System.Windows.Forms.RadioButton();
            this.radDown = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.pnControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnControl
            // 
            this.pnControl.Controls.Add(this.label1);
            this.pnControl.Controls.Add(this.radDown);
            this.pnControl.Controls.Add(this.radRight);
            this.pnControl.Controls.Add(this.radLeft);
            this.pnControl.Controls.Add(this.radUp);
            this.pnControl.Controls.Add(this.btnExecute);
            this.pnControl.Controls.Add(this.lblInfo2);
            this.pnControl.Controls.Add(this.btnBlock);
            this.pnControl.Controls.Add(this.btnMove);
            this.pnControl.Controls.Add(this.btnAttack);
            this.pnControl.Controls.Add(this.lstMoveQueue);
            this.pnControl.Controls.Add(this.lblInfo1);
            this.pnControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnControl.Location = new System.Drawing.Point(0, 487);
            this.pnControl.Name = "pnControl";
            this.pnControl.Size = new System.Drawing.Size(711, 97);
            this.pnControl.TabIndex = 0;
            // 
            // radUp
            // 
            this.radUp.AutoSize = true;
            this.radUp.Checked = true;
            this.radUp.Location = new System.Drawing.Point(359, 23);
            this.radUp.Name = "radUp";
            this.radUp.Size = new System.Drawing.Size(14, 13);
            this.radUp.TabIndex = 7;
            this.radUp.TabStop = true;
            this.radUp.UseVisualStyleBackColor = true;
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(71, 64);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(187, 23);
            this.btnExecute.TabIndex = 6;
            this.btnExecute.Text = "Execute Move Queue";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // lblInfo2
            // 
            this.lblInfo2.AutoSize = true;
            this.lblInfo2.Location = new System.Drawing.Point(4, 35);
            this.lblInfo2.Name = "lblInfo2";
            this.lblInfo2.Size = new System.Drawing.Size(35, 13);
            this.lblInfo2.TabIndex = 5;
            this.lblInfo2.Text = "label1";
            // 
            // btnBlock
            // 
            this.btnBlock.Location = new System.Drawing.Point(264, 64);
            this.btnBlock.Name = "btnBlock";
            this.btnBlock.Size = new System.Drawing.Size(75, 23);
            this.btnBlock.TabIndex = 4;
            this.btnBlock.Text = "Block";
            this.btnBlock.UseVisualStyleBackColor = true;
            this.btnBlock.Click += new System.EventHandler(this.btnBlock_Click);
            // 
            // btnMove
            // 
            this.btnMove.Location = new System.Drawing.Point(264, 35);
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(75, 23);
            this.btnMove.TabIndex = 3;
            this.btnMove.Text = "Move";
            this.btnMove.UseVisualStyleBackColor = true;
            this.btnMove.Click += new System.EventHandler(this.btnMove_Click);
            // 
            // btnAttack
            // 
            this.btnAttack.Location = new System.Drawing.Point(264, 6);
            this.btnAttack.Name = "btnAttack";
            this.btnAttack.Size = new System.Drawing.Size(75, 23);
            this.btnAttack.TabIndex = 2;
            this.btnAttack.Text = "Attack";
            this.btnAttack.UseVisualStyleBackColor = true;
            this.btnAttack.Click += new System.EventHandler(this.btnAttack_Click);
            // 
            // lstMoveQueue
            // 
            this.lstMoveQueue.Dock = System.Windows.Forms.DockStyle.Right;
            this.lstMoveQueue.FormattingEnabled = true;
            this.lstMoveQueue.Location = new System.Drawing.Point(391, 0);
            this.lstMoveQueue.Name = "lstMoveQueue";
            this.lstMoveQueue.Size = new System.Drawing.Size(320, 97);
            this.lstMoveQueue.TabIndex = 1;
            // 
            // lblInfo1
            // 
            this.lblInfo1.AutoSize = true;
            this.lblInfo1.Location = new System.Drawing.Point(4, 7);
            this.lblInfo1.Name = "lblInfo1";
            this.lblInfo1.Size = new System.Drawing.Size(35, 13);
            this.lblInfo1.TabIndex = 0;
            this.lblInfo1.Text = "label1";
            // 
            // pnlGame
            // 
            this.pnlGame.AutoSize = true;
            this.pnlGame.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pnlGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGame.Location = new System.Drawing.Point(0, 0);
            this.pnlGame.Name = "pnlGame";
            this.pnlGame.Size = new System.Drawing.Size(711, 487);
            this.pnlGame.TabIndex = 1;
            // 
            // radLeft
            // 
            this.radLeft.AutoSize = true;
            this.radLeft.Location = new System.Drawing.Point(346, 34);
            this.radLeft.Name = "radLeft";
            this.radLeft.Size = new System.Drawing.Size(14, 13);
            this.radLeft.TabIndex = 8;
            this.radLeft.UseVisualStyleBackColor = true;
            // 
            // radRight
            // 
            this.radRight.AutoSize = true;
            this.radRight.Location = new System.Drawing.Point(371, 34);
            this.radRight.Name = "radRight";
            this.radRight.Size = new System.Drawing.Size(14, 13);
            this.radRight.TabIndex = 9;
            this.radRight.UseVisualStyleBackColor = true;
            // 
            // radDown
            // 
            this.radDown.AutoSize = true;
            this.radDown.Location = new System.Drawing.Point(359, 46);
            this.radDown.Name = "radDown";
            this.radDown.Size = new System.Drawing.Size(14, 13);
            this.radDown.TabIndex = 10;
            this.radDown.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(355, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Up";
            // 
            // TestingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 584);
            this.Controls.Add(this.pnlGame);
            this.Controls.Add(this.pnControl);
            this.Name = "TestingForm";
            this.Text = "Bazinga";
            this.pnControl.ResumeLayout(false);
            this.pnControl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnControl;
        private System.Windows.Forms.TableLayoutPanel pnlGame;
        private System.Windows.Forms.Label lblInfo1;
        private System.Windows.Forms.ListBox lstMoveQueue;
        private System.Windows.Forms.Button btnMove;
        private System.Windows.Forms.Button btnAttack;
        private System.Windows.Forms.Button btnBlock;
        private System.Windows.Forms.Label lblInfo2;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.RadioButton radUp;
        private System.Windows.Forms.RadioButton radDown;
        private System.Windows.Forms.RadioButton radRight;
        private System.Windows.Forms.RadioButton radLeft;
        private System.Windows.Forms.Label label1;

    }
}

