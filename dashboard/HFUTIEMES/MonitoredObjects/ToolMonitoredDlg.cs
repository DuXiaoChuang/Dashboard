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



namespace HFUTIEMES
{
    public partial class ToolMonitoredDlg : Form
    {
        public string MoniteredObjectCode = "";//画布编号设为设备编号
        public int type = 0;//画布类型
        public string txt = @"/Log/可视化监控/";
        public string postfix = @".txt";
        public string canvasID = "";
        public Thread thread;
        int topPointY = SystemInformation.WorkingArea.Bottom - 21;
        private Document currentDocument = new Document();
        const int alert = 1;
        const int working = 2;
        const int none = 0;
        public string text = "";
        public System.Threading.Timer timer;
        CommentBoxElement messageElement = new CommentBoxElement();

        #region OPC
        /// <summary>
        /// 数据存取服务器
        /// </summary>
        private Opc.Da.Server m_server = null;

        /// <summary>
        /// 组对象（订阅者）
        /// </summary>
        private Opc.Da.Subscription subscription = null;

        /// <summary>
        /// 组（订阅者）状态，相当于OPC规范中组的参数
        /// </summary>
        private Opc.Da.SubscriptionState state = null;

        /// <summary>
        /// 枚举基于COM服务器的接口，用来搜索所有的此类服务器
        /// </summary>
        private Opc.IDiscovery m_discovery = new OpcCom.ServerEnumerator();
        private bool connectedOPC = false;
        #endregion

        public ToolMonitoredDlg()
        {
            InitializeComponent();
        }


        private void Equipment_Load(object sender, EventArgs e)
        {
            canvasID = loadCanvasCode(MoniteredObjectCode);
            if (canvasID != "")
            {
                designer1.Type = TypeOfDesigner.监控;
                Operations.ShowObjectsByCode(MoniteredObjectCode, designer1);
                designer1.Invalidate();
                StartAlertTimer(designer1.Document);
            }
            loadGridView1();
            Operations.AutoSizeDataGridView(dataGridView1);
        }
        private string loadCanvasCode(string equipcode)
        {
            string can = "";
            DataTable dt = DbHelperSQL.OpenTable("select canvas_ID from AT_CANVAS_B where canvas_code='" + equipcode + "'");
            if (dt.Rows.Count > 0)
                can = dt.Rows[0][0].ToString();
            return can;
        }
        private void EquipState(object sender)
        {
            for (int i = 0; i < currentDocument.Elements.Count; i++)
            {
                BaseElement el = currentDocument.Elements[i];
                el.IsInvalidated = false;
                MonitoredType elementMonitoredType = el.监控类型;
                if (el.MoniteredObjectID != string.Empty)
                {
                    if (elementMonitoredType == MonitoredType.刀槽)
                    {
                        Groove els = (Groove)el;
                        string sql = string.Format("select ToolCode,ToolResidualLife,lifeWarning from Tool_State_P where EquipCode='{0}' and AddressContent='{1}'", els.MoniteredObjectCode, els.groove);
                        //上边这一句和下一句是一个意思。
                        //string sql = string.Format("select ToolCode,ToolResidualLife,lifeWarning from Tool_State_P where EquipCode='"+els.MoniteredObjectCode+"' and AddressContent='"+els.groove+"'");
                        var dt = DbHelperSQL.OpenTable(sql);
                        if (dt.Rows.Count > 0)
                        {
                            els.toolcode = dt.Rows[0][0].ToString();
                            if (dt.Rows[0][1].ToString() != "" && dt.Rows[0][2].ToString() != "")
                            {
                                if (System.Convert.ToInt16(dt.Rows[0][1].ToString()) < 0)
                                   els.backcolor  = Color.Red;
                                else if (System.Convert.ToInt16(dt.Rows[0][1].ToString()) < System.Convert.ToInt16(dt.Rows[0][2].ToString()))
                                    els.backcolor = Color.Orange;
                                else
                                    els.backcolor = Color.Green;
                            }
                            else
                                els.backcolor = Color.Transparent ;
                        }
                        else
                        {
                            els.toolcode = "";
                            els.backcolor = Color.Transparent;
                        }
                    }
                }
            }
        }

        public void StartAlertTimer(Document document)
        {
            currentDocument = document;
            timer = new System.Threading.Timer(new TimerCallback(EquipState), document, 0, 10000);
        }

        public void GetAlertInfomation(object sender)
        {
            designer1.Invalidate();
        }
        private void loadGridView1()
        {
            ArrayList a =new ArrayList ();
            string sql = "select ToolCode,ToolManufacturer,ToolResidualLife,lifeWarning,SettingLife,ToolLength,ToolWearLength,ToolRadius,ToolWearRadius,ToolStarttime from Tool_State_P where EquipCode='" + MoniteredObjectCode + "'";
            DataTable dt0 = new DataTable();
            dt0 = DbHelperSQL.OpenTable(sql);
            if (dt0.Rows.Count > 0)
            {
                for (int i = 0; i < dt0.Rows.Count; i++)
                {
                    object[] items = new object[10];
                    items[0] = dt0.Rows[i]["ToolCode"].ToString();
                    items[1] = dt0.Rows[i]["ToolManufacturer"].ToString();
                    items[2] = dt0.Rows[i]["ToolResidualLife"].ToString();
                    items[3] = dt0.Rows[i]["lifeWarning"].ToString();
                    items[4] = dt0.Rows[i]["SettingLife"].ToString();
                    items[5] = dt0.Rows[i]["ToolLength"].ToString();
                    items[6] = dt0.Rows[i]["ToolWearLength"].ToString();
                    items[7] = dt0.Rows[i]["ToolRadius"].ToString();
                    items[8] = dt0.Rows[i]["ToolWearRadius"].ToString();
                    items[9] = dt0.Rows[i]["ToolStarttime"].ToString();
                    dataGridView1.Rows.Add(items);
                    if (dt0.Rows[i]["ToolResidualLife"].ToString() != "" && dt0.Rows[i]["lifeWarning"].ToString() != "")
                    {
                        if (System.Convert.ToInt16(dt0.Rows[i]["ToolResidualLife"].ToString()) < 0)
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        else if (System.Convert.ToInt16(dt0.Rows[i]["ToolResidualLife"].ToString()) < System.Convert.ToInt16(dt0.Rows[i]["lifeWarning"].ToString()))
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                        else
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                    }
                }
                dataGridView1.ClearSelection();
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
