using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Dalssoft.DiagramNet;
using System.Data.SqlClient;
using System.Threading;
using Opc;
using Opc.Da;
using OpcCom;
using System.Collections;
using System.IO;
using System.Xml;



namespace HFUTIEMES
{
    public partial class ToolStateMonitoredDlg : Form
    {
        public string MoniteredObjectID = "";
        public string MoniteredObjectCode = "";
        public string MoniteredObjectName = "";

        public int type = 0;//画布类型
        public string txt = @"/Log/可视化监控/";
        public string postfix = @".txt";
        public string canvasID, canvasCode, canvasName, parent = "";
        public Thread thread;
        private Document currentDocument = new Document();
        const int alert = 1;
        const int working = 2;
        const int none = 0;
        public string text = "";
        public System.Threading.Timer timer;

        public ToolStateMonitoredDlg()
        {
            InitializeComponent();
        }
        private void Equipment_Load(object sender, EventArgs e)
        {
            loadGridView1();
        }

        private void loadGridView1()
        {
            string sql = "select ToolCode,ToolResidualLife,lifeWarning,SettingLife,ToolLength,ToolWearLength,ToolRadius,ToolWearRadius,EquipCode,EquipName from Tool_State_P order by EquipCode";
            DataTable dt0 = new DataTable();
            dt0 = DbHelperSQL.OpenTable(sql);
            if (dt0.Rows.Count > 0)
            {
                DataTable OperStructure = new DataTable();//重新构造的datatable,
                OperStructure.Columns.Add("刀具编号");
                OperStructure.Columns.Add("剩余寿命");
                OperStructure.Columns.Add("剩余寿命预警");
                OperStructure.Columns.Add("最大寿命");
                OperStructure.Columns.Add("刀具刀长");
                OperStructure.Columns.Add("刀补尺寸");
                OperStructure.Columns.Add("刀具直径");
                OperStructure.Columns.Add("直径补偿尺寸");
                OperStructure.Columns.Add("设备编号");
                OperStructure.Columns.Add("设备名称");
                for (int i = 0; i < dt0.Rows.Count; i++)
                {
                    DataRow dr = OperStructure.NewRow();
                    dr["刀具编号"] = dt0.Rows[i]["ToolCode"].ToString();
                    dr["剩余寿命"] = dt0.Rows[i]["ToolResidualLife"].ToString();
                    dr["剩余寿命预警"] = dt0.Rows[i]["lifeWarning"].ToString();
                    dr["最大寿命"] = dt0.Rows[i]["SettingLife"].ToString();
                    dr["刀具刀长"] = dt0.Rows[i]["ToolLength"].ToString();
                    dr["刀补尺寸"] = dt0.Rows[i]["ToolWearLength"].ToString();
                    dr["刀具直径"] = dt0.Rows[i]["ToolRadius"].ToString();
                    dr["直径补偿尺寸"] = dt0.Rows[i]["ToolWearRadius"].ToString();
                    dr["设备编号"] = dt0.Rows[i]["EquipCode"].ToString();
                    dr["设备名称"] = dt0.Rows[i]["EquipName"].ToString();
                    OperStructure.Rows.Add(dr);
                }
                dataGridView1.DataSource = OperStructure;
                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
                Operations.AutoSizeDataGridView(dataGridView1);
            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dataGridView1.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(System.Convert.ToString(e.RowIndex + 1, System.Globalization.CultureInfo.CurrentUICulture),
                e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
            }
        }

    }
}
