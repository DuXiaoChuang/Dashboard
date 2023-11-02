namespace HFUTIEMES
{
    partial class DatabaseTable
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
            this.CANCELButt = new System.Windows.Forms.Button();
            this.OKButt = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbServerName = new System.Windows.Forms.ComboBox();
            this.btnConnServer = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CANCELButt
            // 
            this.CANCELButt.Location = new System.Drawing.Point(422, 491);
            this.CANCELButt.Name = "CANCELButt";
            this.CANCELButt.Size = new System.Drawing.Size(75, 23);
            this.CANCELButt.TabIndex = 20;
            this.CANCELButt.Text = "取消";
            this.CANCELButt.UseVisualStyleBackColor = true;
            // 
            // OKButt
            // 
            this.OKButt.Location = new System.Drawing.Point(120, 491);
            this.OKButt.Name = "OKButt";
            this.OKButt.Size = new System.Drawing.Size(75, 23);
            this.OKButt.TabIndex = 21;
            this.OKButt.Text = "确定";
            this.OKButt.UseVisualStyleBackColor = true;
            this.OKButt.Click += new System.EventHandler(this.OKButt_Click);
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(0, 117);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox1.Size = new System.Drawing.Size(698, 352);
            this.listBox1.TabIndex = 19;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.cmbServerName);
            this.groupBox1.Controls.Add(this.btnConnServer);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(698, 117);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "连接数据库";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(86, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "检索数据库表格";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // cmbServerName
            // 
            this.cmbServerName.BackColor = System.Drawing.SystemColors.Window;
            this.cmbServerName.FormattingEnabled = true;
            this.cmbServerName.Location = new System.Drawing.Point(86, 64);
            this.cmbServerName.Name = "cmbServerName";
            this.cmbServerName.Size = new System.Drawing.Size(344, 20);
            this.cmbServerName.TabIndex = 17;
            // 
            // btnConnServer
            // 
            this.btnConnServer.Location = new System.Drawing.Point(465, 62);
            this.btnConnServer.Name = "btnConnServer";
            this.btnConnServer.Size = new System.Drawing.Size(75, 23);
            this.btnConnServer.TabIndex = 16;
            this.btnConnServer.Text = "检索字段";
            this.btnConnServer.UseVisualStyleBackColor = true;
            this.btnConnServer.Click += new System.EventHandler(this.btnConnServer_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "表格:";
            // 
            // DatabaseTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 526);
            this.Controls.Add(this.CANCELButt);
            this.Controls.Add(this.OKButt);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.groupBox1);
            this.Name = "DatabaseTable";
            this.Text = "DatabaseTable";
            this.Load += new System.EventHandler(this.DatabaseTable_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CANCELButt;
        private System.Windows.Forms.Button OKButt;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmbServerName;
        private System.Windows.Forms.Button btnConnServer;
        private System.Windows.Forms.Label label6;
    }
}