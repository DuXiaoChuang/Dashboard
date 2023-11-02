namespace HFUTIEMES
{
    partial class SetData
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.sjk = new System.Windows.Forms.ComboBox();
            this.mm = new System.Windows.Forms.TextBox();
            this.yh = new System.Windows.Forms.TextBox();
            this.fwq = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(154, 122);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 19;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(33, 122);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // sjk
            // 
            this.sjk.FormattingEnabled = true;
            this.sjk.Location = new System.Drawing.Point(119, 85);
            this.sjk.Name = "sjk";
            this.sjk.Size = new System.Drawing.Size(121, 20);
            this.sjk.TabIndex = 17;
            this.sjk.Text = "IEMES";
            this.sjk.DropDown += new System.EventHandler(this.sjk_DropDown);
            // 
            // mm
            // 
            this.mm.Location = new System.Drawing.Point(119, 58);
            this.mm.Name = "mm";
            this.mm.Size = new System.Drawing.Size(121, 21);
            this.mm.TabIndex = 16;
            this.mm.Text = "**";
            this.mm.UseSystemPasswordChar = true;
            // 
            // yh
            // 
            this.yh.Location = new System.Drawing.Point(119, 31);
            this.yh.Name = "yh";
            this.yh.Size = new System.Drawing.Size(121, 21);
            this.yh.TabIndex = 15;
            this.yh.Text = "sa";
            // 
            // fwq
            // 
            this.fwq.FormattingEnabled = true;
            this.fwq.Location = new System.Drawing.Point(119, 5);
            this.fwq.Name = "fwq";
            this.fwq.Size = new System.Drawing.Size(121, 20);
            this.fwq.TabIndex = 14;
            this.fwq.Text = "(local)";
            this.fwq.SelectedIndexChanged += new System.EventHandler(this.fwq_SelectedIndexChanged);
            this.fwq.TextUpdate += new System.EventHandler(this.fwq_TextUpdate);
            this.fwq.DropDown += new System.EventHandler(this.fwq_DropDown_1);
            this.fwq.TextChanged += new System.EventHandler(this.fwq_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "数据库名称";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "数据库服务器密码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "数据库服务器用户";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "数据库服务器名称";
            // 
            // SetData
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(263, 155);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.sjk);
            this.Controls.Add(this.mm);
            this.Controls.Add(this.yh);
            this.Controls.Add(this.fwq);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据库设置";
            this.Load += new System.EventHandler(this.SetDate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox sjk;
        private System.Windows.Forms.TextBox mm;
        private System.Windows.Forms.TextBox yh;
        private System.Windows.Forms.ComboBox fwq;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}