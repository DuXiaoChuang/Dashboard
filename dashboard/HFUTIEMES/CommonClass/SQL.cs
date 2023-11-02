using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace HFUTIEMES
{
    public static class SQL
    {
        /// <summary>
        /// 由列名及其值搜索指定表的指定字段
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="goalColumn">目标列</param>
        /// <param name="searchColumn">搜索条件列</param>
        /// <param name="value">搜索条件值</param>
        /// <returns></returns>

        public static string QueryByValue(string tableName, string goalColumn, string searchColumn, string value)
        {
            string sql = "select " + goalColumn + " from " + tableName + " where " + searchColumn + " = '" + value + "'";
            DataTable dt = data.DBQuery.OpenTable1(sql);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
                return null;
        }

        public static string QueryByValue(string tableName, string goalColumn, string searchColumn, long value)
        {
            string sql = "select " + goalColumn + " from " + tableName + " where " + searchColumn + " = " + value + "";
            DataTable dt = data.DBQuery.OpenTable1(sql);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
                return null;
        }

        /// <summary>
        /// 由列名及其值搜索指定表的指定字段
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="goalColumns">目标列</param>
        /// <param name="searchColumn">搜索条件列</param>
        /// <param name="value">搜索条件值</param>
        /// <returns></returns>
        public static DataTable QueryByValue(string tableName, string[] goalColumns, string searchColumn, string value)
        {
            DataTable dt = new DataTable();
            if (goalColumns.Length >= 1)
            {
                string sql = "";
                sql += "select ";
                for (int i = 0; i < goalColumns.Length; i++)
                {
                    sql += goalColumns[i];
                    if (i < goalColumns.Length - 1)
                    { sql += ","; }
                }
                sql += " from " + tableName + " where " + searchColumn + "='" + value + "'";

                dt = data.DBQuery.OpenTable1(sql);
            }

            return dt;
        }

        public static DataTable QueryByValue(string tableName, string[] goalColumns, string searchColumn, long value)
        {
            DataTable dt = new DataTable();
            if (goalColumns.Length >= 1)
            {
                string sql = "";
                sql += "select ";
                for (int i = 0; i < goalColumns.Length; i++)
                {
                    sql += goalColumns[i];
                    if (i < goalColumns.Length - 1)
                    { sql += ","; }
                }
                sql += " from " + tableName + " where " + searchColumn + "=" + value + "";

                dt = data.DBQuery.OpenTable1(sql);
            }

            return dt;
        }

        /// <summary>
        /// 根据编号找UDA表的名称
        /// </summary>
        /// <param name="style">类型</param>
        /// <param name="code">编号</param>
        /// <returns></returns>
        public static string findNameByCode(string style, string code)
        {
            string sql = "";
            string name = "";
            switch (style)
            {
                case "WORK_CENTER":
                    sql = "select b.wc_mc_u_S from WORK_CENTER a , UDA_WorkCenter b where a.wc_key=b.object_key and a.wc_name='" + code + "'";
                    break;
                case "EQUIPMENT":
                    sql = "select b.equip_mc_u_S from EQUIPMENT a , UDA_Equipment b where a.equip_key=b.object_key and a.equip_name='" + code + "'";
                    break;
                case "PRODUCTION_LINE":
                    sql = "select b.p_line_mc_u_S from PRODUCTION_LINE a , UDA_ProductionLine b where a.p_line_key=b.object_key and a.p_line_name='" + code + "'";
                    break;
                case "PART":
                    sql = "select b.part_mc_u_S from PART a , UDA_Part b where a.part_key=b.object_key and a.part_name='" + code + "'";
                    break;
                case "BOM":
                    sql = "select b.bom_mc_u_S from BOM a , UDA_BillOfMaterials b where a.bom_key=b.object_key and a.bom_name='" + code + "'";
                    break;
                case "ACCOUNT":
                     sql = "select b.account_mc_u_S from ACCOUNT a , UDA_Account b where a.account_key=b.object_key and a.account_name='" + code + "'";
                    break;
            }
            DataTable dt = data.DBQuery.OpenTable1(sql);
            if (dt.Rows.Count > 0)
            {
                name = dt.Rows[0][0].ToString();
            }
            return name;
        }

    }
}
