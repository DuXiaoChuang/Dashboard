using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;

namespace HFUTIEMES
{
    public partial class CanvasDlg : Form
    {

        public int radiobutton = 0;
        public int radiobutton1 = 0;
        public bool isModified = false;
        public bool isMonitor = false;//是否监控
        public bool isStop = false;//是否常驻
        public string parentID;//父节点
        public string canvasID, canvasCode, canvasName = "";
        public int typeIndex = 0;
        ArrayList nameCollection = new ArrayList();//名称集合
        ArrayList codeCollection = new ArrayList();//编号集合
        ArrayList idCollection = new ArrayList();//ID集合


        public CanvasDlg()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked==true)
            {
                radiobutton = 1;
            }
            else
            {
                radiobutton = 0;
            }
            if (radioButton3.Checked == true)
            {
                radiobutton1 = 1;
            }
            else
            {
                radiobutton1= 0;
            }
           
            errorProvider1.Clear();
            if (txtCode.Text == string.Empty)
            {
                errorProvider1.SetError(txtCode, "画布编号不可为空!");
                return;
            }
            if (txtName.Text == string.Empty)
            {
                errorProvider1.SetError(txtName, "画布名称不可为空!");
                return;
            }
            string text = cmbType.SelectedItem.ToString();
            typeIndex = Convert.ToInt32((Dalssoft.DiagramNet.CanvasType)Enum.Parse(typeof(Dalssoft.DiagramNet.CanvasType), text));


            if (isModified)
            {
                if (txtCode.Text != canvasCode)
                {
                    string sql = "select canvas_code from AT_CANVAS_B where canvas_code='" + txtCode.Text + "' ";
                    DataTable dt = data.DBQuery.OpenTable1(sql);
                    if (dt.Rows.Count > 0)
                    {
                        errorProvider1.SetError(txtCode, "重复的画布编号,请重新填入!");
                        return;
                    }
                }
                if (txtName.Text != canvasName)
                {
                    string sql = "select canvas_code from AT_CANVAS_B where canvas_name='" + txtName.Text + "'";
                    DataTable dt = data.DBQuery.OpenTable1(sql);
                    if (dt.Rows.Count > 0)
                    {
                        errorProvider1.SetError(txtName, "重复的画布名称,请重新填入!");
                        return;
                    }
                }

                if (TxtLocationX.Text == "")
                { 
                    
                }

                //string str = "update AT_CANVAS_B set canvas_code='" + txtCode.Text + "',canvas_name='" + txtName.Text + "',remark='" + txtMemo.Text + "',monitor_type='" + typeIndex + "' where canvas_ID='" + canvasID + "'";
                //data.DBQuery.ExceuteNonQuery(str);


                StringBuilder strBuilder = new StringBuilder();
                strBuilder.Append("update AT_CANVAS_B set canvas_code =@canvasCode,canvas_name =@canvasName,remark =@memo,monitor_type =@monitorType ,is_monitor =@is_monitor,is_stop =@is_stop,monitor_time=@monitor_time,locationX =@locationX,locationY =@locationY,size_width =@size_width,size_height =@size_height where canvas_ID =@canvasID");
                SqlParameter[] parameters = {
                                    new SqlParameter("@canvasID", SqlDbType.NVarChar,50),
                                    new SqlParameter("@canvasCode", SqlDbType.NVarChar,50),
                                    new SqlParameter("@canvasName", SqlDbType.NVarChar,1000),  
                                    new SqlParameter("@monitorType", SqlDbType.Int,8),
                                    new SqlParameter("@memo", SqlDbType.NVarChar,2000),
                                    new SqlParameter("@is_monitor", SqlDbType.Int,8),
                                    new SqlParameter("@is_stop", SqlDbType.Int,8),
                                    new SqlParameter("@monitor_time", SqlDbType.Int,8),
                                    new SqlParameter("@locationX", SqlDbType.Int,8),
                                    new SqlParameter("@locationY", SqlDbType.Int,8),
                                    new SqlParameter("@size_width", SqlDbType.Int,8),
                                    new SqlParameter("@size_height", SqlDbType.Int,8)};
                parameters[0].Value = canvasID;
                parameters[1].Value = txtCode.Text;
                parameters[2].Value = txtName.Text;
                parameters[3].Value = typeIndex;
                parameters[4].Value = txtMemo.Text;
                parameters[5].Value = radiobutton;
                parameters[6].Value = radiobutton1;
                parameters[7].Value = (int)numericUpDown1.Value;

                if (TxtLocationX.Text == "")
                    parameters[8].Value = null;
                else
                    parameters[8].Value = int.Parse(TxtLocationX.Text);
                if (TxtLocationY.Text == "")
                    parameters[9].Value = null;
                else
                    parameters[9].Value = int.Parse(TxtLocationY.Text);
                if (TxtSizeL.Text == "")
                    parameters[10].Value = null;
                else
                    parameters[10].Value = int.Parse(TxtSizeL.Text);
                if (TxtSizeH.Text == "")
                    parameters[11].Value = null;
                else
                    parameters[11].Value = int.Parse(TxtSizeH.Text);

                //parameters[7].Value = int.Parse(TxtLocationX.Text);
                //parameters[8].Value = int.Parse(TxtLocationY.Text);
                //parameters[9].Value = int.Parse(TxtSizeL.Text);
                //parameters[10].Value = int.Parse(TxtSizeH.Text);
                data.DBQuery.ExecuteSql(strBuilder.ToString(), parameters);

            }
            else
            {
                string sql = "select canvas_code from AT_CANVAS_B where canvas_code='" + txtCode.Text + "'";
                DataTable dt = data.DBQuery.OpenTable1(sql);
                if (dt.Rows.Count > 0)
                {
                    errorProvider1.SetError(txtCode, "重复的画布编号,请重新填入!");
                    return;

                }
                string sql1 = "select canvas_code from AT_CANVAS_B where canvas_code='" + txtName.Text + "'";
                DataTable dt1 = data.DBQuery.OpenTable1(sql1);
                if (dt1.Rows.Count > 0)
                {
                    errorProvider1.SetError(txtName, "重复的画布名称,请重新填入!");
                    return;

                }
            }
            DialogResult = DialogResult.OK;
        }

        private void DlgAddCanvas_Load(object sender, EventArgs e)
        {
            LoadcmbType();
            if (!isModified)//新建
            {
                canvasID = Operations.GenerateNewID();
                if (cmbType.Items.Count > 0)
                {
                    cmbType.SelectedIndex = 0;
                }
                if (txtCode.Items.Count > 0)
                {
                    txtCode.SelectedIndex = 0;
                }
            }
            else//修改
            {
                string sql = "select canvas_ID,canvas_code,canvas_name,remark,monitor_type,is_monitor,is_stop,monitor_time,locationX,locationY,size_width,size_height from AT_CANVAS_B where canvas_ID='" + canvasID + "'";
                DataTable dt = data.DBQuery.OpenTable1(sql);
                if (dt.Rows.Count > 0)
                {
                    cmbType.Text = ((Dalssoft.DiagramNet.CanvasType)Convert.ToInt32(dt.Rows[0]["monitor_type"].ToString())).ToString();
                    txtMemo.Text = dt.Rows[0]["remark"].ToString();
                    canvasCode = txtCode.Text = dt.Rows[0]["canvas_code"].ToString();
                    canvasName = txtName.Text = dt.Rows[0]["canvas_name"].ToString();
                    if (Convert.ToString(dt.Rows[0]["is_monitor"]) == "1")
                        isMonitor = true;
                    else
                        isMonitor = false;
                    if (Convert.ToString(dt.Rows[0]["is_stop"]) == "1")
                        isStop = true;
                    else
                        isStop = false;
                    numericUpDown1.Value = Convert.ToInt32(dt.Rows[0]["monitor_time"].ToString().Trim());
                    TxtLocationX.Text = Convert.ToString(dt.Rows[0]["locationX"]);
                    TxtLocationY.Text = Convert.ToString(dt.Rows[0]["locationY"]);
                    TxtSizeL.Text = Convert.ToString(dt.Rows[0]["size_width"]);
                    TxtSizeH.Text = Convert.ToString(dt.Rows[0]["size_height"]);
                    if (isMonitor)
                        radioButton1.Checked = true;
                    else
                        radioButton2.Checked = true;
                    if (isStop)
                        radioButton3.Checked = true;
                    else
                        radioButton4.Checked = true;
                }
            }
        }

        private void LoadcmbType()
        {
            string[] items = Enum.GetNames(typeof(Dalssoft.DiagramNet.CanvasType));
            for (int i = 0; i < items.GetLength(0); i++)
            {
                cmbType.Items.Add(items[i]);
            }
        }

        private void txtCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            canvasCode = txtCode.Text;
            txtName.Text = canvasName = nameCollection[txtCode.SelectedIndex].ToString();
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCode.Items.Clear();
            nameCollection.Clear();
            codeCollection.Clear();
            idCollection.Clear();

            if ((Dalssoft.DiagramNet.CanvasType)Enum.Parse(typeof(Dalssoft.DiagramNet.CanvasType), cmbType.SelectedItem.ToString()) == Dalssoft.DiagramNet.CanvasType.区域监控)
            {
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButton3.Enabled = false;
                radioButton4.Enabled = false;
            }
            else if ((Dalssoft.DiagramNet.CanvasType)Enum.Parse(typeof(Dalssoft.DiagramNet.CanvasType), cmbType.SelectedItem.ToString()) == Dalssoft.DiagramNet.CanvasType.无)
            {
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButton3.Enabled = false;
                radioButton4.Enabled = false;
            }
            //else if ((Dalssoft.DiagramNet.CanvasType)Enum.Parse(typeof(Dalssoft.DiagramNet.CanvasType), cmbType.SelectedItem.ToString()) == Dalssoft.DiagramNet.CanvasType.设备监控)
            //{
            //    radioButton1.Enabled = false;
            //    radioButton2.Enabled = false;
            //    string sql = "select distinct a.equip_key,a.equip_name,b.equip_mc_u_S from EQUIPMENT a,UDA_Equipment b where a.equip_key = b.object_key";
            //    DataTable dt = data.DBQuery.OpenTable1(sql);
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        nameCollection.Add(dt.Rows[i][2].ToString());
            //        codeCollection.Add(dt.Rows[i][1].ToString());
            //        idCollection.Add(dt.Rows[i][0].ToString());
            //        txtCode.Items.Add(dt.Rows[i][1].ToString());//添加item
            //    }
            //}
            else if ((Dalssoft.DiagramNet.CanvasType)Enum.Parse(typeof(Dalssoft.DiagramNet.CanvasType), cmbType.SelectedItem.ToString()) == Dalssoft.DiagramNet.CanvasType.LED看板)
            {
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                radioButton1.Checked = true;
                radioButton3.Enabled = false;
                radioButton4.Enabled = false;
            }
            if (parentID == "0")//二级节点
            {
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                numericUpDown1.Enabled = false;
            }
            else//非二级节点
            {
                TxtLocationX.Enabled = false;
                TxtLocationY.Enabled = false;
                TxtSizeL.Enabled = false;
                TxtSizeH.Enabled = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                radioButton3.Enabled = true;
                radioButton4.Enabled = true;
            }
            else
            {
                radioButton3.Enabled = false;
                radioButton4.Enabled = false;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            //if (radioButton3.Checked == true)
            //{
            //    numericUpDown1.Enabled = true; 
            //}
            //else
            //{
            //    numericUpDown1.Enabled = false;
            //}
        }
    }
}