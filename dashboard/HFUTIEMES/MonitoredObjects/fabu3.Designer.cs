namespace HFUTIEMES
{
    partial class fabu3
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timerMonitor = new System.Windows.Forms.Timer(this.components);
            this.designer1 = new Dalssoft.DiagramNet.Designer();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 3000;
            // 
            // timerMonitor
            // 
            this.timerMonitor.Interval = 3000;
            this.timerMonitor.Tick += new System.EventHandler(this.timerMonitor_Tick);
            // 
            // designer1
            // 
            this.designer1.AutoScroll = true;
            this.designer1.BackColor = System.Drawing.SystemColors.Window;
            this.designer1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.designer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.designer1.Location = new System.Drawing.Point(0, 0);
            this.designer1.Name = "designer1";
            this.designer1.ShowGrid = Dalssoft.DiagramNet.IsShowGrid.否;
            this.designer1.Size = new System.Drawing.Size(1360, 750);
            this.designer1.TabIndex = 0;
            this.designer1.Type = Dalssoft.DiagramNet.TypeOfDesigner.配置;
            this.designer1.Click += new System.EventHandler(this.designer1_Click);
            // 
            // fabu3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1360, 750);
            this.Controls.Add(this.designer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "fabu3";
            this.Text = "可视化监控";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fabu3_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.fabu3_FormClosed);
            this.Load += new System.EventHandler(this.fabu3_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Dalssoft.DiagramNet.Designer designer1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timerMonitor;
    }
}