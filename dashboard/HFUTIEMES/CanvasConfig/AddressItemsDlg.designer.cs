namespace HFUTIEMES
{
    partial class AddressItemsDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddressItemsDlg));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbServerName = new System.Windows.Forms.ComboBox();
            this.btnConnServer = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.OKButt = new System.Windows.Forms.Button();
            this.CANCELButt = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbServerName);
            this.groupBox1.Controls.Add(this.btnConnServer);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(572, 56);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "连接OPC服务器";
            // 
            // cmbServerName
            // 
            this.cmbServerName.FormattingEnabled = true;
            this.cmbServerName.Location = new System.Drawing.Point(86, 20);
            this.cmbServerName.Name = "cmbServerName";
            this.cmbServerName.Size = new System.Drawing.Size(344, 20);
            this.cmbServerName.TabIndex = 17;
            this.cmbServerName.SelectedIndexChanged += new System.EventHandler(this.cmbServerName_SelectedIndexChanged);
            // 
            // btnConnServer
            // 
            this.btnConnServer.Enabled = false;
            this.btnConnServer.Location = new System.Drawing.Point(465, 18);
            this.btnConnServer.Name = "btnConnServer";
            this.btnConnServer.Size = new System.Drawing.Size(75, 23);
            this.btnConnServer.TabIndex = 16;
            this.btnConnServer.Text = "连接";
            this.btnConnServer.UseVisualStyleBackColor = true;
            this.btnConnServer.Click += new System.EventHandler(this.btnConnServer_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "服务器:";
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(5, 61);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox1.Size = new System.Drawing.Size(572, 352);
            this.listBox1.TabIndex = 16;
            // 
            // OKButt
            // 
            this.OKButt.Location = new System.Drawing.Point(91, 424);
            this.OKButt.Name = "OKButt";
            this.OKButt.Size = new System.Drawing.Size(75, 23);
            this.OKButt.TabIndex = 17;
            this.OKButt.Text = "确定";
            this.OKButt.UseVisualStyleBackColor = true;
            this.OKButt.Click += new System.EventHandler(this.OKButt_Click);
            // 
            // CANCELButt
            // 
            this.CANCELButt.Location = new System.Drawing.Point(401, 424);
            this.CANCELButt.Name = "CANCELButt";
            this.CANCELButt.Size = new System.Drawing.Size(75, 23);
            this.CANCELButt.TabIndex = 17;
            this.CANCELButt.Text = "取消";
            this.CANCELButt.UseVisualStyleBackColor = true;
            this.CANCELButt.Click += new System.EventHandler(this.CANCELButt_Click);
            // 
            // AddressItemsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 455);
            this.Controls.Add(this.CANCELButt);
            this.Controls.Add(this.OKButt);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddressItemsDlg";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加ITEMS";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TagItemsAdd_FormClosed);
            this.Load += new System.EventHandler(this.TagItemsAdd_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbServerName;
        private System.Windows.Forms.Button btnConnServer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button OKButt;
        private System.Windows.Forms.Button CANCELButt;
    }
}