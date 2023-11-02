using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Collections.Generic;

namespace Dalssoft.DiagramNet
{
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
    public class ProductTraceExcel : BaseElement, IControllable
    {
        [NonSerialized]
        private RectangleController controller;
        //
        protected Color backColor = Color.GreenYellow;
        protected Color stringheadColor = Color.Green;
        protected Color stringfootColor = Color.Red;
        protected Font headfont = new Font(FontFamily.GenericSansSerif, 10);
        protected Font footfont = new Font(FontFamily.GenericSansSerif, 10);
        protected int line1 = 100;
        protected int line2 = 100;
        protected int line3 = 100;
        //滚动用
        public int StartRow = 0;
        public string[,] arr;
        protected int rows = 5;

        //[CategoryAttribute("监控信息")]
        //[DescriptionAttribute("监控对象")]
        //[DynamicProps.DynamicList("SQL")]
        //[TypeConverterAttribute(typeof(DynamicProps.NameConverter))]
        //[RefreshProperties(RefreshProperties.All)]
        //public override string 监控对象
        //{
        //    get
        //    {
        //        return monitorObject;
        //    }
        //    set
        //    {
        //        if (value != string.Empty)
        //        {
        //            monitorObject = value;
        //            DynamicProps.ListAttribute attributes = new DynamicProps.ListAttribute(SQL);
        //            int index = attributes.codeNameCollection.IndexOf(this.监控对象);
        //            monitoredObjectID = attributes.idCollection[index].ToString();
        //            monitoredObjectCode = attributes.codeCollection[index].ToString();
        //            monitoredObjectName = attributes.nameCollection[index].ToString();
        //            OnAppearanceChanged(new EventArgs());
        //        }
        //    }
        //}

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
        [Description("第二列长度")]
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
        [Description("第三列长度")]
        public virtual int Line3
        {
            get { return line3; }
            set
            {
                if (value < 0) { line3 = 0; } else { line3 = value; }
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

        public ProductTraceExcel()
            : this(0, 0, 100, 100)
        { }

        public ProductTraceExcel(Rectangle rec)
            : this(rec.Location, rec.Size)
        { elementMonitoredType = MonitoredType.产品跟踪表; }

        public ProductTraceExcel(Point l, Size s)
            : this(l.X, l.Y, s.Width, s.Height)
        { }

        public ProductTraceExcel(int top, int left, int width, int height)
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
            Rectangle r1 = GetUnsignedRectangle(new Rectangle(location.X, location.Y, line1, (int)(size.Height * (float)(1.5 / (1.5 + rows)))));
            Rectangle r2 = GetUnsignedRectangle(new Rectangle(location.X + line1, location.Y, line2, (int)(size.Height * (float)(1.5 / (1.5 + rows)))));
            Rectangle r3 = GetUnsignedRectangle(new Rectangle(location.X + line1 + line2, location.Y, line3, (int)(size.Height * (float)(1.5 / (1.5 + rows)))));
            Rectangle r4 = GetUnsignedRectangle(new Rectangle(location.X + line1 + line2 + line3, location.Y, size.Width - line1 - line2 - line3, (int)(size.Height * (float)(1.5 / (1.5 + rows)))));
            g.DrawString("产品出生证", headfont, new SolidBrush(stringheadColor), r1, sf);
            g.DrawString("产品名称", headfont, new SolidBrush(stringheadColor), r2, sf);
            g.DrawString("工位编号", headfont, new SolidBrush(stringheadColor), r3, sf);
            g.DrawString("设备编号", headfont, new SolidBrush(stringheadColor), r4, sf);
            g.DrawLine(new Pen(borderColor, borderWidth), location.X, location.Y, location.X + (int)(size.Width), location.Y);
            for (int i = 0; i < rows + 1; i++)
            {
                g.DrawLine(new Pen(borderColor, borderWidth), location.X, location.Y + (int)(size.Height * ((float)(1.5 + i) / (1.5 + rows))), location.X + (int)(size.Width), location.Y + (int)(size.Height * ((float)(1.5 + i) / (1.5 + rows))));
            }
            g.DrawLine(new Pen(borderColor, borderWidth), location.X, location.Y, location.X, location.Y + (int)(size.Height));
            g.DrawLine(new Pen(borderColor, borderWidth), location.X + line1, location.Y, location.X + line1, location.Y + (int)(size.Height));
            g.DrawLine(new Pen(borderColor, borderWidth), location.X + +line1 + line2, location.Y, location.X + line1 + line2, location.Y + (int)(size.Height));
            g.DrawLine(new Pen(borderColor, borderWidth), location.X + line1 + line2 + line3, location.Y, location.X + line1 + line2 + line3, location.Y + (int)(size.Height));
            g.DrawLine(new Pen(borderColor, borderWidth), location.X + size.Width, location.Y, location.X + size.Width, location.Y + (int)(size.Height));
            var dt = DbHelperSQL.OpenTable("select ProductBornCode,ProductName,WorkCenterCode,EquipCode from Product_Route_P order by Starttime ");
            if (dt.Rows.Count > 0)
            {
                arr = null;
                arr = new string[dt.Rows.Count, 4];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        arr[i, j] = dt.Rows[i][j].ToString().Trim();
                    }
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
                        g.DrawString(arr[x + StartRow, 0].ToString().Trim(), footfont, new SolidBrush(stringfootColor), new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, (int)(size.Height * 1.0 / (1.5 + rows)))), sf);

                        g.DrawString(arr[x + StartRow, 1].ToString().Trim(), footfont, new SolidBrush(stringfootColor), new RectangleF(new System.Drawing.Point(location.X + line1, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line2, (int)(size.Height * 1.0 / (1.5 + rows)))), sf);

                        g.DrawString(arr[x + StartRow, 2].ToString().Trim(), footfont, new SolidBrush(stringfootColor), new RectangleF(new System.Drawing.Point(location.X + line1 + line2, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line3, (int)(size.Height * 1.0 / (1.5 + rows)))), sf);

                        g.DrawString(arr[x + StartRow, 3].ToString().Trim(), footfont, new SolidBrush(stringfootColor), new RectangleF(new System.Drawing.Point(location.X + line1 + line2 + line3, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(size.Width - line1 - line2 - line3, (int)(size.Height * 1.0 / (1.5 + rows)))), sf);
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
                        g.DrawString(arr[x, 0].ToString().Trim(), footfont, new SolidBrush(stringfootColor), new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line1, (int)(size.Height * 1.0 / (1.5 + rows)))), sf);
                        g.DrawString(arr[x, 1].ToString().Trim(), footfont, new SolidBrush(stringfootColor), new RectangleF(new System.Drawing.Point(location.X + line1, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line2, (int)(size.Height * 1.0 / (1.5 + rows)))), sf);
                        g.DrawString(arr[x, 2].ToString().Trim(), footfont, new SolidBrush(stringfootColor), new RectangleF(new System.Drawing.Point(location.X + line1 + line2, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(line3, (int)(size.Height * 1.0 / (1.5 + rows)))), sf);
                        g.DrawString(arr[x, 3].ToString().Trim(), footfont, new SolidBrush(stringfootColor), new RectangleF(new System.Drawing.Point(location.X + line1 + line2 + line3, location.Y + (int)(size.Height * (1.5 + x) / (1.5 + rows))), new Size(size.Width - line1 - line2 - line3, (int)(size.Height * 1.0 / (1.5 + rows)))), sf);
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
