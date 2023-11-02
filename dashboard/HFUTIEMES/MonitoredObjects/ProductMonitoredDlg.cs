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
    public partial class ProductMonitoredDlg : Form
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

        public ProductMonitoredDlg()
        {
            InitializeComponent();
        }
        private void loadGridView1()
        {
            string sql = "select ProductBornCode,ProductDrawCode,ProductName,ProductModel,SerialCode,WorkCenterCode,WorkCenterName,EquipCode,EquipName from Product_Route_P order by Starttime";
            DataTable dt0 = new DataTable();
            dt0 = DbHelperSQL.OpenTable(sql);
            if (dt0.Rows.Count > 0)
            {
                DataTable OperStructure = new DataTable();//重新构造的datatable,
                OperStructure.Columns.Add("产品出生证");
                OperStructure.Columns.Add("产品图号");
                OperStructure.Columns.Add("产品名称");
                OperStructure.Columns.Add("产品机型");
                OperStructure.Columns.Add("流水号");
                OperStructure.Columns.Add("工位编号");
                OperStructure.Columns.Add("工位名称");
                OperStructure.Columns.Add("设备编号");
                OperStructure.Columns.Add("设备名称");
                for (int i = 0; i < dt0.Rows.Count; i++)
                {
                    DataRow dr = OperStructure.NewRow();
                    dr["产品出生证"] = dt0.Rows[i]["ProductBornCode"].ToString();
                    dr["产品图号"] = dt0.Rows[i]["ProductDrawCode"].ToString();
                    dr["产品名称"] = dt0.Rows[i]["ProductName"].ToString();
                    dr["产品机型"] = dt0.Rows[i]["ProductModel"].ToString();
                    dr["流水号"] = dt0.Rows[i]["SerialCode"].ToString();
                    dr["工位编号"] = dt0.Rows[i]["WorkCenterCode"].ToString();
                    dr["工位名称"] = dt0.Rows[i]["WorkCenterName"].ToString();
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

        private void ProductMonitoredDlg_Load(object sender, EventArgs e)
        {
            loadGridView1();
        }

    }
}
