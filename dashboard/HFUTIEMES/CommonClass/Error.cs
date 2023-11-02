using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace HFUTIEMES
{
    public class Error
    {
        public static string TXTPATH = @"/Log/";
        public static string TXTPOSTFIX = @".txt";
        /// <summary>
        /// 记录错误信息文本
        /// </summary>
        /// <param name="UnitID">工作站ID</param>
        /// <param name="Content">内容</param>
        public  static void AddText(string Content)
        {
            string str = Application.StartupPath;
            DirectoryInfo di;
            if (TXTPATH.Contains(str)|TXTPATH.Contains(":"))
            { 
                di = new DirectoryInfo(TXTPATH);
            }
            else
            {
                di = new DirectoryInfo(str + TXTPATH);
            }
            if (!di.Exists)
            {
                di.Create();
            }
            string filepath = di.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day .ToString() + TXTPOSTFIX;
            if (!File.Exists(filepath))//判断文件是否存在
            {
                System.IO.FileStream fs1 = new System.IO.FileStream(filepath, FileMode.Create, FileAccess.Write);//创建写入文件
                fs1.Close();
            }
            if (File.Exists(filepath))//如果文件存在
            {
                FileInfo file = new FileInfo(filepath);
                StreamWriter textFile = null;
                try
                {
                    textFile = file.AppendText();
                }
                catch
                {
                    System.Windows.Forms.MessageBox.Show("系统找不到指定目录下的文件：" + filepath);
                    return;
                }

                textFile.WriteLine(Content);
                textFile.WriteLine(DateTime.Now.ToString());
                textFile.WriteLine("*************************************************");
                textFile.Close();
            }
        }
    }
}
