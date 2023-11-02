using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HFUTIEMES
{
    public partial class OutQueMonitor : Form
    {
        public string type = "";
        public string pline = "";
        public OutQueMonitor()
        {
            InitializeComponent();
        }

        private void EquipmentMonitored_Load(object sender, EventArgs e)
        {
            loadGridView2();
        }
        private void loadGridView2()
        {
            DataTable dt = new DataTable();
            string sql = "";
            if (pline != "")
            {
                if (type == "上线数量")
                {
                    sql = "select ProductBornCode,ProductDrawCode,ProductName,ProductModel,SerialCode,ProductType,Starttime from Product_Online_P  where Starttime>'" + DateTime.Now.Date+ "' and  reserve5='" + pline + "'";

                }
                else if (type == "下线数量")
                {
                    sql = "select ProductBornCode,ProductDrawCode,ProductName,ProductModel,SerialCode,ProductType,Starttime from Product_Online_P  where remark='是' and Endtime>'" + DateTime.Now.Date + "' and  reserve5='" + pline + "'";
                    
                }
                else if (type == "线上数量")
                {
                    sql = "select ProductBornCode,ProductDrawCode,ProductName,ProductModel,SerialCode,ProductType,Starttime from Product_Online_P  where remark='否' and reserve5='" + pline + "'";
                }
                dt = DbHelperSQL.OpenTable(sql);
                if (dt.Rows.Count > 0)
                {
                    DataTable OperStructure = new DataTable();//重新构造的datatable,
                    OperStructure.Columns.Add("产品出生证");
                    OperStructure.Columns.Add("产品图号");
                    OperStructure.Columns.Add("产品名称");
                    OperStructure.Columns.Add("产品机型");
                    OperStructure.Columns.Add("产品流水号");
                    OperStructure.Columns.Add("产品类型");
                    OperStructure.Columns.Add("上线时间");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = OperStructure.NewRow();
                        dr["产品出生证"] = dt.Rows[i][0].ToString();
                        dr["产品图号"] = dt.Rows[i][1].ToString();
                        dr["产品名称"] = dt.Rows[i][2].ToString();
                        dr["产品机型"] = dt.Rows[i][3].ToString();
                        dr["产品流水号"] = dt.Rows[i][4].ToString();
                        dr["产品类型"] = dt.Rows[i][5].ToString();
                        dr["上线时间"] = dt.Rows[i][6].ToString();
                        OperStructure.Rows.Add(dr);
                    }
                    dataGridView2.DataSource = OperStructure;
                    dataGridView2.EnableHeadersVisualStyles = false;
                    dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
                    Operations.AutoSizeDataGridView(dataGridView2);
                }
            }
           
        }
        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dataGridView2.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(System.Convert.ToString(e.RowIndex + 1, System.Globalization.CultureInfo.CurrentUICulture),
                e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
            }
        }
    }
}
