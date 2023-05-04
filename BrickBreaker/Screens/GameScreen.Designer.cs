namespace BrickBreaker
{
    partial class GameScreen
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.livesBox1 = new System.Windows.Forms.PictureBox();
            this.livesBox2 = new System.Windows.Forms.PictureBox();
            this.livesBox3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.livesBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.livesBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.livesBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // gameTimer
            // 
            this.gameTimer.Enabled = true;
            this.gameTimer.Interval = 1;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // livesBox1
            // 
            this.livesBox1.BackColor = System.Drawing.Color.Transparent;
            this.livesBox1.Location = new System.Drawing.Point(14, 740);
            this.livesBox1.Name = "livesBox1";
            this.livesBox1.Size = new System.Drawing.Size(40, 40);
            this.livesBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.livesBox1.TabIndex = 0;
            this.livesBox1.TabStop = false;
            // 
            // livesBox2
            // 
            this.livesBox2.BackColor = System.Drawing.Color.Transparent;
            this.livesBox2.Location = new System.Drawing.Point(60, 740);
            this.livesBox2.Name = "livesBox2";
            this.livesBox2.Size = new System.Drawing.Size(40, 40);
            this.livesBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.livesBox2.TabIndex = 1;
            this.livesBox2.TabStop = false;
            // 
            // livesBox3
            // 
            this.livesBox3.BackColor = System.Drawing.Color.Transparent;
            this.livesBox3.Location = new System.Drawing.Point(106, 740);
            this.livesBox3.Name = "livesBox3";
            this.livesBox3.Size = new System.Drawing.Size(40, 40);
            this.livesBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.livesBox3.TabIndex = 2;
            this.livesBox3.TabStop = false;
            // 
            // GameScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.livesBox3);
            this.Controls.Add(this.livesBox2);
            this.Controls.Add(this.livesBox1);
            this.DoubleBuffered = true;
            this.Name = "GameScreen";
            this.Size = new System.Drawing.Size(1386, 788);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GameScreen_Paint);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GameScreen_KeyUp);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.GameScreen_PreviewKeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.livesBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.livesBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.livesBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.PictureBox livesBox1;
        private System.Windows.Forms.PictureBox livesBox2;
        private System.Windows.Forms.PictureBox livesBox3;
    }
}
