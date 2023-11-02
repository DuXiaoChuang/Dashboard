using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;

namespace Dalssoft.DiagramNet
{
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class ToolStateExcel : BaseElement, IControllable
    {
        [NonSerialized]
        private RectangleController controller;
        protected Image imageDefault = Diagram.NET.Resource.tool;
        protected Color backColor = Color.GreenYellow;
        protected Color stringheadColor = Color.Blue ;
        protected Color stringfootColor = Color.Green;
        protected Font headfont = new Font(FontFamily.GenericSansSerif, 10);
        protected Font footfont = new Font(FontFamily.GenericSansSerif, 10);
        protected int line1 = 100;
        protected int line2 = 100;
        protected Statistics_type1 statisticstyle1 = Statistics_type1.无;

        //滚动用
        public int StartRow = 0;
        public string[,] arr;
        protected int rows = 5;

        [CategoryAttribute("监控信息")]
        [DescriptionAttribute("监控对象")]
        [DynamicProps.DynamicList("SQL")]
        [TypeConverterAttribute(typeof(DynamicProps.NameConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public override string 监控对象
        {
            get
            {
                return monitorObject;
            }
            set
            {
                if (value != string.Empty)
                {
                    monitorObject = value;
                    DynamicProps.ListAttribute attributes = new DynamicProps.ListAttribute(SQL);
                    int index = attributes.codeNameCollection.IndexOf(this.监控对象);
                    monitoredObjectID = attributes.idCollection[index].ToString();
                    monitoredObjectCode = attributes.codeCollection[index].ToString();
                    monitoredObjectName = attributes.nameCollection[index].ToString();
                    OnAppearanceChanged(new EventArgs());
                }
            }
        }
        [CategoryAttribute("监控信息")]
        [DescriptionAttribute("统计类型")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual Statistics_type1 区域
        {
            get
            {
                return statisticstyle1;
            }
            set
            {
                statisticstyle1 = value;
                OnAppearanceChanged(new EventArgs());
            }
        }
        [Category("设计")]
        [Description("第一列长度")]
        public virtual int Line1
        {
            get { return line1; }
            set
            {
                if (value < 0) { line1 = 0; } else { line1 = value; }
                OnAppearanceChanged(new EventArgs());
            }
        }

        [Category("设计")]
        [Description("最后一列长度")]
        public virtual int Line2
        {
            get { return line2; }
            set
            {
                if (value < 0) { line2 = 0; } else { line2 = value; }
                OnAppearanceChanged(new EventArgs());
            }
        }

        [Category("设计")]
        [Description("行数")]
        public virtual int 行数
        {
            get { return rows; }
            set
            {
                if (value < 0) { rows = 0; } else { rows = value; }
                OnAppearanceChanged(new EventArgs());
            }
        }

        [Category("设计")]
        [Description("背景颜色")]
        [Browsable(false)]
        public virtual Color 背景颜色
        {
            get { return backColor; }
            set
            {
                if (value != Color.Empty) { backColor = value; OnAppearanceChanged(new EventArgs()); }
            }
        }

        [Category("设计")]
        [Description("表头文字颜色")]
        public virtual Color 表头文字颜色
        {
            get { return stringheadColor; }
            set
            {
                if (value != Color.Empty) { stringheadColor = value; OnAppearanceChanged(new EventArgs()); }
            }
        }

        [Category("设计")]
        [Description("表底文字颜色")]
        public virtual Color 表底文字颜色
        {
            get { return stringfootColor; }
            set
            {
                if (value != Color.Empty) { stringfootColor = value; OnAppearanceChanged(new EventArgs()); }
            }
        }

        [Category("设计")]
        [Description("表头字体")]
        public virtual Font 表头字体
        {
            get
            {
                return headfont;
            }
            set
            {
                headfont = value;
                OnAppearanceChanged(new EventArgs());
            }
        }

        [Category("设计")]
        [Description("表底字体")]
        public virtual Font 表底字体
        {
            get
            {
                return footfont;
            }
            set
            {
                footfont = value;
                OnAppearanceChanged(new EventArgs());
            }
        }

        [Category("外观")]
        [Description("大小")]
        public override Size Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                size = value;
                OnAppearanceChanged(new EventArgs());
            }
        }
        public ToolStateExcel()
            : this(0, 0, 100, 100)
        { }
        public ToolStateExcel(Rectangle rec)
            : this(rec.Location, rec.Size)
        { elementMonitoredType = MonitoredType.刀具状态信息表; }
        public ToolStateExcel(Point l, Size s)
            : this(l.X, l.Y, s.Width, s.Height)
        { }

        public ToolStateExcel(int top, int left, int width, int height)
        {
            location = new Point(top, left);
            size = new Size(width, height);
        }
        internal override void Draw(Graphics g)
        {
            IsInvalidated = false;
            Rectangle r = GetUnsignedRectangle(
                 new Rectangle(
                 location.X, location.Y,
                 size.Width, size.Height));
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            int a = (int)((Size.Width - line1 - line2) /5);//第一列和最后一列外的其他列宽度
            int b = (int)(size.Height * (float)(1.5 / (1.5 + rows)));//标题高度
            int c = (int)(size.Height * (float)(1.0 / (1.5 + rows)));//每行高度
            Rectangle r1 = GetUnsignedRectangle(new Rectangle(location.X, location.Y, line1, b));
            Rectangle r2 = GetUnsignedRectangle(new Rectangle(location.X + line1, location.Y, a, b));
            Rectangle r3 = GetUnsignedRectangle(new Rectangle(location.X + line1 + a, location.Y, a, b));
            Rectangle r4 = GetUnsignedRectangle(new Rectangle(location.X + line1 + 2 * a, location.Y, a, b));
            Rectangle r5 = GetUnsignedRectangle(new Rectangle(location.X + line1 + 3 * a, location.Y, a, b));
            Rectangle r6 = GetUnsignedRectangle(new Rectangle(location.X + line1 + 4 * a, location.Y, a, b));
            Rectangle r7 = GetUnsignedRectangle(new Rectangle(location.X + line1 + 5 * a, location.Y, line2, b));
            g.DrawString("刀具图例", headfont, new SolidBrush(stringheadColor), r1, sf);
            g.DrawString("刀具编号", headfont, new SolidBrush(stringheadColor), r2, sf);
            g.DrawString("剩余寿命", headfont, new SolidBrush(stringheadColor), r3, sf);
            g.DrawString("剩余寿命预警", headfont, new SolidBrush(stringheadColor), r4, sf);
            g.DrawString("设备编号", headfont, new SolidBrush(stringheadColor), r5, sf);
            g.DrawString("使用时间", headfont, new SolidBrush(stringheadColor), r6, sf);
            g.DrawString("状态", headfont, new SolidBrush(stringheadColor), r7, sf);
            g.DrawLine(new Pen(borderColor, borderWidth), location.X, location.Y, location.X + (int)(size.Width), location.Y);
            for (int i = 0; i < rows + 1; i++)
            {
                g.DrawLine(new Pen(borderColor, borderWidth), location.X, location.Y + (int)(size.Height * ((float)(1.5 + i) / (1.5 + rows))), location.X + (int)(size.Width), location.Y + (int)(size.Height * ((float)(1.5 + i) / (1.5 + rows))));
            }
            g.DrawLine(new Pen(borderColor, borderWidth), location.X, location.Y, location.X, location.Y + (int)(size.Height));
            g.DrawLine(new Pen(borderColor, borderWidth), location.X + line1, location.Y, location.X + line1, location.Y + (int)(size.Height));
            g.DrawLine(new Pen(borderColor, borderWidth), location.X + +line1 + a, location.Y, location.X + line1 + a, location.Y + (int)(size.Height));
            g.DrawLine(new Pen(borderColor, borderWidth), location.X + line1 + 2 * a, location.Y, location.X + line1 + 2 * a, location.Y + (int)(size.Height));
            g.DrawLine(new Pen(borderColor, borderWidth), location.X + line1 + 3 * a, location.Y, location.X + line1 + 3 * a, location.Y + (int)(size.Height));
            g.DrawLine(new Pen(borderColor, borderWidth), location.X + line1 + 4 * a, location.Y, location.X + line1 + 4 * a, location.Y + (int)(size.Height));
            g.DrawLine(new Pen(borderColor, borderWidth), location.X + line1 + 5 * a, location.Y, location.X + line1 + 5 * a, location.Y + (int)(size.Height));
            g.DrawLine(new Pen(borderColor, borderWidth), location.X + size.Width, location.Y, location.X + size.Width, location.Y + (int)(size.Height));
            string sql = "";
            if (MoniteredObjectID != "")
            {
                if (statisticstyle1.ToString() == "无")
                    sql = "select ToolCode,ToolResidualLife,lifeWarning,EquipCode,ToolStarttime from Tool_State_P  where   reserve5='" + monitoredObjectID + "'order by ToolCode";
                else if (statisticstyle1.ToString() == "区域1")
                    sql = "select ToolCode,ToolResidualLife,lifeWarning,EquipCode,ToolStarttime from Tool_State_P  where    reserve5='" + monitoredObjectID + "''and (equipcode in (select equipcode from EquipMent_B where zone=1))order by ToolCode";
                else if (statisticstyle1.ToString() == "区域2")
                    sql = "select ToolCode,ToolResidualLife,lifeWarning,EquipCode,ToolStarttime from Tool_State_P   where    reserve5='" + monitoredObjectID + "''and (equipcode in (select equipcode from EquipMent_B where zone=2))order by ToolCodee ";
                else if (statisticstyle1.ToString() == "区域3")
                    sql = "select ToolCode,ToolResidualLife,lifeWarning,EquipCode,ToolStarttime from Tool_State_P  where    reserve5='" + monitoredObjectID + "''and (equipcode in (select equipcode from EquipMent_B where zone=3))order by ToolCode";
                //DataTable dt = DbHelperSQL.OpenTable(sql);
                //a = dt.Rows.Count;
            }
            else
            {
                if(statisticstyle1.ToString() == "无")
                    sql = "select ToolCode,ToolResidualLife,lifeWarning,EquipCode,ToolStarttime from Tool_State_P  where   reserve5='" + monitoredObjectID + "'order by ToolCode";
                else if (statisticstyle1.ToString() == "区域1")
                    sql = "select ToolCode,ToolResidualLife,lifeWarning,EquipCode,ToolStartime from Tool_State_P  where    reserve5='" + monitoredObjectID + "''and (equipcode in (select equipcode from EquipMent_B where zone=1))order by ToolCode";
                else if (statisticstyle1.ToString() == "区域2")
                    sql = "select ToolCode,ToolResidualLife,lifeWarning,EquipCode,ToolStarttime from Tool_State_P   where    reserve5='" + monitoredObjectID + "''and (equipcode in (select equipcode from EquipMent_B where zone=2))order by ToolCodee ";
                else if (statisticstyle1.ToString() == "区域3")
                    sql = "select ToolCode,ToolResidualLife,lifeWarning,EquipCode,ToolStarttime from Tool_State_P  where    reserve5='" + monitoredObjectID + "''and (equipcode in (select equipcode from EquipMent_B where zone=3))order by ToolCode";
            }
            //    if (statisticstyle1.ToString() == "无")
            //        sql = "select ToolCode,ToolResidualLife,lifeWarning,EquipCode,UseTime from Tool_State_P  where EquipCode in (select EquipCode from Equip_State_P  where reserve5='MoniteredObjectID'and StateDescription = '运行状态'and (equipcode in (select equipcode from EquipMent_B )))order by ToolCode";
            //    else if (statisticstyle1.ToString() == "区域1")
            //        sql = "select ToolCode,ToolResidualLife,lifeWarning,EquipCode,UseTime from Tool_State_P  where EquipCode in (select EquipCode from Equip_State_P  where reserve5='MoniteredObjectID'and StateDescription = '运行状态'and (equipcode in (select equipcode from EquipMent_B where zone=1)))order by ToolCode";
            //    else if (statisticstyle1.ToString() == "区域2")
            //        sql = "select ToolCode,ToolResidualLife,lifeWarning,EquipCode,UseTime from Tool_State_P  where EquipCode in (select EquipCode from Equip_State_P  where reserve5='MoniteredObjectID'and StateDescription = '运行状态'and (equipcode in (select equipcode from EquipMent_B where zone=2)))order by ToolCode";
            //    else if (statisticstyle1.ToString() == "区域3")
            //        sql = "select ToolCode,ToolResidualLife,lifeWarning,EquipCode,UseTime from Tool_State_P  where EquipCode in (select EquipCode from Equip_State_P  where reserve5='MoniteredObjectID'and StateDescription = '运行状态'and (equipcode in (select equipcode from EquipMent_B where zone=3)))order by ToolCode";
            //}
            //else
            //{
            //    if (statisticstyle1.ToString() == "无")
            //        sql = "select ToolCode,ToolResidualLife,lifeWarning,EquipCode,UseTime from Tool_State_P order by ToolCode";
            //    else if (statisticstyle1.ToString() == "区域1")
            //        sql = "select ToolCode,ToolResidualLife,lifeWarning,EquipCode,UseTime from Tool_State_P  where EquipCode in (select EquipCode from Equip_State_P  where  StateDescription = '运行状态'and (equipcode in (select equipcode from EquipMent_B where zone=1)))order by ToolCode";
            //    else if (statisticstyle1.ToString() == "区域2")
            //        sql = "select ToolCode,ToolResidualLife,lifeWarning,EquipCode,UseTime from Tool_State_P  where EquipCode in (select EquipCode from Equip_State_P  where  StateDescription = '运行状态'and (equipcode in (select equipcode from EquipMent_B where zone=2)))order by ToolCode";
            //    else if (statisticstyle1.ToString() == "区域2")
            //        sql = "select ToolCode,ToolResidualLife,lifeWarning,EquipCode,UseTime from Tool_State_P  where EquipCode in (select EquipCode from Equip_State_P  where  StateDescription = '运行状态'and (equipcode in (select equipcode from EquipMent_B where zone=3)))order by ToolCode";
            //}
            DataTable dt = DbHelperSQL.OpenTable(sql);
            if (dt.Rows.Count > 0)
            {
                arr = null;
                arr = new string[dt.Rows.Count, 6];//最后一列为剩余寿命与预警寿命的比较值，标识颜色
                int residual;
                int lifewarn;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        arr[i, j] = dt.Rows[i][j].ToString().Trim();
                    }
                    residual =System .Convert .ToInt32 (dt.Rows[i][1]);//剩余寿命
                    lifewarn =System .Convert .ToInt32 (dt.Rows[i][2]);//剩余寿命预警
                    if(residual<0)
                        arr[i, 5] = "超期";//剩余寿命小于0，超期，红色
                    else if(residual<lifewarn)
                        arr[i, 5] = "预警";//低于预警寿命，橙色
                    else 
                        arr[i, 5] = "正常";//高于预警寿命，绿色
                }
                if ((dt.Rows.Count) > rows)
                {
                    int drawRowsCount = dt.Rows.Count - StartRow;
                    if ((dt.Rows.Count - StartRow) > rows)
                    {
                        drawRowsCount = rows;
                    }
                    for (int x = 0; x < drawRowsCount; x++)
                    {
                        if (arr[x + StartRow, 5].ToString() == "超期")
                            stringfootColor = Color.Red;
                        if (arr[x + StartRow, 5].ToString() == "预警")
                            stringfootColor = Color.Orange ;
                        if (arr[x + StartRow, 5].ToString() == "正常")
                            stringfootColor = Color.Green;
                            if (arr[x + StartRow, 0].ToString().Trim() == "")
           { g.DrawImage(imageDefault, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c))); }
             else
            {
            string rr = arr[x + StartRow, 0].ToString().Trim();
            if (rr == "T00001")
            {
            Image imageDefault1 = Diagram.NET.Resource.T00001;
            g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));                    }
            if (rr == "T00002")
            {
             Image imageDefault1 = Diagram.NET.Resource.T00002;
             g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
             }
             if (rr == "T00003")
             {
              Image imageDefault1 = Diagram.NET.Resource.T00003;
              g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
               }
              if (rr == "T00004")
            {
                Image imageDefault1 = Diagram.NET.Resource.T00004;
                                g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
                            }
                            if (rr == "T00005")
                            {
                                Image imageDefault1 = Diagram.NET.Resource.T00005;
                                g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
                            }
                            if (rr == "T00006")
                            {
                                Image imageDefault1 = Diagram.NET.Resource.T00006;
                                g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
                            }
                            if (rr == "T00007")
                            {
                                Image imageDefault1 = Diagram.NET.Resource.T00007;
                                g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
                            }
                            if (rr == "T00008")
                            {
                                Image imageDefault1 = Diagram.NET.Resource.T00008;
                                g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
                            }
                            if (rr == "T00009")
                            {
                                Image imageDefault1 = Diagram.NET.Resource.T00009;
                                g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
                            }
                            if (rr == "T00010")
                            {
                                Image imageDefault1 = Diagram.NET.Resource.T00010;
                                g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
                            }
                            if (rr == "T00011")
                            {
                                Image imageDefault1 = Diagram.NET.Resource.T00011;
                                g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
                            }
                            if (rr == "T00012")
                            {
                                Image imageDefault1 = Diagram.NET.Resource.T00012;
                                g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
                            }
                            if (rr == "T00013")
                            {
                                Image imageDefault1 = Diagram.NET.Resource.T00013;
                                g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
                            }
                            if (rr == "T00014")
                            {
                                Image imageDefault1 = Diagram.NET.Resource.T00014;
                                g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
                            }
                            if (rr == "T00015")
                            {
                                Image imageDefault1 = Diagram.NET.Resource.T00015;
                                g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
                            }
                            if (rr == "T00016")
                            {
                                Image imageDefault1 = Diagram.NET.Resource.T00016;
                                g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
                            }
                            if (rr == "T00017")
                            {
                                Image imageDefault1 = Diagram.NET.Resource.T00017;
                                g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
                            }
                            //if (rr == "T00018")
                            //{
                            //    Image imageDefault1 = Diagram.NET.Resource.T00018;
                            //    g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
                            //}
                        }
                        g.DrawString(arr[x + StartRow, 0].ToString().Trim(), footfont, new SolidBrush(stringfootColor), new RectangleF(new System.Drawing.Point(location.X + line1, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(a, c)), sf);
                        g.DrawString(arr[x + StartRow, 1].ToString().Trim(), footfont, new SolidBrush(stringfootColor), new RectangleF(new System.Drawing.Point(location.X + line1 + a, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(a, c)), sf);
                        g.DrawString(arr[x + StartRow, 2].ToString().Trim(), footfont, new SolidBrush(stringfootColor), new RectangleF(new System.Drawing.Point(location.X + line1 + 2 * a, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(a, c)), sf);
                        g.DrawString(arr[x + StartRow, 3].ToString().Trim(), footfont, new SolidBrush(stringfootColor), new RectangleF(new System.Drawing.Point(location.X + line1 + 3 * a, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(a, c)), sf);
                        g.DrawString(arr[x + StartRow, 4].ToString().Trim(), footfont, new SolidBrush(stringfootColor), new RectangleF(new System.Drawing.Point(location.X + line1 + 4 * a, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(a, c)), sf);
                        g.DrawString(arr[x + StartRow, 5].ToString().Trim(), footfont, new SolidBrush(stringfootColor), new RectangleF(new System.Drawing.Point(location.X + line1 + 5 * a, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line2, c)), sf);
                    }
                    if ((dt.Rows.Count - StartRow) > rows)
                    {
                        StartRow += drawRowsCount;
                    }
                    else
                    {
                        StartRow = 0;
                    }
                }
                else     //如果行数小于5，则直接执行显示
                {
                    int drawRowsCount = dt.Rows.Count;
                    for (int x = 0; x < drawRowsCount; x++)
                    {
                        if (arr[x, 4].ToString() == "超期")
                            stringfootColor = Color.Red;
                        if (arr[x, 4].ToString() == "预警")
                            stringfootColor = Color.Orange;
                        if (arr[x, 4].ToString() == "正常")
                            stringfootColor = Color.Green;
                        if (arr[x + StartRow, 0].ToString().Trim() == "")
                        { g.DrawImage(imageDefault, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c))); }
                        else
                        {
                            string rr = arr[x + StartRow, 0].ToString().Trim();
                            if (rr == "T00001")
                            {
                                Image imageDefault1 = Diagram.NET.Resource.T00001;
                                g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
                            }
                            if (rr == "T00002")
                            {
                                Image imageDefault1 = Diagram.NET.Resource.T00002;
                                g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
                            }
                            if (rr == "T00003")
                            {
                                Image imageDefault1 = Diagram.NET.Resource.T00003;
                                g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
                            }
                            if (rr == "T00004")
                            {
                                Image imageDefault1 = Diagram.NET.Resource.T00004;
                                g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
                            }
                            if (rr == "T00005")
                            {
                                Image imageDefault1 = Diagram.NET.Resource.T00005;
                                g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
                            }
                            if (rr == "T00006")
                            {
                                Image imageDefault1 = Diagram.NET.Resource.T00006;
                                g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
                            }
                            if (rr == "T00007")
                            {
                                Image imageDefault1 = Diagram.NET.Resource.T00007;
                                g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
                            }
                            if (rr == "T00008")
                            {
                                Image imageDefault1 = Diagram.NET.Resource.T00008;
                                g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
                            }
                            if (rr == "T00009")
                            {
                                Image imageDefault1 = Diagram.NET.Resource.T00009;
                                g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
                            }
                            if (rr == "T00010")
                            {
                                Image imageDefault1 = Diagram.NET.Resource.T00010;
                                g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
                            }
                            if (rr == "T00011")
                            {
                                Image imageDefault1 = Diagram.NET.Resource.T00011;
                                g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
                            }
                            if (rr == "T00012")
                            {
                                Image imageDefault1 = Diagram.NET.Resource.T00012;
                                g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
                            }
                            if (rr == "T00012")
                            {
                                Image imageDefault1 = Diagram.NET.Resource.T00012;
                                g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
                            }
                            if (rr == "T00013")
                            {
                                Image imageDefault1 = Diagram.NET.Resource.T00013;
                                g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
                            }
                            if (rr == "T00014")
                            {
                                Image imageDefault1 = Diagram.NET.Resource.T00014;
                                g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
                            }
                            if (rr == "T00015")
                            {
                                Image imageDefault1 = Diagram.NET.Resource.T00015;
                                g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
                            }
                            if (rr == "T00016")
                            {
                                Image imageDefault1 = Diagram.NET.Resource.T00016;
                                g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
                            }
                            if (rr == "T00017")
                            {
                                Image imageDefault1 = Diagram.NET.Resource.T00017;
                                g.DrawImage(imageDefault1, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
                            }

                        }
                        //g.DrawImage(imageDefault, new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, c)));
                        g.DrawString(arr[x, 0].ToString().Trim(), footfont, new SolidBrush(stringfootColor), new RectangleF(new System.Drawing.Point(location.X + line1, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(a, c)), sf);
                        g.DrawString(arr[x, 1].ToString().Trim(), footfont, new SolidBrush(stringfootColor), new RectangleF(new System.Drawing.Point(location.X + line1 + a, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(a, c)), sf);
                        g.DrawString(arr[x, 2].ToString().Trim(), footfont, new SolidBrush(stringfootColor), new RectangleF(new System.Drawing.Point(location.X + line1 + 2 * a, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(a, c)), sf);
                        g.DrawString(arr[x, 3].ToString().Trim(), footfont, new SolidBrush(stringfootColor), new RectangleF(new System.Drawing.Point(location.X + line1 + 3 * a, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(a, c)), sf);
                        g.DrawString(arr[x , 4].ToString().Trim(), footfont, new SolidBrush(stringfootColor), new RectangleF(new System.Drawing.Point(location.X + line1 + 4 * a, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(a, c)), sf);
                        g.DrawString(arr[x,5].ToString().Trim(), footfont, new SolidBrush(stringfootColor), new RectangleF(new System.Drawing.Point(location.X + line1 + 5 * a, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line2, c)), sf);
                    }
                }
            }
        }
        #region interface 接口
        IController IControllable.GetController()
        {
            if (controller == null)
                controller = new RectangleController(this);
            return controller;
        }
        #endregion
    }
}
