
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace HFUTIEMES
{
    public partial class EquipmentMonitoredDlg : Form
    {
        public string MoniteredObjectID = "";
        public string MoniteredObjectCode = "";
        public string MoniteredObjectName = "";
        public EquipmentMonitoredDlg()
        {
            InitializeComponent();
        }
        private void Equipment_Load(object sender, EventArgs e)
        {
            showInfor();
            loadGridView1();
            //loadpic();
        }
        //public void loadpic()
        //{
        //    connect();
        //    if (!System.IO.Directory.Exists(Application.StartupPath + "\\pic"))
        //    {
        //        System.IO.Directory.CreateDirectory(Application.StartupPath + "\\pic");//不存在就创建目录 


        //    }




        //    WebClient client = new WebClient();


        //    //打开数据库


        //    string sql = "select  EquipCode from   Equipment_B where [EquipId]='" + MoniteredObjectID + "' ";
        //    //string sql = "select  * from  SELF_PRODUCE_CHECK_PIC   ";
        //    DataTable dt = DbHelperSQL.OpenTable(sql);
        //    if (dt.Rows.Count > 0)
        //    {


        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            try
        //            {

        //                string strUrlFilePath = "\\\\192.168.1.3\\pic\\" + dt.Rows[0][0].ToString();
        //                WebRequest myWebRequest = WebRequest.Create(strUrlFilePath);
        //                string path = Application.StartupPath + "\\pic\\" + dr[0].ToString();


        //                client.DownloadFile(strUrlFilePath, path);//(服务器存放图片路径，本地下载保存图片路径)


        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show(ex.ToString());

        //            }
        //        }
        //    }
        //    // pictur
        //    //string sql = "select  EquipCode from   Equipment_B where [EquipId]='" + MoniteredObjectID + "' ";
        //    //string sql = "select  * from  SELF_PRODUCE_CHECK_PIC   ";
        //    //DataTable dt = DbHelperSQL.OpenTable(sql);
        //    //pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\pic\\" + dt.Rows[0][0].ToString());//这一句到时等上层传图做好了我就可以直接把屏蔽撤销就可以了。
        //    //string sql = "select  * from   Equipment_B where [EquipId]='" + MoniteredObjectID + "' ";
        //    ////string sql = "select  * from  SELF_PRODUCE_CHECK_PIC   ";
        //    //DataTable dt = DbHelperSQL.OpenTable(sql);
        //    //if (dt.Rows.Count > 0)
        //    //{
        //    //    foreach (DataRow dr in dt.Rows)
        //    //    {
        //    //        string path = Application.StartupPath + "\\pic\\inspection\\" + dr[4].ToString();
        //    //        //list.Add(path);
        //    //        //txt.Add(dr[8].ToString());
        //    //        //client.DownloadFile(strUrlFilePath, path);
        //    //        //pictureBox1.Image = Image.FromFile(list[0].ToString());
        //    //    }
        //    //}



        //    //byte[] imagebytes = null;
        //    ////打开数据库
        //    //SqlConnection con = new SqlConnection(data.data.ConnStr);
        //    //con.Open();
        //    //SqlCommand com = new SqlCommand("select  pic from Equipment_B where EquipId='" + MoniteredObjectID + "' ", con);
        //    //SqlDataReader dr = com.ExecuteReader();//获取到了指定sign_code那一行的信息存到dr中
        //    //while (dr.Read())
        //    //{
        //    //    if (dr.GetValue(0).ToString() != "")
        //    //    {
        //    //        imagebytes = (byte[])dr.GetValue(0);//将该行第二列的的数据图片赋给imagebytes
        //    //    }
        //    //}
        //    //dr.Close();
        //    //com.Clone();
        //    //con.Close();//关闭数据库连接
        //    //if (imagebytes != null)
        //    //{
        //    //    MemoryStream ms = new MemoryStream(imagebytes);
        //    //    Bitmap bmpt = new Bitmap(ms);//转换了数据类型
        //    //    pictureEdit1.Image = bmpt;
        //    //}
        //    //else { Bitmap bmpt = null; pictureEdit1.Image = bmpt; }


        //}
        //public void connect()
        //{
        //    bool Flag = true;
        //    string remoteHost = "192.168.1.3";
        //    string userName = "Administrator";
        //    string passWord = "2016IEmes";
        //    Process proc = new Process();
        //    proc.StartInfo.FileName = "cmd.exe";
        //    proc.StartInfo.UseShellExecute = false;
        //    proc.StartInfo.RedirectStandardInput = true;
        //    proc.StartInfo.RedirectStandardOutput = true;
        //    proc.StartInfo.RedirectStandardError = true;
        //    proc.StartInfo.CreateNoWindow = true;
        //    try
        //    {
        //        proc.Start();
        //        string command = @"net  use  \\" + remoteHost + "  " + passWord + "  " + "  /user:" + userName + ">NUL";
        //        proc.StandardInput.WriteLine(command);
        //        command = "exit";
        //        proc.StandardInput.WriteLine(command);
        //        while (proc.HasExited == false)
        //        {
        //            proc.WaitForExit(1000);
        //        }
        //        string errormsg = proc.StandardError.ReadToEnd();
        //        if (errormsg != "")
        //            Flag = false;
        //        proc.StandardError.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        Flag = false;
        //    }
        //    if (Flag == false)
        //    {
        //        MessageBox.Show("远程服务失败！！！");
        //        return;
        //    }
        //}
        private void showInfor()
        {
            //pictureBox1.ImageLocation = Application.StartupPath + "\\Image\\OP010.png";

            DataTable dt2 = DbHelperSQL.OpenTable("select EquipCode,EquipName,Description,Maker,ManuTel from Equipment_B where EquipId='" + MoniteredObjectID + "'");
            if (dt2.Rows.Count > 0)
            {
                string a= dt2.Rows[0]["EquipCode"].ToString().Trim();
                if ( File.Exists(Application.StartupPath +"\\Image\\"+a+".png"))//判断文件是否存在。
                {
                    pictureBox1.ImageLocation = Application.StartupPath + "\\Image\\" + a + ".png";
                }
                else
                {
                   pictureBox1.ImageLocation = Application.StartupPath + "\\Image\\1.png";
                }
                label01.Text = dt2.Rows[0]["EquipCode"].ToString();
                label02.Text = dt2.Rows[0]["EquipName"].ToString();
                label05.Text = dt2.Rows[0]["Description"].ToString();
                label06.Text = dt2.Rows[0]["Maker"].ToString();
                label07.Text = dt2.Rows[0]["ManuTel"].ToString();
               
            }
            else
            {
                label01.Text = "";
                label02.Text = "";

                label05.Text = "";
                label06.Text = "";
                label07.Text = "";
            }
            DataTable dt = DbHelperSQL.OpenTable("select b.WorkCenterCode,b.WorkCenterName from Equipment_B a,WorkCenter_B b where a.WorkCenterId=b.WorkCenterId and a.EquipId='" + MoniteredObjectID + "'");
            if (dt.Rows.Count > 0)
            {
                label03.Text = dt.Rows[0]["WorkCenterCode"].ToString();
                label04.Text = dt.Rows[0]["WorkCenterName"].ToString();
            }
            else
            {
                label03.Text = "";
                label04.Text = "";
            }
            DataTable dt1 = DbHelperSQL.OpenTable("select AddressContent,StateStarttime,getdate(),DATEDIFF(s,StateStarttime,getdate()) from Equip_State_P where EquipId='" + MoniteredObjectID + "' and StateDescription='运行状态'");
            if (dt1.Rows.Count > 0)
            {
                label015.Text = dt1.Rows[0]["AddressContent"].ToString();
                label016.Text = dt1.Rows[0]["StateStarttime"].ToString();
                TimeSpan d = System.Convert.ToDateTime(dt1.Rows[0][2]) - System.Convert.ToDateTime(dt1.Rows[0][1]);
                label018.Text = d.Days +"天"+ d.Hours +"小时"+d.Minutes +"分钟"+d.Minutes +"秒";
                
            }
            else
            {
                label015.Text = "";
                label016.Text = "";
                label018.Text = "";
            }
        }

        private void loadGridView1()
        {
            string sql = "select a.EquipCode,a.EquipName,a.StateType,a.AddressContent,a.StateStarttime,c.WorkCenterCode,c.WorkCenterName from Equip_State_P a,Equipment_B b,WorkCenter_B c where a.EquipId='" + MoniteredObjectID + "' and a.EquipId=b.EquipId and c.WorkCenterId=b.WorkCenterId and StateDescription='设备报警'";
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

        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dataGridView2.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(System.Convert.ToString(e.RowIndex + 1, System.Globalization.CultureInfo.CurrentUICulture),
                e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
            }
        }

        private void dataGridView3_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dataGridView3.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(System.Convert.ToString(e.RowIndex + 1, System.Globalization.CultureInfo.CurrentUICulture),
                e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //dataGridView3.Rows.Clear();
            DateTime start = DateTime.Parse(dateTimePicker4.Value.ToString("yyyy-MM-dd 00:00"));
            DateTime end = DateTime.Parse(dateTimePicker3.Value.ToString("yyyy-MM-dd 23:59"));
            string sql = "select EquipCode,EquipName,ProductBornCode,ProductName,ProductModel,SerialCode,ToolCode,ToolManufacturer,ToolResidualLife,remark,SettingLife,ToolLength,ToolWearLength,ToolRadius,ToolWearRadius,ActualTime,MachineTime from Tool_Machine_D where EquipId='" + MoniteredObjectID + "' and (MachineTime BETWEEN '" + start + "' and '" + end + "') order by MachineTime desc  ";
            DataTable dt1 = new DataTable();
            dt1 = DbHelperSQL.OpenTable(sql);
            if (dt1.Rows.Count > 0)
            {
                DataTable OperStructure = new DataTable();//重新构造的datatable,
                OperStructure.Columns.Add("设备编号");
                OperStructure.Columns.Add("设备名称");
                OperStructure.Columns.Add("产品出生证号");
                OperStructure.Columns.Add("产品名称");
                OperStructure.Columns.Add("产品机型");
                OperStructure.Columns.Add("产品流水号");
                OperStructure.Columns.Add("刀具编号");
                OperStructure.Columns.Add("刀具制造商");
                OperStructure.Columns.Add("刀具剩余寿命");
                OperStructure.Columns.Add("刀具剩余寿命预警");
                OperStructure.Columns.Add("最大寿命");
                OperStructure.Columns.Add("刀具刀长");
                OperStructure.Columns.Add("刀补尺寸");
                OperStructure.Columns.Add("刀具直径");
                OperStructure.Columns.Add("直径补偿尺寸");
                OperStructure.Columns.Add("实际节拍");
                OperStructure.Columns.Add("采集时间");
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    DataRow dr = OperStructure.NewRow();
                    dr["设备编号"] = dt1.Rows[i]["EquipCode"].ToString();
                    dr["设备名称"] = dt1.Rows[i]["EquipName"].ToString();
                    dr["产品出生证号"] = dt1.Rows[i]["ProductBornCode"].ToString();
                    dr["产品名称"] = dt1.Rows[i]["ProductName"].ToString();
                    dr["产品机型"] = dt1.Rows[i]["ProductModel"].ToString();
                    dr["产品流水号"] = dt1.Rows[i]["SerialCode"].ToString();
                    dr["刀具编号"] = dt1.Rows[i]["ToolCode"].ToString();
                    dr["刀具制造商"] = dt1.Rows[i]["ToolManufacturer"].ToString();
                    dr["刀具剩余寿命"] = dt1.Rows[i]["ToolResidualLife"].ToString();
                    dr["刀具剩余寿命预警"] = dt1.Rows[i]["remark"].ToString();
                    dr["最大寿命"] = dt1.Rows[i]["SettingLife"].ToString();
                    dr["刀具刀长"] = dt1.Rows[i]["ToolLength"].ToString();
                    dr["刀补尺寸"] = dt1.Rows[i]["ToolWearLength"].ToString();
                    dr["刀具直径"] = dt1.Rows[i]["ToolRadius"].ToString();
                    dr["直径补偿尺寸"] = dt1.Rows[i]["ToolWearRadius"].ToString();
                    dr["实际节拍"] = dt1.Rows[i]["ActualTime"].ToString();
                    dr["采集时间"] = dt1.Rows[i]["MachineTime"].ToString();
                    OperStructure.Rows.Add(dr);
                }
                dataGridView3.DataSource = OperStructure;
                dataGridView3.EnableHeadersVisualStyles = false;
                dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
                Operations.AutoSizeDataGridView(dataGridView3);
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            //dataGridView2.Rows.Clear();
            DateTime start = DateTime.Parse(dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00"));
            DateTime end = DateTime.Parse(dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59"));
            string sql = "select a.EquipCode,a.EquipName,a.StateType,a.AddressContent,a.StateStarttime,a.StateEndtime,a.ContinueTime,c.WorkCenterCode,c.WorkCenterName from Equip_State_P a,Equipment_B b,WorkCenter_B c where a.EquipId='" + MoniteredObjectID + "' and a.EquipId=b.EquipId and c.WorkCenterId=b.WorkCenterId and (a.StateStarttime BETWEEN '" + start + "' and '" + end + "') order by a.StateStarttime desc  ";
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
                OperStructure.Columns.Add("结束时间");
                OperStructure.Columns.Add("持续时间");
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
                    dr["结束时间"] = dt0.Rows[i]["StateEndtime"].ToString();
                    dr["持续时间"] = dt0.Rows[i]["ContinueTime"].ToString();
                    OperStructure.Rows.Add(dr);
                }
                dataGridView2.DataSource = OperStructure;
                dataGridView2.EnableHeadersVisualStyles = false;
                dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
                Operations.AutoSizeDataGridView(dataGridView2);
            }
        }

    }
}
