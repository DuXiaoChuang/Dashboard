using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HFUTIEMES
{
    public partial class DatabaseTable : Form
    {
        public string fieldKey = "";
        public string TableKey = "";
        public int ok = 0;
        public DatabaseTable()
        {
            InitializeComponent();
        }

        private void DatabaseTable_Load(object sender, EventArgs e)
        {
            try
            {
                string sql = "select * from sys.tables order by name";//装载所有的表

                DataTable dt0 = data.DBQuery.OpenTable1(sql);
                for (int i = 0; i < dt0.Rows.Count; i++)
                {
                    cmbServerName.Items.Add(dt0 .Rows[i]["name"].ToString());
                }

                cmbServerName.SelectedIndex = 0;
                btnConnServer.Enabled = true;
            }
            catch (Exception err)
            {
                MessageBox.Show("检索数据库表出错：" + err.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnConnServer_Click(object sender, EventArgs e)
        {
            try
            {
                string sql1 = "select name from syscolumns where id=(select max(id) from sysobjects where name='" + cmbServerName.Text + "')";//通过表名查这个表的所有列名（字段名）

                DataTable dt0 = data.DBQuery.OpenTable1(sql1);
                for (int i = 0; i < dt0.Rows.Count; i++)
                {
                    listBox1.Items.Add(dt0 .Rows[i]["name"]);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("检索表字段出错：" + err.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void OKButt_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count == 1)
            {
                fieldKey = listBox1.SelectedItem.ToString();//字段名
                TableKey = cmbServerName.Text;//表名
                ok = 1;
                this.Close();
            }
            else
            {
                MessageBox.Show("未选择任何行！");
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
