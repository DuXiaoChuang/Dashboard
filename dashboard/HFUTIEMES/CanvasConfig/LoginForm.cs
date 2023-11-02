using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.IO;
using System.Xml;
using System.Collections;
using System.Net;

namespace HFUTIEMES
{
    public partial class LoginForm : Form
    {
        #region 属性
        public int tt;//1表示可以操作，２表示浏览，３表示既没操作也没浏览
        public string mancode = "";//获取登陆人员编号
        private const string TXTPATH = @"\Login\";
        private const string TXTPOSTFIX = ".txt";
        private const string TXTPATH2 = @"\Update\";
        string content1;
        DateTime edition;//版本号
        DecryptEncrypt DeEn = new DecryptEncrypt();

        //动画窗体调用,关闭时将向上移出屏幕
        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
        const int AW_HOR_POSITIVE = 0x0001;
        const int AW_HOR_NEGATIVE = 0x0002;
        const int AW_VER_POSITIVE = 0x0004;
        const int AW_VER_NEGATIVE = 0x0008;
        const int AW_CENTER = 0x0010;
        const int AW_HIDE = 0x10000;
        const int AW_ACTIVATE = 0x20000;
        const int AW_SLIDE = 0x40000;
        const int AW_BLEND = 0x80000;
        public long time;
        public string h;
        //…………………………………………………………
        #endregion

        #region 事件
        public LoginForm()
        {
            InitializeComponent();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {   
            ////登陆框时钟显示在最前面
            //this.TopMost = true;       
            //动画由小渐大,现在取消
            AnimateWindow(this.Handle, 500, AW_CENTER | AW_ACTIVATE);
            //主界面渐变设置
            this.jianbian.Enabled = true;//让jianbian的timer值有效
            this.Opacity = 0;
            //………………………………………………

            string str = Application.StartupPath;
            string filepath = str + TXTPATH + "login" + TXTPOSTFIX;
            if (System.IO.File.Exists(filepath))
            {
                string content = File.ReadAllText(filepath);
                Nameid.Text = content;
            }
            
            string datasorucePath = str + "\\config.xml";
            if (System.IO.File.Exists(datasorucePath))
            {
                XmlReader rdr = XmlReader.Create(datasorucePath);
                ArrayList al = new ArrayList();
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
                data.data.dataSource = al[5].ToString();
                data.data.dataBase = al[11].ToString();
                data.data.uid = al[7].ToString();
                try
                {
                    //data.data.passWord = DeEn.Decrypto(al[9].ToString());
                    data.data.passWord ="sa123";
                }
                catch (Exception ex)
                {
                    data.data.passWord = "";
                    MessageBox.Show("请重新设定数据库连接！" + ex.ToString());
                }
                data.data.ConnStr = "data source=" + al[5].ToString() + ";database=" + al[11].ToString() + ";user id=" + al[7].ToString() + ";password=" + data.data.passWord + "";
            }
            //string datasorucePath1 = str + "\\fabu.xml";
            //if (System.IO.File.Exists(datasorucePath1))
            //{
            //    XmlReader rdr = XmlReader.Create(datasorucePath1);
            //    ArrayList al = new ArrayList();
            //    while (rdr.Read())
            //    {
            //        if (rdr.Value != "")
            //        {
            //            al.Add(rdr.Value.ToString());
            //        }
            //    }
            //    h = al[2].ToString();
            //    rdr.Close();
            //}
            Password.Focus();
           
        }
        # region ★★判断是否有实例在运行★★
        private bool CheckProcess()
        {
            Process[] myProcesses;
            myProcesses = Process.GetProcessesByName("YSJX");
            if (myProcesses.Length > 1)
            {
                MessageBox.Show("已有一个实例在运行!\r\n请在任务栏关闭单击右键退出系统后重新启动!");
                this.Dispose();
                this.Close();
                return false;
            }
            return true;
        }
        # endregion
        //# region ★★检查版本更新★★
        //private void CheckEdition()
        //{
        //    //try
        //    {
        //        string str = Application.StartupPath;
        //        string filepath = str + TXTPATH2 + "UnitId" + TXTPOSTFIX;
        //        string UnitId = "";//工作站ID
        //        if (System.IO.File.Exists(filepath))
        //        {
        //            //try
        //            //{
        //            //    string content = File.ReadAllText(filepath);
        //            //    UnitId = content;
        //            //}
        //            //catch
        //            //{
        //            //    return;
        //            //}
        //        }
        //        /*****************************************************/
        //        filepath = str + TXTPATH + "edition" + TXTPOSTFIX;
        //        if (System.IO.File.Exists(filepath))
        //        {
        //            try
        //            {
        //                string content = File.ReadAllText(filepath);
        //                edition = Convert.ToDateTime(content);
        //            }
        //            catch
        //            {
        //                return;
        //            }

        //        }
        //        /*****************************************************/
        //        DataTable dt = new DataTable();
        //        string sql = "select Edition from ApplicationTable where Edition >'" + edition.ToString() + "'";
        //        dt = data.DBQuery.OpenTable1(sql);
        //        if (dt.Rows.Count > 0)
        //        {
        //            //this.Close();
        //            string templetFile = Application.StartupPath + "\\北奔程序更新.exe";
        //            System.Diagnostics.Process.Start(templetFile);
        //            this.Dispose();
        //            System.Environment.Exit(System.Environment.ExitCode);
        //        }
        //        /*****************************************************/
        //    }
        //    //catch
        //    //{


        //    //}

        //}
        //# endregion

        private void retreat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void jianbian_Tick(object sender, EventArgs e)
        {
            //让背景由0变到1
            if (this.Opacity < 1)
            {
                this.Opacity = this.Opacity + 0.2;
            }
            else
            {
                this.jianbian.Enabled = false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //关闭时动画
            AnimateWindow(this.Handle, 300, AW_SLIDE | AW_HIDE | AW_VER_NEGATIVE);
        }

        private void label15_Click(object sender, EventArgs e)
        {
            //About dlg = new About();
            //dlg.Show();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            if (this.Nameid.Text == "")
            {
                MessageBox.Show("用户名不能为空", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
            DataTable dt = new DataTable();
            string sql = "select * from PERSONNEL where login_name='" + this.Nameid.Text + "'";
            dt = data.DBQuery.OpenTable1(sql);
            if (dt.Rows.Count > 0)
            {
                SystemLog.UserKey = dt.Rows[0]["user_key"].ToString();
                SystemLog.LoginName = this.Nameid.Text;
                if (SystemLog.LoginName.ToString().Trim() == "sa")/////10.21
                {
                    SystemLog.personalRolesName = "超级用户";
                }
                else
                {
                    SystemLog.personalPowerList = SystemLog.getPowerList(dt.Rows[0]["user_key"].ToString());
                }
                if (!(HFUTIEMES.SystemLog.personalRolesName == "超级用户" || HFUTIEMES.SystemLog.personalPowerList.Contains("G")))
                {
                    MessageBox.Show("您没有操作该功能的权限！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                MessageBox.Show("该用户不存在！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SystemLog.AddLog1("设置可视化数据库信息");
            SetData dlg = new SetData();
            dlg.ShowDialog();
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            if (this.Nameid.Text == "")
            {
                MessageBox.Show("用户名不能为空", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
            if (this.Password.Text == "")
            {
                MessageBox.Show("请输入用户密码！", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
           
            DataTable dt = new DataTable();
            string sql = "select * from PERSONNEL where login_name='" + this.Nameid.Text + "'";
            //string sql = "select * from APP_USER where login_name='" + this.Nameid.Text + "'";
            dt = data.DBQuery.OpenTable1(sql);
            if (dt.Rows.Count > 0)
            {
                string toshow = DeEn.Decrypto(dt.Rows[0]["password"].ToString());//解密
                if (toshow == this.Password.Text)
                {
                    SystemLog.InOrOut = 1;
                    SystemLog.UserCode = dt.Rows[0]["user_code"].ToString().Trim();
                    SystemLog.UserName = dt.Rows[0]["user_name"].ToString();
                    SystemLog.UserKey = dt.Rows[0]["user_key"].ToString();
                    SystemLog.LoginName = this.Nameid.Text;


                    //SystemLog.UserCode = dt.Rows[0]["user_name"].ToString().Trim();
                    //SystemLog.UserName = dt.Rows[0]["first_name"].ToString();
                    //SystemLog.UserKey = dt.Rows[0]["user_key"].ToString();

                    string hostinfo = Dns.GetHostName();
                    IPHostEntry ipEntry = Dns.GetHostEntry(hostinfo);
                    string IpAddress = ipEntry.AddressList[0].ToString();
                    SystemLog.PCname = hostinfo;
                    SystemLog.IP = IpAddress;
                    if (SystemLog.LoginName.ToString().Trim() == "sa")/////10.21
                    {
                        SystemLog.personalRolesName = "超级用户";
                    }
                    else
                    {
                        SystemLog.personalPowerList = SystemLog.getPowerList(dt.Rows[0]["user_key"].ToString());
                    }
                    if (!(HFUTIEMES.SystemLog.personalRolesName == "超级用户" || HFUTIEMES.SystemLog.personalPowerList.Contains("H")))
                    {
                        MessageBox.Show("您没有操作该功能的权限！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    SystemLog.AddLog1("操作可视化");
                    ExportToTxt();
                    //CheckEdition();//检查程序更新
                    if (!CheckProcess()) //判断是否有实例存在
                    {
                        return;
                    }
                    this.Close();
                    SystemLog.AddLog1("登录系统");

                }
                else
                {
                    MessageBox.Show("密码错误,请输入正确的密码！", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                    this.Password.Clear();
                    return;
                }
            }
            else
            {
                MessageBox.Show("该用户名不存在,请重新输入", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                this.Nameid.Clear();
                this.Password.Clear();
                return;
            }
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            stopwatch.Stop();
            time += stopwatch.ElapsedTicks;
            if (time >190)
            {
                this.pictureBox7.Visible = false;
                timer1.Stop();
            }

        }

        private void lnkbaidu_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            lnkbaidu.LinkVisited = true;
            Process.Start(lnkbaidu.Text.Substring(0));

        }
        #endregion

        #region 方法
        private void ExportToTxt()
        {
            string str = Application.StartupPath;
            string filepath = str + TXTPATH + "login" + TXTPOSTFIX;
            FileInfo file = new FileInfo(filepath);
            StreamWriter textFile = null;
            try
            {
                textFile = file.CreateText();
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("系统找不到指定目录下的文件： " + filepath);
                return;
            }
            textFile.Write(Nameid.Text);
            textFile.Close();
        }
        #endregion

        private void label15_Click_1(object sender, EventArgs e)
        {
            if (this.Nameid.Text == "")
            {
                MessageBox.Show("用户名不能为空", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
            DataTable dt = new DataTable();
            string sql = "select * from PERSONNEL where login_name='" + this.Nameid.Text + "'";
            dt = data.DBQuery.OpenTable1(sql);
            if (dt.Rows.Count > 0)
            {
                SystemLog.UserKey = dt.Rows[0]["user_key"].ToString();
                SystemLog.LoginName = this.Nameid.Text;
                if (SystemLog.LoginName.ToString().Trim() == "sa")/////10.21
                {
                    SystemLog.personalRolesName = "超级用户";
                }
                else
                {
                    SystemLog.personalPowerList = SystemLog.getPowerList(dt.Rows[0]["user_key"].ToString());
                }
                if (!(HFUTIEMES.SystemLog.personalRolesName == "超级用户" || HFUTIEMES.SystemLog.personalPowerList.Contains("K")))
                {
                    MessageBox.Show("您没有操作该功能的权限！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                MessageBox.Show("该用户不存在！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            SystemLog.AddLog1("设置可视化发布状态");
            fabustute dlg = new fabustute();
            dlg.h = h;
            dlg.ShowDialog();
        }
    }
}