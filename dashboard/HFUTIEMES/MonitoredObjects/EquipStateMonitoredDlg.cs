using Dalssoft.DiagramNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace HFUTIEMES
{
    public partial class EquipStateMonitoredDlg : Form
    {
       public string A = "";
        public string MoniteredObjectCode = "";
        public string MoniteredObjectName = "";
        public string B = "";
        public EquipStateMonitoredDlg()
        {
            InitializeComponent();
        }
        private void Equipment_Load(object sender, EventArgs e)
        {
            loadGridView1();
        }

        private void loadGridView1()
        {
            string sql = "";
            if (A != "")
            {
                 if (B.ToString() == "无")
                     sql = "select a.EquipCode,a.EquipName,a.StateType,a.AddressContent,a.StateStarttime,c.WorkCenterCode,c.WorkCenterName from Equip_State_P a,Equipment_B b,WorkCenter_B c where a.EquipId=b.EquipId and c.WorkCenterId=b.WorkCenterId and a.reserve5='" + A + "' and StateDescription='设备报警'";
            else if (B.ToString() == "区域1")
                    sql = "select a.EquipCode,a.EquipName,a.StateType,a.AddressContent,a.StateStarttime,c.WorkCenterCode,c.WorkCenterName from Equip_State_P a,Equipment_B b,WorkCenter_B c where a.EquipId=b.EquipId and c.WorkCenterId=b.WorkCenterId and a.reserve5='" + A + "' and (a.equipcode in (select equipcode from EquipMent_B where zone=1))and StateDescription='设备报警'";
                else if (B.ToString() == "区域2")
                    sql = "select a.EquipCode,a.EquipName,a.StateType,a.AddressContent,a.StateStarttime,c.WorkCenterCode,c.WorkCenterName from Equip_State_P a,Equipment_B b,WorkCenter_B c where a.EquipId=b.EquipId and c.WorkCenterId=b.WorkCenterId and a.reserve5='" + A + "' and (a.equipcode in (select equipcode from EquipMent_B where zone=2))and StateDescription='设备报警'";
                else if (B.ToString() == "区域3")
                    sql = "select a.EquipCode,a.EquipName,a.StateType,a.AddressContent,a.StateStarttime,c.WorkCenterCode,c.WorkCenterName from Equip_State_P a,Equipment_B b,WorkCenter_B c where a.EquipId=b.EquipId and c.WorkCenterId=b.WorkCenterId and a.reserve5='" + A + "' and (a.equipcode in (select equipcode from EquipMent_B where zone=3))and StateDescription='设备报警'";
            }
            else
            {

                if (B.ToString() == "无")
                    sql = "select a.EquipCode,a.EquipName,a.StateType,a.AddressContent,a.StateStarttime,c.WorkCenterCode,c.WorkCenterName from Equip_State_P a,Equipment_B b,WorkCenter_B c where a.EquipId=b.EquipId and c.WorkCenterId=b.WorkCenterId and a.reserve5='" + A + "' and StateDescription='设备报警'";
                else if (B.ToString() == "区域1")
                    sql = "select a.EquipCode,a.EquipName,a.StateType,a.AddressContent,a.StateStarttime,c.WorkCenterCode,c.WorkCenterName from Equip_State_P a,Equipment_B b,WorkCenter_B c where a.EquipId=b.EquipId and c.WorkCenterId=b.WorkCenterId and  a.reserve5='" + A + "'and(a.equipcode in (select equipcode from EquipMent_B where zone=1))and StateDescription='设备报警'";
                else if (B.ToString() == "区域2")
                    sql = "select a.EquipCode,a.EquipName,a.StateType,a.AddressContent,a.StateStarttime,c.WorkCenterCode,c.WorkCenterName from Equip_State_P a,Equipment_B b,WorkCenter_B c where a.EquipId=b.EquipId and c.WorkCenterId=b.WorkCenterId and  a.reserve5='" + A + "'and (a.equipcode in (select equipcode from EquipMent_B where zone=2))and StateDescription='设备报警'";
                else if (B.ToString() == "区域3")
                    sql = "select a.EquipCode,a.EquipName,a.StateType,a.AddressContent,a.StateStarttime,c.WorkCenterCode,c.WorkCenterName from Equip_State_P a,Equipment_B b,WorkCenter_B c where a.EquipId=b.EquipId and c.WorkCenterId=b.WorkCenterId and  a.reserve5='" + A + "' and (a.equipcode in (select equipcode from EquipMent_B where zone=3))and StateDescription='设备报警'";
            }
            DataTable dt0 = new DataTable();
            dt0 = DbHelperSQL.OpenTable(sql);
            if (dt0.Rows.Count > 0)
            {
                DataTable OperStructure = new DataTable();//重新构造的datatable,
                OperStructure.Columns.Add("设备编号");
                OperStructure.Columns.Add("设备名称");
                OperStructure.Columns.Add("工位编号");
                OperStructure.Columns.Add("工位名称");
                OperStructure.Columns.Add("状态类型");
                OperStructure.Columns.Add("报警内容");
                OperStructure.Columns.Add("开始时间");
                for (int i = 0; i < dt0.Rows.Count; i++)
                {
                    DataRow dr = OperStructure.NewRow();
                    dr["设备编号"] = dt0.Rows[i]["EquipCode"].ToString();
                    dr["设备名称"] = dt0.Rows[i]["EquipName"].ToString();
                    dr["工位编号"] = dt0.Rows[i]["WorkCenterCode"].ToString();
                    dr["工位名称"] = dt0.Rows[i]["WorkCenterName"].ToString();
                    dr["状态类型"] = dt0.Rows[i]["StateType"].ToString();
                    dr["报警内容"] = dt0.Rows[i]["AddressContent"].ToString();
                    dr["开始时间"] = dt0.Rows[i]["StateStarttime"].ToString();
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
