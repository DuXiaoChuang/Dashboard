using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml;
using System.Data.Sql;
using System.Data.SqlClient;

namespace HFUTIEMES
{
    public partial class SetData : Form
    {
        #region 属性
        bool isOpenFwq = true;
        bool isOpenSjk = true;
        bool fwqcz = false;
        DecryptEncrypt DeEn = new DecryptEncrypt();
        #endregion

        #region 事件
        public SetData()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)//取消,关闭页面
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)//确定
        {
            data.data.dataSource = fwq.Text;
            data.data.uid = yh.Text;
            data.data.passWord = mm.Text;
            data.data.dataBase = sjk.Text;
            data.data.ConnStr = "data source=" + fwq.Text.ToString() + ";database=" + sjk.Text.ToString() + ";user id=" + yh.Text.ToString() + ";password=" + mm.Text.ToString() + "";

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("   ");
            settings.ConformanceLevel = ConformanceLevel.Document;
            settings.CloseOutput = false;
            settings.OmitXmlDeclaration = false;
            string outfilename = Application.StartupPath + "\\config.xml";
            XmlWriter writer = XmlWriter.Create(outfilename, settings);
            writer.WriteStartDocument();
            writer.WriteComment("数据库设置——合肥工业大学工业工程与工程管理实验室");
            writer.WriteStartElement("data");
            writer.WriteElementString("dataSource", fwq.Text.ToString());
            writer.WriteElementString("uid", yh.Text.ToString());
            writer.WriteElementString("passWord", DeEn.Encrypto(mm.Text.ToString()));
            writer.WriteElementString("dataBase", sjk.Text.ToString());
            writer.WriteEndElement();
            writer.Flush();
            writer.Close();
            this.Close();
        }

        private void fwq_DropDown(object sender, EventArgs e)//利用.net现成的类实现检测局域网中的SQL服务器
        {
            ArrayList al = new ArrayList();
            if (isOpenFwq)
            {
                SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
                System.Data.DataTable table = instance.GetDataSources();
                al = GetServerList(table);
                fwq.Items.Clear();
                for (int i = 0; i < al.Count; i++)
                {
                    fwq.Items.Add(al[i].ToString());
                }
                isOpenFwq = false;
            }
            for (int i = 0; i < al.Count; i++)
            {
                if (al[i].ToString().ToLower() == fwq.Text.ToString().ToLower())
                {
                    fwqcz = true;
                    break;
                }
            }
        }
        
        private void fwq_SelectedIndexChanged(object sender, EventArgs e)//联通服务器
        {
            isOpenSjk = true;
            fwqcz = true;
        }

        private void sjk_DropDown(object sender, EventArgs e)
        {
            if (fwqcz)
            {
                ArrayList al = new ArrayList();
                if (isOpenSjk)
                {
                    sjk.Items.Clear();
                    al = GetDbList(fwq.Text.ToString(), yh.Text.ToString(), mm.Text.ToString());
                    for (int i = 0; i < al.Count; i++)
                    {
                        sjk.Items.Add(al[i].ToString());
                    }
                    isOpenSjk = false;
                }
            }
            else
            {
                MessageBox.Show("组件没有找到该服务器，请检查网络或重新搜索服务器！");
            }
        }

        private void fwq_TextChanged(object sender, EventArgs e)
        {
            isOpenSjk = true;
            fwqcz = true;
        }

        private void fwq_TextUpdate(object sender, EventArgs e)
        {
            isOpenSjk = true;
            fwqcz = true;
        }

        private void SetDate_Load(object sender, EventArgs e)
        {
            fwq.Text = data.data.dataSource.ToString();
            yh.Text = data.data.uid.ToString();
            mm.Text = data.data.passWord.ToString();
            sjk.Text = data.data.dataBase.ToString();
            fwq_SelectedIndexChanged(sender,e);
        }

        private void fwq_DropDown_1(object sender, EventArgs e)
        {
            ArrayList al = new ArrayList();
            if (isOpenFwq)
            {
                ////////利用.net现成的类实现检测局域网中的SQL服务器
                SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
                System.Data.DataTable table = instance.GetDataSources();
                al = GetServerList(table);
                fwq.Items.Clear();
                for (int i = 0; i < al.Count; i++)
                {
                    fwq.Items.Add(al[i].ToString());
                }
                isOpenFwq = false;
            }
            for (int i = 0; i < al.Count; i++)
            {
                if (al[i].ToString().ToLower() == fwq.Text.ToString().ToLower())
                {
                    fwqcz = true;
                    break;
                }
            }
        }
        #endregion

        #region 方法
        private ArrayList GetServerList(System.Data.DataTable table)//获得数据库服务器
        {
            ArrayList al = new ArrayList();
            foreach (System.Data.DataRow row in table.Rows)
            {
                al.Add(row["ServerName"].ToString());
            }
            return al;
        }

        private ArrayList GetDbList(string strServerName, string strUserName, string strPwd)//获得数据库
        {
            ArrayList al = new ArrayList();
            SqlConnection conn = new SqlConnection(string.Format("Data Source={0};Initial Catalog=master;User ID={1};PWD={2}", strServerName, strUserName, strPwd));
            DataTable dt = new DataTable();
            SqlDataAdapter Adapter = new SqlDataAdapter("select Name from Master..Sysdatabases order by Name", conn);
            try
            {
                Adapter.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {

                    al.Add(row["Name"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库暂时连接不上，请检查网络或重试！" + ex.ToString());
            }
            finally
            {
                conn.Close();
            }

            return al;
        }

        public ArrayList GetTBList(string strServerName, string strDataBase, string strUserName, string strPwd) //获得数据库的表
        {
            string ServerName = strServerName;
            string DataBase = strDataBase;
            string UserName = strUserName;
            string Password = strPwd;

            ArrayList alDbs = new ArrayList();
            //SQLDMO.Application sqlApp = new SQLDMO.ApplicationClass();
            //SQLDMO.SQLServer svr = new SQLDMO.SQLServerClass();
            //try
            //{
            //    svr.Connect(ServerName, UserName, Password);
            //    foreach (SQLDMO.Database db in svr.Databases)
            //    {
            //        if (db.Name != null)
            //            alDbs.Add(db.Name);
            //    }
            //}
            //catch (Exception e)
            //{
            //    throw (new Exception("连接数据库出错：" + e.Message));
            //}
            //finally
            //{
            //    svr.DisConnect();
            //    sqlApp.Quit();
            //}
            return alDbs;
        }
        #endregion
    }

}