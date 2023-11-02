using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
//using log4net;

namespace HFUTIEMES
{
    class SystemLog
    {
        #region 属性
        /// <summary>
        /// 用户编号
        /// </summary>
        public static string UserCode = "";//用户编号
        /// <summary>
        /// 用户key
        /// </summary>
        public static string UserKey = "";//用户key
        /// <summary>
        /// 用户姓名
        /// </summary>
        public static string UserName = "";//姓名
        /// <summary>
        /// 是否可以进入MainForm
        /// </summary>
        public static int InOrOut = 0;//
        /// <summary>
        /// 当前计算机名
        /// </summary>
        public static string PCname = "";//计算机名
        /// <summary>
        /// 当前计算机IP
        /// </summary>
        public static string IP = "";//IP地址
        public static string personalRolesName = "";
        public static string LoginName = "";
        public static ArrayList personalPowerList;
        public static bool Test = true;
        /// <summary>
        /// 当前用户权限列表
        /// </summary>
        public static ArrayList RightList = new ArrayList();//权限表
        #endregion

        #region 方法
        /// <summary>
        /// 添加操作日志
        /// </summary>
        /// <param name="ct">操作内容</param>
        /// <param name="OperateType">操作类型（添加，修改，删除等等）</param>
        public static void AddLog1(string ct)
        {
            data.DBQuery.ExceuteNonQuery("insert into SYSTEM (usercode,username,ct,time,pcname,ip) values('" + UserCode + "','" + UserName + "','" + ct + "','" + DateTime.Now + "','" + PCname + "','" + IP + "')");
        }
        public static void AddLog(string ct,string a)
        {
            
        }

        /// <summary>
        /// 判断是否有操作权限
        /// </summary>
        /// <param name="RightIP">权限编号</param>
        /// <returns></returns>
        public static bool RightJudge(string RightIP)
        {
            return true;
        }

        public static ArrayList getPowerList(string personalID)
        {
            ArrayList list = new ArrayList();
            if (personalID == "")
            {
                return list;
            }

            string sqlStr = "";
            DataTable dt = new DataTable();
            try
            {
                sqlStr = "select a.right_key,a.right_code,a.right_name from RIGHT_INFO a,RIGHT_ROLE b,RIGHT_USER_ROLE c where b.role_key=c.role_key and a.right_key=b.right_key and c.user_key='" + personalID + "'";
                DataSet ds = DbHelperSQL.Query(sqlStr);
                 dt = ds.Tables[0];
                //dt = data.DBQuery.OpenTable1(sqlStr);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        list.Add(dt.Rows[i]["right_code"].ToString());
                    }
                }
                sqlStr = "";
                dt = new DataTable();
                sqlStr = "select a.right_name,a.right_code,a.right_key from RIGHT_INFO a,RIGHT_USER b where a.right_key=b.right_key and b.user_key='" + personalID + "'";
                dt = data.DBQuery.OpenTable1(sqlStr);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (list.Contains(dt.Rows[i]["right_code"].ToString()))
                    {
                        continue;
                    }
                    else
                    {
                        list.Add(dt.Rows[i]["right_code"].ToString());
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                MessageBox.Show("获得操作权限时出现错误!");
                list = new ArrayList();
                return list;
            }
        }
        #endregion
    }
}
