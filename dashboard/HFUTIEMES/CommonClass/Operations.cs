using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using Dalssoft.DiagramNet;

namespace HFUTIEMES
{
    class Operations
    {
        public static string DBCToSBC(string input)
        {
             char[] cc = input.ToCharArray();
             for (int i = 0; i < cc.Length; i++)
             {
                 if (cc[i] == 12288)
                 {
                     // 表示空格   
                     cc[i] = (char)32;
                     continue;
                 }
                 if (cc[i] > 65280 && cc[i] < 65375)
                 {
                     cc[i] = (char)(cc[i] - 65248);
                 }
             }
            return new string(cc);
        }   

        /// <summary>
        /// 得到本节点的名称
        /// </summary>
        /// <returns>返回本节点的名称</returns>
        protected static String GetActivityNamePrefix()
        {
            return "Canvas";
        }

        /// <summary>
        /// 生成新的节点名称（如果相同的节点，自动累加并将累加值加在节点名称后）
        /// </summary>
        /// <returns>返回新节点的名称</returns>
        public static  String GenerateNewID()
        {
            //存放判断名字是否存在
            bool nameExist;
            //存放新名字
            String newName;
            int i = 1;
            do
            {
                newName = GetActivityNamePrefix() + i.ToString();
                string sql = "select canvas_ID from AT_CANVAS_B where canvas_ID='" + newName + "'";
                DataTable dt = data.DBQuery.OpenTable1(sql);
                //如果有相同名字的节点
                if (dt.Rows.Count>0)
                {
                    nameExist = true;
                }
                else
                {
                    nameExist = false;
                }
                i++;
            } while (nameExist);
            return newName;
        }
       
        public static void LoadCanvasTree(TreeView treeView1,int type)
        {            
            treeView1.BeginUpdate();
            TreeNode rootNode = new TreeNode("画布导航", 0,0);
            rootNode.Tag = "0";
            treeView1.Nodes.Add(rootNode);
            LoadNodes(rootNode,type);
            rootNode.Expand();
            treeView1.EndUpdate();

        }

        public static void LoadCanvasTree(TreeView treeView1)
        {
            treeView1.BeginUpdate();
            TreeNode rootNode = new TreeNode("画布导航", 0, 0);
            rootNode.Tag = "0";
            treeView1.Nodes.Add(rootNode);
            LoadNodes(rootNode);
            rootNode.Expand();
            treeView1.EndUpdate();
        }

        public static bool ShowObjectsByCode(string canvasID, Dalssoft.DiagramNet.Designer designer1)
        {
            try
            {
                designer1.CreatNewDocument();
                byte[] content = null;
                string path = Application.StartupPath + "\\画布";
                string sql = "select canvas_content from AT_CANVAS_B where canvas_code='" + canvasID + "'";
                DataTable dt = data.DBQuery.OpenTable1(sql);
                if (dt.Rows.Count > 0)
                {
                    string fileName = path + "\\" + canvasID;

                    if (dt.Rows[0]["canvas_content"] != DBNull.Value)
                    {
                        content = (byte[])dt.Rows[0]["canvas_content"];
                        CreatPath(path);
                        if (File.Exists(fileName) == true)
                        {

                            File.Delete(fileName);

                        }

                        FileStream fs = new FileStream(fileName, FileMode.CreateNew);
                        fs.Write(content, 0, System.Convert.ToInt32(content.Length));
                        fs.Seek(0, SeekOrigin.Begin);
                        fs.Close();

                        designer1.Open(fileName);
                    }
                }
                designer1.Refresh();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool ShowObjects(string canvasID, Dalssoft.DiagramNet.Designer designer1)
        {
            try
            {
                designer1.CreatNewDocument();
                byte[] content = null;
                string path = Application.StartupPath + "\\画布";
                string sql = "select canvas_content from AT_CANVAS_B where canvas_ID='" + canvasID + "'";
                DataTable dt = data.DBQuery.OpenTable1(sql);
                if (dt.Rows.Count > 0)
                {
                    string fileName = path + "\\" + canvasID;

                    if (dt.Rows[0]["canvas_content"] != DBNull.Value)
                    {
                        content = (byte[])dt.Rows[0]["canvas_content"];
                        CreatPath(path);
                        if (File.Exists(fileName) == true)
                        {

                            File.Delete(fileName);

                        }

                        FileStream fs = new FileStream(fileName, FileMode.CreateNew);
                        fs.Write(content, 0, System.Convert.ToInt32(content.Length));
                        fs.Seek(0, SeekOrigin.Begin);
                        fs.Close();

                        designer1.Open(fileName);
                    }
                }
                designer1.Refresh();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 下载所有非LED画布
        /// </summary>
        public static void getAllCanvas()
        {
            try
            {
                byte[] content = null;
                string path = Application.StartupPath + "\\画布";
                string sql = "select [canvas_ID],canvas_content from AT_CANVAS_B where [monitor_type] !='2'";
                DataTable dt = data.DBQuery.OpenTable1(sql);
                for (int i = 0; i < dt.Rows.Count;i++ )
                {
                    string fileName = path + "\\" + dt.Rows[i]["canvas_ID"].ToString();

                    if (dt.Rows[i]["canvas_content"] != DBNull.Value)
                    {
                        content = (byte[])dt.Rows[i]["canvas_content"];
                        CreatPath(path);
                        if (File.Exists(fileName) == true)
                        {
                            File.Delete(fileName);
                        }

                        FileStream fs = new FileStream(fileName, FileMode.CreateNew);
                        fs.Write(content, 0, System.Convert.ToInt32(content.Length));
                        fs.Seek(0, SeekOrigin.Begin);
                        fs.Close();
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// 从本地获取画布文件
        /// </summary>
        /// <param name="canvas_ID"></param>
        public static void getLocalCanvas(string canvas_ID, Dalssoft.DiagramNet.Designer designer1)
        {
            try
            {
                designer1.CreatNewDocument();
                string path = Application.StartupPath + "\\画布";
                string fileName = path + "\\" + canvas_ID;
                if (File.Exists(fileName) == true)
                {
                    designer1.Open(fileName);
                }
                else
                {
                    byte[] content = null;
                    string sql = "select canvas_content from AT_CANVAS_B where canvas_ID='" + canvas_ID + "'";
                    DataTable dt = data.DBQuery.OpenTable1(sql);
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["canvas_content"] != DBNull.Value)
                        {
                            content = (byte[])dt.Rows[0]["canvas_content"];
                            CreatPath(path);
                            FileStream fs = new FileStream(fileName, FileMode.CreateNew);
                            fs.Write(content, 0, System.Convert.ToInt32(content.Length));
                            fs.Seek(0, SeekOrigin.Begin);
                            fs.Close();

                            designer1.Open(fileName);
                        }
                    }
                }

            }
            catch { }
        }

        public static bool ShowObjectsByCode2(string canvasID, Dalssoft.DiagramNet.Designer designer1)
        {
            try
            {
                designer1.CreatNewDocument();
                byte[] content = null;
                string path = Application.StartupPath + "\\画布";
                string sql = "select canvas_content from AT_CANVAS_B where canvas_code='" + canvasID + "'";
                DataTable dt = data.DBQuery.OpenTable1(sql);
                if (dt.Rows.Count > 0)
                {
                    string fileName = path + "\\" + canvasID;

                    if (dt.Rows[0]["canvas_content"] != DBNull.Value)
                    {
                        content = (byte[])dt.Rows[0]["canvas_content"];
                        CreatPath(path);
                        if (File.Exists(fileName) == true)
                        {

                            File.Delete(fileName);

                        }

                        FileStream fs = new FileStream(fileName, FileMode.CreateNew);
                        fs.Write(content, 0, System.Convert.ToInt32(content.Length));
                        fs.Seek(0, SeekOrigin.Begin);
                        fs.Close();

                        designer1.Open(fileName);
                    }
                }
                designer1.Refresh();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string ShowObjectsFileName(string canvasID)
        {
            byte[] content = null;
            string path = Application.StartupPath + "\\画布";
            string sql = "select canvas_content from AT_CANVAS_B where canvas_code='" + canvasID + "'";
            DataTable dt = data.DBQuery.OpenTable1(sql);
            if (dt.Rows.Count > 0)
            {
                string fileName = path + "\\" + canvasID;

                if (dt.Rows[0]["canvas_content"] != DBNull.Value)
                {
                    content = (byte[])dt.Rows[0]["canvas_content"];
                    CreatPath(path);
                    if (File.Exists(fileName) == true)
                    {

                        File.Delete(fileName);

                    }

                    FileStream fs = new FileStream(fileName, FileMode.CreateNew);
                    fs.Write(content, 0, System.Convert.ToInt32(content.Length));
                    fs.Seek(0, SeekOrigin.Begin);
                    fs.Close();
                    //Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                    return fileName;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public static void GetDocumentInfo(string canvasID, Dalssoft.DiagramNet.Document docment)
        {
            string sql = "select canvas_ID,canvas_code,canvas_name,monitor_type from AT_CANVAS_B where canvas_ID='" + canvasID + "'";
            DataTable dt = data.DBQuery.OpenTable1(sql);
            if (dt.Rows.Count > 0)
            {
                docment.画布编号 = dt.Rows[0]["canvas_code"].ToString();
                docment.画布名称 = dt.Rows[0]["canvas_name"].ToString();
                docment.画布类型 = (Dalssoft.DiagramNet.CanvasType)Convert.ToInt32(dt.Rows[0]["monitor_type"].ToString());
                docment.画布ID = dt.Rows[0]["canvas_ID"].ToString();
            }
        }

        public static bool CreatPath(string path)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(path);
                if (!di.Exists)
                {
                  
                    di.Create(); 
                }
                return true;
            }
            catch 
            {
                return false;
            }

        }

        public static bool DeleteFolder(string path)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(path);
                if (di.Exists)
                {
                    Directory.Delete(di.ToString(), true);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public static string GetAlertMediaFileName(string path)
        {

            string filepath = "";
            if (System.IO.File.Exists(path))
            {
                string content = File.ReadAllText(path);
                filepath = content;
            }
            return filepath;
        }

        private static void LoadNodes(TreeNode node,int type)
        { 
            string sqlStr = "";
            DataTable dt = new DataTable();
            sqlStr = "select * from AT_CANVAS_B where parent_canvas_ID='" + node.Tag.ToString() + "' and monitor_type='" + type + "' order by canvas_name asc";
            dt = data.DBQuery.OpenTable1(sqlStr);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TreeNode myNode = new TreeNode(dt.Rows[i]["canvas_name"].ToString(), 2, 3);
                    myNode.Tag = dt.Rows[i]["canvas_ID"].ToString();
                    myNode.Name = dt.Rows[i]["canvas_code"].ToString();
                    node.Nodes.Add(myNode);
                    LoadNodes(myNode,type);
                }
            }
 
        }

        private static void LoadNodes(TreeNode node)
        {
            try
            {
                string sqlStr = "";
                DataTable dt = new DataTable();
                sqlStr = "select * from AT_CANVAS_B where parent_canvas_ID='" + node.Tag.ToString() + "' order by canvas_name asc";
                dt = data.DBQuery.OpenTable1(sqlStr);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TreeNode myNode = new TreeNode(dt.Rows[i]["canvas_name"].ToString(), 2, 3);
                        myNode.Tag = dt.Rows[i]["canvas_ID"].ToString();
                        myNode.Name = dt.Rows[i]["canvas_code"].ToString();
                        node.Nodes.Add(myNode);
                        LoadNodes(myNode);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接失败：" + ex.ToString());
            }
        }

        /// <summary>
        /// 将文件转换成二进制数组
        /// </summary>
        /// <param name="fileName">文件完整路径</param>
        /// <returns></returns>
        public static byte[] ReadFileToByteArray(string fileName)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Read);
            long len;
            len = fileStream.Length;
            byte[] fileAsByte = new byte[len];
            
            fileStream.Read(fileAsByte, 0, fileAsByte.Length);
            MemoryStream memoryStream = new MemoryStream(fileAsByte);
            return memoryStream.ToArray();
        }

        public static void GetElement(Dalssoft.DiagramNet.Document document, int index)
        {
            if (index < 0)
                return;
            if (index>= document.Elements.Count)
            {
                return;
            }
            Dalssoft.DiagramNet.BaseElement el = document.Elements[index];
            document.ClearSelection();
            
          
        }

        public static void AutoSizeDataGridView(DataGridView dataGridView1)
        {
            //dataGridView1.AutoResizeColumns();//显示的所有的列
            if (dataGridView1.ColumnCount != 0)
            {
                int a = 0;
                int count = 0;//
                dataGridView1.AutoResizeColumns();//显示的所有的列
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    if (dataGridView1.Columns[i].Visible == true)
                    {
                        a = a + dataGridView1.Columns[i].Width;
                        count++;
                    }
                }
                if (a < dataGridView1.Parent.Width)
                {
                    int b = dataGridView1.Parent.Width - a;
                    int c = b / count;
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        if (dataGridView1.Columns[i].Visible == true)
                            dataGridView1.Columns[i].Width = dataGridView1.Columns[i].Width + c;
                    }
                }
            }

        }

        public static string GetParentCanvasId(string canvasID)
        {
            string parentID = null;
            string sql = "select parent_canvas_ID from AT_CANVAS_B where canvas_ID='" + canvasID + "'";
            DataTable dt = data.DBQuery.OpenTable1(sql);
            if (dt.Rows.Count > 0)
            {
                parentID = dt.Rows[0][0].ToString();
            }
            return parentID;
        }
    }
}
