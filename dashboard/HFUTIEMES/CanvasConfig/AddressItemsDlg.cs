using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Collections;
//using OPCAutomation;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace HFUTIEMES
{
    public partial class AddressItemsDlg : Form
    {

        /// <summary>
        /// OPCServer Object
        /// </summary>
        //OPCServer OPCServer;
        /// 连接状态
        /// </summary>
        bool opc_connected = false;
        public string ItemKey = "";
        public int ok = 0;


        public AddressItemsDlg()
        {
            InitializeComponent();
        }

        private void TagItemsAdd_Load(object sender, EventArgs e)
        {
            //获取本地计算机上的OPCServerName
            try
            {
                //OPCServer = new OPCServer();
                //object serverList = OPCServer.GetOPCServers(Environment.MachineName);

                //foreach (string turn in (Array)serverList)
                //{
                //    cmbServerName.Items.Add(turn);
                //}

                //cmbServerName.SelectedIndex = 0;
                //btnConnServer.Enabled = true;
            }
            catch (Exception err)
            {
                MessageBox.Show("枚举本地OPC服务器出错：" + err.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnConnServer_Click(object sender, EventArgs e)
        {
            try
            {
                //连接OPC服务器
                if (!ConnectRemoteServer("", cmbServerName.Text))
                {
                    return;
                }
                opc_connected = true;//服务器连接状态
                //RecurBrowse(OPCServer.CreateBrowser());//列出服务器中所有节点
            }
            catch (Exception err)
            {
                MessageBox.Show("初始化出错：" + err.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 列出OPC服务器中所有节点
        /// </summary>
        /// <param name="oPCBrowser"></param>
        //private void RecurBrowse(OPCBrowser oPCBrowser)
        //{
        //    //展开分支
        //    oPCBrowser.ShowBranches();
        //    //展开叶子
        //    oPCBrowser.ShowLeafs(true);

        //    //int i = 1;
        //    foreach (object turn in oPCBrowser)
        //    {
        //        if (!turn.ToString().Contains("System") && !turn.ToString().Contains("_Hints") && !turn.ToString().Contains("_DataLogger") && !turn.ToString().Contains("_InternalTags"))
        //            listBox1.Items.Add(turn.ToString());
        //    }
        //}

        /// <summary>
        /// 连接OPC服务器
        /// </summary>
        /// <param name="remoteServerIP">OPCServerIP</param>
        /// <param name="remoteServerName">OPCServer名称</param>
        private bool ConnectRemoteServer(string remoteServerIP, string remoteServerName)
        {
            try
            {
                //OPCServer.Connect(remoteServerName, remoteServerIP);
                //if (OPCServer.ServerState != (int)OPCServerState.OPCRunning)
                //{
                //    //这里你可以根据返回的状态来自定义显示信息，请查看自动化接口API文档
                //    MessageBox.Show(OPCServer.ServerState.ToString());
                //}
            }
            catch (Exception err)
            {
                MessageBox.Show("连接远程服务器出现错误：" + err.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void TagItemsAdd_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!opc_connected)
            {
                return;
            }
            //if (OPCServer != null)
            //{
            //    OPCServer.Disconnect();
            //    OPCServer = null;
            //}
            opc_connected = false;
        }

        private void OKButt_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count == 1)
            {
                ItemKey = listBox1.SelectedItem.ToString();
                ok = 1;
                this.Close();
            }
            else
            {
                MessageBox.Show("未选择任何行！");
            }
        }

        private void CANCELButt_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbServerName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
