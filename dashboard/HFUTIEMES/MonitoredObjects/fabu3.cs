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
    public partial class fabu3 : Form
    {
        public int type = 0;//画布类型
        public int StartRow = 0;
        public int StartRow1 = 0;
        public string txt = @"/Log/可视化监控/";
        public string postfix = @".txt";
        public string canvasID, canvasCode, canvasName, parent = "";
        public string alerttype = "";
        string path = @"/画布";
        MonitoredInfoList properties = new MonitoredInfoList();
        public Thread thread;
        int topPointY = SystemInformation.WorkingArea.Bottom - 21;
        private Document currentDocument = new Document();
        const int alert = 1;
        const int working = 2;
        const int netBreak = 3;
        const int allworking = 4;
        const int none = 0;
        ToolTip tip = new ToolTip();
        Dalssoft.DiagramNet.BaseElement selectElement = null;
        string currentCanvasCode = "";
        private ArrayList Arry1 = new ArrayList();
        private ArrayList Arry2 = new ArrayList();
        private ArrayList Arry3 = new ArrayList();
        private ArrayList Arry4 = new ArrayList();
        private int index = 0;//用于记录当前的画布序号
        private int a;//用于记录报警看板的画布序号
        public System.Threading.Timer timer;
        public string monitorCanvasId;//当前监控的画布ID
        public int monitorCanvasType;//画布的监控类型
        public Size canvasSize;
        public Point canvasPoint;
        public ArrayList info = new ArrayList();
        public ArrayList lastinfo = new ArrayList();
        string mediaPath = Application.StartupPath + @"/Alert/AlertMediaPath.ini";
        string mediaFileName = Application.StartupPath + @"/Alert/alert.wav";
        public ClassMediaPlay player = new ClassMediaPlay();
        private Font font = new Font(FontFamily.GenericSansSerif, 15);
        public fabu3()
        {
            InitializeComponent();
        }

        private void fabu3_Load(object sender, EventArgs e)
        {
            this.Size = canvasSize;
            this.Location = canvasPoint;
            loadCanvas();
            designer1.Type = TypeOfDesigner.监控;
            Operations.DeleteFolder(path);
           
            if ((Dalssoft.DiagramNet.CanvasType)monitorCanvasType == Dalssoft.DiagramNet.CanvasType.LED看板)
            {
                timer1.Enabled = true;
                timerMonitor.Enabled = true;
                timerMonitor.Start();
            }
            else
            {
                if (Arry3[0].ToString() == "" || Arry3[0].ToString() == null)
                    return;
                else
                {
                    this.designer1.Open(Arry3[0].ToString());
                }
            }
            StartAlertTimer(designer1.Document);
            tip.ToolTipIcon = ToolTipIcon.None;
            tip.BackColor = Color.GhostWhite;
        }
       
        public void StartAlertTimer(Document document)
        {
            currentDocument = document;
            //ConnectOPCServer(document);
            timer = new System.Threading.Timer(new TimerCallback(GetAlertInfomation), document, 0, 3000);
            timer.Change(0, 10000);
        }

        public void GetAlertInfomation(object sender)
        {

            try
            {
                Document document = (Document)sender;
                GetRealTimeInfo(document);
                //ShowRealTimeInfo();
               
            }
            catch (Exception ex)
            {
                Error.AddText(ex.ToString());
                //return;
            }

        }
        public void GetRealTimeInfo(object obj)
        {

            try
            {
                Document document = (Document)obj;
                info = new ArrayList();
                for (int i = 0; i < document.Elements.Count; i++)
                {
                    BaseElement el = document.Elements[i];
                    el.IsInvalidated = false;
                    MonitoredType elementMonitoredType = el.监控类型;
                    if (el.MoniteredObjectID != string.Empty)
                    {
                        #region 设备状态
                        if (elementMonitoredType == MonitoredType.设备状态)
                        {
                            EquipState els = (EquipState)el;
                            string sql = "select EquipId,AddressContent from Equip_State_P where EquipId='" + els.MoniteredObjectID + "' and StateDescription='运行状态'";
                            DataTable dt = DbHelperSQL.OpenTable(sql);
                            if (dt.Rows.Count > 0)
                            {
                                if (dt.Rows[0][1].ToString() == "正常工作")
                                    els.statue = 1;
                                else if (dt.Rows[0][1].ToString() == "故障")
                                    els.statue = 2;
                                else if (dt.Rows[0][1].ToString() == "急停")
                                    els.statue = 3;
                                else
                                    els.statue = 4;
                            }
                            else
                                els.statue = 0;
                            string sql1 = "select ProductBornCode from Product_Route_P where EquipId='" + els.MoniteredObjectID + "' order by Starttime";
                            var dt1 = DbHelperSQL.OpenTable(sql1);
                            if (dt1.Rows.Count > 0)
                                els.productcode = dt1.Rows[0][0].ToString();
                            else
                                els.productcode = "";
                        }
                        #endregion
                        #region 刀具状态
                        if (elementMonitoredType == MonitoredType.刀具状态)
                        {
                            ToolState els = (ToolState)el;
                            DataTable dt = new DataTable();
                            DataTable dt1 = new DataTable();
                            dt = DbHelperSQL.OpenTable("select ToolCode,ToolResidualLife,lifeWarning from Tool_State_P where EquipId='" + els.MoniteredObjectID + "'and ToolResidualLife< 0 ");
                            if (dt.Rows.Count > 0)
                            {
                                els.statue = 0;
                            }
                            else
                            {
                                int k=0;
                                dt1 = DbHelperSQL.OpenTable("select ToolCode,ToolResidualLife,lifeWarning from Tool_State_P where EquipId='" + els.MoniteredObjectID + "'");
                                for (int j = 0; i < dt1.Rows.Count; j++)
                                {
                                    if (System.Convert.ToInt16(dt1.Rows[j][1].ToString()) < System.Convert.ToInt16(dt.Rows[j][2].ToString()))
                                    {
                                        k++;
                                    }
                                }
                                if(k >0)    
                                    els.statue = 1;
                                else 
                                    els.statue =2;
                            }
                        }
                        #endregion
                    }
                }
                designer1.Invalidate();
            }
            catch (Exception ex)
            {
                Error.AddText(ex.ToString());
            }

        }
        /// <summary>
        /// 返回对比结果
        /// </summary>
        /// <param name="info"></param>
        /// <param name="lastinfo"></param>
        /// <returns></returns>
        public bool CheckContent(ArrayList info, ArrayList lastinfo)
        {
            bool result = false;
            int zck = 0;
            int zck1 = 0;
            if (alerttype != "" || alerttype != null)
            {
                for (int i = 0; i < info.Count; i++)
                {
                    if (info[i].ToString() == alerttype)
                        zck++;
                }
                for (int i = 0; i < lastinfo.Count; i++)
                {
                    if (lastinfo[i].ToString() == alerttype)
                        zck1++;
                }
                if (zck > zck1)
                {
                    result = true;
                }
            }
            return result;
        }
        public void ShowRealTimeInfo()
        {
            bool check = CheckContent(info, lastinfo);
            if (check && designer1.Document.是否有报警音乐)
            {
                mediaFileName = designer1.Document.报警音乐;
                if (!mediaFileName.Contains(":"))
                {
                    mediaFileName = Application.StartupPath + @"\Alert\" + mediaFileName;
                }
                player.fileName = mediaFileName;
                player.PlayMedia();
                lastinfo = info;
            }
            else
            {
                player.Stop();
                lastinfo = info;

            }
        }
        private void loadCanvas()
        {
            if ((Dalssoft.DiagramNet.CanvasType)monitorCanvasType == Dalssoft.DiagramNet.CanvasType.LED看板)
            {
                string sql = "select canvas_code,monitor_time,is_stop from AT_CANVAS_B where [parent_canvas_ID] = '" + monitorCanvasId + "' and is_monitor='1'";
                DataTable dt = data.DBQuery.OpenTable1(sql);
                if (dt.Rows.Count > 0)
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Arry1.Add(dt.Rows[i]["canvas_code"].ToString());
                        Arry2.Add(dt.Rows[i]["monitor_time"].ToString());
                        Arry3.Add(Operations.ShowObjectsFileName(dt.Rows[i]["canvas_code"].ToString()));
                        Arry4.Add(dt.Rows[i]["is_stop"].ToString());
                    }
            }

            else
            {
                string sql = "select canvas_code,monitor_time from AT_CANVAS_B where [canvas_ID] = '" + monitorCanvasId + "'";
                DataTable dt = data.DBQuery.OpenTable1(sql);
                if (dt.Rows.Count > 0)
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Arry1.Add(dt.Rows[i]["canvas_code"].ToString());
                        Arry2.Add(dt.Rows[i]["monitor_time"].ToString());
                        Arry3.Add(Operations.ShowObjectsFileName(dt.Rows[i]["canvas_code"].ToString()));
                    }
            }
        }
       
        private void designer1_Click(object sender, EventArgs e)
        {

           
                try
                {
                    designer1_Click();
                }
                catch (Exception ex)
                {
                    Error.AddText(ex.ToString());
                    //return;
                }
           

        }

        private void designer1_Click()
        {
            if (designer1.Document.SelectedElements.Count != 1) return;
            Dalssoft.DiagramNet.BaseElement el = designer1.Document.SelectedElements[0];
            if (el.监控类型 == MonitoredType.区域 && el.MoniteredObjectID != "")
            {
                string elementCode = el.MoniteredObjectCode;
                currentCanvasCode = elementCode;
                string elementID = el.MoniteredObjectID;
                timer.Dispose();
                Operations.getLocalCanvas(elementID, designer1);
                if (designer1.Document.Elements.Count > 0)
                {
                    StartAlertTimer(designer1.Document);
                }
            }
            if (el.监控类型 == MonitoredType.设备状态)
            {
                EquipmentMonitoredDlg dlg = new EquipmentMonitoredDlg();
                dlg.ShowInTaskbar = false;
                dlg.MoniteredObjectID = el.MoniteredObjectID;
                dlg.ShowDialog();
            }
            if (el.监控类型 == MonitoredType.统计数量)
            {
                OnlineNumber els = (OnlineNumber)el;
                OutQueMonitor dlg = new OutQueMonitor();
                dlg.type = els.计数类型.ToString();
                dlg.pline = els.MoniteredObjectID;
                dlg.ShowInTaskbar = false;
                dlg.ShowDialog();
            }
            if (el.监控类型 == MonitoredType.设备报警信息表)
            {
               
                EquipAlarmExcel els = (EquipAlarmExcel)el;
                EquipStateMonitoredDlg dlg = new EquipStateMonitoredDlg();
                dlg.A = els.MoniteredObjectID;
                dlg.B = els.区域.ToString();
                dlg.ShowInTaskbar = false;
                dlg.ShowDialog();
            }
            if (el.监控类型 == MonitoredType.刀具状态信息表)
            {
                ToolStateMonitoredDlg dlg = new ToolStateMonitoredDlg();
                dlg.ShowInTaskbar = false;
                dlg.ShowDialog();
            }
            if (el.监控类型 == MonitoredType.产品跟踪表)
            {
                ProductMonitoredDlg dlg = new ProductMonitoredDlg();
                dlg.ShowInTaskbar = false;
                dlg.ShowDialog();
            }
            if (el.监控类型 == MonitoredType.刀具状态)
            {
                ToolMonitoredDlg dlg = new ToolMonitoredDlg();
                dlg.ShowInTaskbar = false;
                dlg.MoniteredObjectCode = el.MoniteredObjectCode ;
                dlg.ShowDialog();
            }
        }
        public void subscription_DataChange(object subscriptionHandle, object requestHandle, ItemValueResult[] values)
        {
            foreach (ItemValueResult item in values)
            {
                for (int i = 0; i < currentDocument.Elements.Count; i++)
                {
                    BaseElement el = currentDocument.Elements[i];
                    if (el.MoniteredObjectID != string.Empty)
                    {
                        if (item.ItemName.ToString() == el.MoniteredObjectID)
                        {
                            if (item.Value.ToString() == "0")
                            {
                                el.State = (ObjectState)none;
                            }
                            else
                            {
                                el.State = (ObjectState)alert;
                            }
                        }
                    }
                }
            }
        }

        private void timerMonitor_Tick(object sender, EventArgs e)
        {
            try
            {
                CheckConn();
                this.designer1.Open(Arry3[index].ToString());
                GetAlertInfomation(designer1.Document);
                if (Arry4[index].ToString() == "1")
                {
                    a = index;
                }

                timerMonitor.Interval = System.Convert.ToInt32(Arry2[index].ToString().Trim()) * 1000;
                index = index + 1;
                if (index > Arry1.Count - 1)
                {
                    index = 0;
                }
            }
            catch (Exception ex)
            {
               
            }
        }
        private void fabu3_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer.Dispose();
            timer1.Enabled = false;
            timerMonitor.Enabled = false;
            player.Stop();
        }

        private void fabu3_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Dispose();
            timer1.Enabled = false;
            timerMonitor.Enabled = false;
            player.Stop();
        }
        private void CheckConn()
        {
            try
            {
                string str = Application.StartupPath;
                string datasorucePath = str + "\\config.xml";
                ArrayList al = new ArrayList();
                if (System.IO.File.Exists(datasorucePath))
                {
                    XmlReader rdr = XmlReader.Create(datasorucePath);
                    while (rdr.Read())
                    {
                        if (rdr.Value != "")
                        {
                            al.Add(rdr.Value.ToString());
                        }
                    }
                    rdr.Close();
                    int count = al.Count;
                    for (int m = 0; m < al.Count; m++)
                    {
                        string tt = al[m].ToString();
                    }
                }
                SqlConnection conn = new SqlConnection("server=" + al[5].ToString() + ";database=" + al[11].ToString() + ";uid=sa;pwd=sa");
                conn.Open();
                if (ConnectionState.Open == conn.State)
                {
                    return;
                }
                else
                {
                    data.data.ConnStr = "data source=" + al[5].ToString() + ";database=" + al[11].ToString() + ";user id=sa;password=sa";
                }
            }
            catch (Exception ex)
            {
                Error.AddText(ex.ToString());
            }

        }
    }
}
