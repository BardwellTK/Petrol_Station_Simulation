namespace Petrol_Somewhat_Unlimited_Ltd
{
    partial class Pump
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
            this.l_Title = new System.Windows.Forms.Label();
            this.l_available = new System.Windows.Forms.Label();
            this.l_static_LicensePlate = new System.Windows.Forms.Label();
            this.BoxLP = new System.Windows.Forms.TextBox();
            this.l_Status = new System.Windows.Forms.Label();
            this.l_Timer = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.PBAr = new System.Windows.Forms.Panel();
            this.l_Percentage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.PBAr.SuspendLayout();
            this.SuspendLayout();
            // 
            // l_Title
            // 
            this.l_Title.AutoSize = true;
            this.l_Title.Location = new System.Drawing.Point(4, 4);
            this.l_Title.Name = "l_Title";
            this.l_Title.Size = new System.Drawing.Size(35, 13);
            this.l_Title.TabIndex = 0;
            this.l_Title.Text = "label1";
            // 
            // l_available
            // 
            this.l_available.AutoSize = true;
            this.l_available.ForeColor = System.Drawing.Color.RoyalBlue;
            this.l_available.Location = new System.Drawing.Point(132, 4);
            this.l_available.Name = "l_available";
            this.l_available.Size = new System.Drawing.Size(50, 13);
            this.l_available.TabIndex = 1;
            this.l_available.Text = "Available";
            // 
            // l_static_LicensePlate
            // 
            this.l_static_LicensePlate.AutoSize = true;
            this.l_static_LicensePlate.Location = new System.Drawing.Point(2, 104);
            this.l_static_LicensePlate.Name = "l_static_LicensePlate";
            this.l_static_LicensePlate.Size = new System.Drawing.Size(74, 13);
            this.l_static_LicensePlate.TabIndex = 2;
            this.l_static_LicensePlate.Text = "License Plate:";
            // 
            // BoxLP
            // 
            this.BoxLP.BackColor = System.Drawing.Color.RoyalBlue;
            this.BoxLP.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.BoxLP.ForeColor = System.Drawing.Color.White;
            this.BoxLP.Location = new System.Drawing.Point(82, 104);
            this.BoxLP.Name = "BoxLP";
            this.BoxLP.ReadOnly = true;
            this.BoxLP.Size = new System.Drawing.Size(100, 13);
            this.BoxLP.TabIndex = 3;
            this.BoxLP.Text = "123";
            // 
            // l_Status
            // 
            this.l_Status.AutoSize = true;
            this.l_Status.Location = new System.Drawing.Point(6, 39);
            this.l_Status.Name = "l_Status";
            this.l_Status.Size = new System.Drawing.Size(28, 13);
            this.l_Status.TabIndex = 5;
            this.l_Status.Text = "Free";
            // 
            // l_Timer
            // 
            this.l_Timer.AutoSize = true;
            this.l_Timer.BackColor = System.Drawing.Color.White;
            this.l_Timer.Location = new System.Drawing.Point(152, 39);
            this.l_Timer.Name = "l_Timer";
            this.l_Timer.Size = new System.Drawing.Size(30, 13);
            this.l_Timer.TabIndex = 6;
            this.l_Timer.Text = "100s";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Petrol_Somewhat_Unlimited_Ltd.Properties.Resources.empty1;
            this.pictureBox1.Location = new System.Drawing.Point(63, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // PBAr
            // 
            this.PBAr.BackColor = System.Drawing.Color.Gray;
            this.PBAr.Controls.Add(this.l_Percentage);
            this.PBAr.Location = new System.Drawing.Point(3, 73);
            this.PBAr.Name = "PBAr";
            this.PBAr.Size = new System.Drawing.Size(179, 16);
            this.PBAr.TabIndex = 0;
            this.PBAr.Paint += new System.Windows.Forms.PaintEventHandler(this.ProgressPaint);
            // 
            // l_Percentage
            // 
            this.l_Percentage.AutoSize = true;
            this.l_Percentage.BackColor = System.Drawing.Color.Transparent;
            this.l_Percentage.ForeColor = System.Drawing.Color.White;
            this.l_Percentage.Location = new System.Drawing.Point(76, 0);
            this.l_Percentage.Name = "l_Percentage";
            this.l_Percentage.Size = new System.Drawing.Size(21, 13);
            this.l_Percentage.TabIndex = 0;
            this.l_Percentage.Text = "0%";
            // 
            // Pump
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.PBAr);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.l_Timer);
            this.Controls.Add(this.l_Status);
            this.Controls.Add(this.BoxLP);
            this.Controls.Add(this.l_static_LicensePlate);
            this.Controls.Add(this.l_available);
            this.Controls.Add(this.l_Title);
            this.Name = "Pump";
            this.Size = new System.Drawing.Size(185, 120);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.PBAr.ResumeLayout(false);
            this.PBAr.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label l_Title;
        private System.Windows.Forms.Label l_available;
        private System.Windows.Forms.Label l_static_LicensePlate;
        private System.Windows.Forms.TextBox BoxLP;
        private System.Windows.Forms.Label l_Status;
        private System.Windows.Forms.Label l_Timer;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel PBAr;
        private System.Windows.Forms.Label l_Percentage;
    }
}
