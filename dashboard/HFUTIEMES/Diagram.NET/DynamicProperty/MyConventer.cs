using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Net;
using System.Runtime.InteropServices;

namespace DynamicProps
{

    #region ListAttribute
    public class ListAttribute:Attribute
    {
        public ArrayList codeNameCollection=new ArrayList();//编号名称集合
        public ArrayList nameCollection = new ArrayList();//名称集合
        public ArrayList codeCollection = new ArrayList();//编号集合
        public ArrayList idCollection = new ArrayList();//ID集合
        public ArrayList collumCollectionFull = new ArrayList();
        public ArrayList collumCollection = new ArrayList();

        public ListAttribute()
        {
           
        }

        public ListAttribute(string str)//0是key，1是编号，2是名称
        {
            //int i = 0;
            if (str != String.Empty)
            {
                DataTable dt = data.DBQuery.OpenTable1(str);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    codeNameCollection.Add(dt.Rows[i][2].ToString() + "(编号:" + dt.Rows[i][1].ToString() + ")");
                    nameCollection.Add(dt.Rows[i][2].ToString());
                    codeCollection.Add(dt.Rows[i][1].ToString());
                    idCollection.Add(dt.Rows[i][0].ToString());
                }
                codeNameCollection.Add("无");
                nameCollection.Add(String.Empty);
                codeCollection.Add(String.Empty);
                idCollection.Add(String.Empty);
            }
        }

        private string GetValue(string value,ArrayList list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                string result=list[i].ToString().Trim();
                if (result.Contains(value))
                {
                    return result;
                }
            }
            return "";
 
        }

    }
    #endregion

    #region NameConverter
    ///   <summary> 
    ///   扩展字符串的转换器（实现下拉列表框的样式） 
    ///   </summary> 
    public class NameConverter : System.ComponentModel.StringConverter
    {

        ///   <summary> 
        ///   根据返回值确定是否支持下拉框的形式 
        ///   </summary> 
        ///   <returns> 
        ///   true:   下来框的形式 
        ///   false:   普通文本编辑的形式 
        ///   </returns> 
        public override bool GetStandardValuesSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return true;
        }
        ///   <summary> 
        ///   下拉框中具体的内容 
        ///   </summary> 
        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(System.ComponentModel.ITypeDescriptorContext context)
        {
            ListAttribute lst = (ListAttribute)context.PropertyDescriptor.Attributes[typeof(ListAttribute)];

            StandardValuesCollection vals = new TypeConverter.StandardValuesCollection(lst.codeNameCollection.ToArray());


            return vals;

        }
        ///   <summary> 
        ///   根据返回值确定是否是不可编辑的文本框 
        ///   </summary> 
        ///   <returns> 
        ///   true:     文本框不可以编辑 
        ///   flase:   文本框可以编辑 
        ///   </returns> 
        public override bool GetStandardValuesExclusive(System.ComponentModel.ITypeDescriptorContext context)
        {
            return true;
        }

    }
    #endregion

    #region SelectionConverter
    ///   <summary> 
    ///   扩展字符串的转换器（实现下拉列表框的样式） 
    ///   </summary> 
    public class SelectionConverter : System.ComponentModel.StringConverter
    {

        ///   <summary> 
        ///   根据返回值确定是否支持下拉框的形式 
        ///   </summary> 
        ///   <returns> 
        ///   true:   下来框的形式 
        ///   false:   普通文本编辑的形式 
        ///   </returns> 
        public override bool GetStandardValuesSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return true;
        }
        ///   <summary> 
        ///   下拉框中具体的内容 
        ///   </summary> 
        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(System.ComponentModel.ITypeDescriptorContext context)
        {
            ListAttribute lst = (ListAttribute)context.PropertyDescriptor.Attributes[typeof(ListAttribute)];

            StandardValuesCollection vals = new TypeConverter.StandardValuesCollection(lst.collumCollection.ToArray());


            return vals;

        }
        ///   <summary> 
        ///   根据返回值确定是否是不可编辑的文本框 
        ///   </summary> 
        ///   <returns> 
        ///   true:     文本框不可以编辑 
        ///   flase:   文本框可以编辑 
        ///   </returns> 
        public override bool GetStandardValuesExclusive(System.ComponentModel.ITypeDescriptorContext context)
        {
            return true;
        }

    }
    #endregion

   
}