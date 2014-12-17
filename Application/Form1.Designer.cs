namespace ImageApp
{
    partial class Form1
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
            this.box1 = new System.Windows.Forms.PictureBox();
            this.box2 = new System.Windows.Forms.PictureBox();
            this.box3 = new System.Windows.Forms.PictureBox();
            this.box4 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.box1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.box2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.box3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.box4)).BeginInit();
            this.SuspendLayout();
            // 
            // box1
            // 
            this.box1.Location = new System.Drawing.Point(12, 103);
            this.box1.Name = "box1";
            this.box1.Size = new System.Drawing.Size(416, 311);
            this.box1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.box1.TabIndex = 0;
            this.box1.TabStop = false;
            // 
            // box2
            // 
            this.box2.Location = new System.Drawing.Point(434, 103);
            this.box2.Name = "box2";
            this.box2.Size = new System.Drawing.Size(416, 311);
            this.box2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.box2.TabIndex = 9;
            this.box2.TabStop = false;
            // 
            // box3
            // 
            this.box3.Location = new System.Drawing.Point(856, 103);
            this.box3.Name = "box3";
            this.box3.Size = new System.Drawing.Size(416, 311);
            this.box3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.box3.TabIndex = 10;
            this.box3.TabStop = false;
            // 
            // box4
            // 
            this.box4.Location = new System.Drawing.Point(1278, 103);
            this.box4.Name = "box4";
            this.box4.Size = new System.Drawing.Size(416, 311);
            this.box4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.box4.TabIndex = 11;
            this.box4.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1805, 818);
            this.Controls.Add(this.box4);
            this.Controls.Add(this.box3);
            this.Controls.Add(this.box2);
            this.Controls.Add(this.box1);
            this.Name = "Form1";
            this.Text = "Image Analysis";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.box1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.box2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.box3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.box4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox box1;
        private System.Windows.Forms.PictureBox box2;
        private System.Windows.Forms.PictureBox box3;
        private System.Windows.Forms.PictureBox box4;
    }
}

