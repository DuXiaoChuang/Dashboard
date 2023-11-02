using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Dalssoft.DiagramNet;
using System.Drawing.Design;
using System.ComponentModel;
using System.Data;

namespace Dalssoft.DiagramNet
{
    [
   Serializable,
   TypeConverter(typeof(ExpandableObjectConverter))
   ]

    public class WorkOrder : BaseElement, IControllable
    {
        [NonSerialized]
        private RectangleController controller;
        private static string guname1 = "";
        private static string proname1 = "";
        private static string proname00 = "";
        public string[,] arr;
        public int StartRow = 0;
        protected Font font = new Font(FontFamily.GenericSansSerif, 10);
        protected Font font1 = new Font(FontFamily.GenericSansSerif, 8);
        protected Font font2 = new Font(FontFamily.GenericSansSerif, 8);
        protected Font font3 = new Font(FontFamily.GenericSansSerif, 8);
        protected Font font4 = new Font(FontFamily.GenericSansSerif, 8);
        protected Font font5 = new Font(FontFamily.GenericSansSerif, 8);
        protected Font font6 = new Font(FontFamily.GenericSansSerif, 8);
        protected Font font7 = new Font(FontFamily.GenericSansSerif, 8);
        //protected Color borderColor = Color.White;
        protected Color fontcolor = Color.White;
        protected Color stringColor = Color.Green;
       
        [Category("lED")]
        [Description("线条颜色")]
        [RefreshProperties(RefreshProperties.All)]
        public override Color BorderColor
        {
            get
            {
                return borderColor;
            }
            set
            {
                borderColor = value;
                OnAppearanceChanged(new EventArgs());
            }
        }

        [Category("lED")]
        [Description("字体颜色")]
        [RefreshProperties(RefreshProperties.All)]
        public Color StringColor
        {
            get
            {
                return stringColor;
            }
            set
            {
                stringColor = value;
                OnAppearanceChanged(new EventArgs());
            }
        }
        //[Category("lED")]
        //[Description("字体")]
        //[RefreshProperties(RefreshProperties.All)]
        //public virtual Font 字体
        //{
        //    get
        //    {
        //        return font;
        //    }
        //    set
        //    {
        //        font = value;
        //        OnAppearanceChanged(new EventArgs());

        //    }
        //}
        [Category("lED")]
        [Description("第一列字体")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual Font 第一列字体
        {
            get
            {
                return font1;
            }
            set
            {
                font1 = value;
                OnAppearanceChanged(new EventArgs());

            }
        }
        [Category("lED")]
        [Description("第二列字体")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual Font 第二列字体
        {
            get
            {
                return font2;
            }
            set
            {
                font2 = value;
                OnAppearanceChanged(new EventArgs());

            }
        }
        [Category("lED")]
        [Description("第三列字体")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual Font 第三列字体
        {
            get
            {
                return font3;
            }
            set
            {
                font3 = value;
                OnAppearanceChanged(new EventArgs());

            }
        }
        [Category("lED")]
        [Description("第四列字体")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual Font 第四列字体
        {
            get
            {
                return font4;
            }
            set
            {
                font4 = value;
                OnAppearanceChanged(new EventArgs());

            }
        }
        [Category("lED")]
        [Description("第五列字体")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual Font 第五列字体
        {
            get
            {
                return font5;
            }
            set
            {
                font5 = value;
                OnAppearanceChanged(new EventArgs());

            }
        }
        [Category("lED")]
        [Description("第六列字体")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual Font 第六列字体
        {
            get
            {
                return font6;
            }
            set
            {
                font6 = value;
                OnAppearanceChanged(new EventArgs());

            }
        }
       
        [Category("LED")]
        [Description("线条宽度")]
        [DefaultValue(1)]
        public override int BorderWidth
        {
            get
            {
                return borderWidth;
            }
            set
            {
                borderWidth = value;
                OnAppearanceChanged(new EventArgs());
            }
        }

        public static string guname
        {
            get
            {
                return guname1;
            }
            set
            {
                guname1 = value;
                
            }
        }

        public static string proname
        {
            get
            {
                return proname1;
            }
            set
            {
                proname1 = value;
            }
        }

        public static string proname0
        {
            get
            {
                return proname00;
            }
            set
            {
                proname00 = value;
            }
        }
        public WorkOrder()
            : this(0, 0, 100, 100)
        { }

        public WorkOrder(Rectangle rec)
            : this(rec.Location, rec.Size)
        { elementMonitoredType = MonitoredType.生产计划; }

        public WorkOrder(Point l, Size s)
            : this(l.X, l.Y, s.Width, s.Height)
        { }

        public WorkOrder(int top, int left, int width, int height)
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
            StringFormat sf1 = new StringFormat();
            sf1.LineAlignment = StringAlignment.Center;
            sf1.Alignment = StringAlignment.Near;
            g.DrawLine(new Pen(borderColor, borderWidth), location.X, location.Y, location.X + (int)(size.Width), location.Y);
            g.DrawLine(new Pen(borderColor, borderWidth), location.X, location.Y + (int)(((size.Height) * (float)(1.0 / 6))), location.X + size.Width, location.Y + (int)((size.Height) * (float)(1.0 / 6)));
            g.DrawLine(new Pen(borderColor, borderWidth), location.X, location.Y + (int)((size.Height) * (float)(2.0 / 6)), location.X + size.Width, location.Y + (int)((size.Height) * (float)(2.0 / 6)));
            g.DrawLine(new Pen(borderColor, borderWidth), location.X, location.Y + (int)((size.Height) * (float)(3.0 / 6)), location.X + size.Width, location.Y + (int)((size.Height) * (float)(3.0 / 6)));
            g.DrawLine(new Pen(borderColor, borderWidth), location.X, location.Y + (int)((size.Height) * (float)(4.0 / 6)), location.X + size.Width, location.Y + (int)((size.Height) * (float)(4.0 / 6)));
            g.DrawLine(new Pen(borderColor, borderWidth), location.X, location.Y + (int)((size.Height) * (float)(5.0 / 6)), location.X + size.Width, location.Y + (int)((size.Height) * (float)(5.0 / 6)));
            g.DrawLine(new Pen(borderColor, borderWidth), location.X, location.Y + (int)(size.Height), location.X + size.Width, location.Y + (int)(size.Height));
            g.DrawLine(new Pen(borderColor, borderWidth), location.X, location.Y, location.X, location.Y + (int)(size.Height));
            g.DrawLine(new Pen(borderColor, borderWidth), location.X, location.Y + (int)(size.Height), location.X + (int)(size.Width), location.Y + (int)(size.Height));
            //g.DrawLine(new Pen(borderColor, borderWidth), location.X + (int)(size.Width * (float)(2.0 / 14)), location.Y, location.X + (int)(size.Width * (float)(2.0 / 14)), location.Y + size.Height);
            g.DrawLine(new Pen(borderColor, borderWidth), location.X + (int)(size.Width * (float)(6.0 / 14)), location.Y, location.X + (int)(size.Width * (float)(6.0 / 14)), location.Y + size.Height);
            g.DrawLine(new Pen(borderColor, borderWidth), location.X + (int)(size.Width * (float)(10.0 / 14)), location.Y, location.X + (int)(size.Width * (float)(10.0 / 14)), location.Y + size.Height);
            g.DrawLine(new Pen(borderColor, borderWidth), location.X + (int)(size.Width * (float)(11.0 / 14)), location.Y, location.X + (int)(size.Width * (float)(11.0 / 14)), location.Y + size.Height);
            g.DrawLine(new Pen(borderColor, borderWidth), location.X + (int)(size.Width * (float)(12.0 / 14)), location.Y, location.X + (int)(size.Width * (float)(12.0 / 14)), location.Y + size.Height);
            g.DrawLine(new Pen(borderColor, borderWidth), location.X + (int)(size.Width * (float)(13.0 / 14)), location.Y, location.X + (int)(size.Width * (float)(13.0 / 14)), location.Y + size.Height);
            g.DrawLine(new Pen(borderColor, borderWidth), location.X + size.Width, location.Y, location.X + size.Width, location.Y + size.Height);

            try
            {
                DataTable dt = new DataTable();
                string sql = "select distinct pro_name,p_line_name,material_name,material_code,order_NO,work_order_type,work_order_NO from PLAN_DETAILS_PRO a where (pro_name='" + proname + "') and p_line_name='" + guname1 + "' and schedule_start_time<'" + DateTime.Now.Date.AddDays(1) + "' and (real_complete_time is null or real_complete_time>'" + DateTime.Now.Date + "')";
                dt = data.DBQuery.OpenTable1(sql);
                if (dt.Rows.Count > 0)
                {
                    arr = null;
                    arr = new string[dt.Rows.Count, 6];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        arr[i, 0] = dt.Rows[i][2].ToString().Trim();
                        arr[i, 1] = dt.Rows[i][0].ToString().Trim();
                        //arr[i, 3] = dt.Rows[i][3].ToString().Trim();
                        string sql01 = "select today_complete_num,today_plan_num from PLAN_LED where order_NO='" + dt.Rows[i][4].ToString() + "' and pro_name='" + dt.Rows[i][0].ToString() + "' and p_line_name='" + dt.Rows[i][1].ToString() + "' and date='" + DateTime.Now.Date + "'";
                        DataTable dt01 = data.DBQuery.OpenTable1(sql01);
                        if (dt01.Rows.Count > 0)
                        {
                            arr[i, 2] = dt01.Rows[0][0].ToString();
                            arr[i, 3] = dt01.Rows[0][1].ToString();
                        }
                        else
                        {
                            string htt = "select real_complete_time from PLAN_DETAILS_PRO where order_NO='" + dt.Rows[i][4].ToString() + "' and pro_name='" + dt.Rows[i][0].ToString() + "' and p_line_name='" + dt.Rows[i][1].ToString() + "' and schedule_start_time<'" + DateTime.Now.Date.AddDays(1) + "' and real_complete_time is null";
                            DataTable dth = data.DBQuery.OpenTable1(htt);
                            string htt1 = "select real_complete_time from PLAN_DETAILS_PRO where order_NO='" + dt.Rows[i][4].ToString() + "' and pro_name='" + dt.Rows[i][0].ToString() + "' and p_line_name='" + dt.Rows[i][1].ToString() + "' and schedule_start_time<'" + DateTime.Now.Date.AddDays(1) + "' and real_complete_time>'" + DateTime.Now.Date + "'";
                            DataTable dth1 = data.DBQuery.OpenTable1(htt1);
                            if (dth1.Rows.Count > 0)
                            {
                                arr[i, 2] = dth1.Rows.Count.ToString();
                            }
                            else
                                arr[i, 2] = "0";
                            arr[i, 3] = (dth.Rows.Count + dth1.Rows.Count).ToString();
                        }

                        float a = Convert.ToSingle(arr[i, 2].ToString());
                        float b = Convert.ToSingle(arr[i, 3].ToString());
                        float c = 0;
                        if (b > 0)
                        {
                            c = Convert.ToSingle(a / b) * 100;
                        }
                        string d = Convert.ToInt32(c) + "%";
                        arr[i, 4] = d.ToString().Trim();
                        string sqlf = "select schedule_start_time from PLAN_DETAILS_PRO where order_NO='" + dt.Rows[i][4].ToString() + "' and pro_name='" + dt.Rows[i][0].ToString() + "' and p_line_name='" + dt.Rows[i][1].ToString() + "' and schedule_start_time<'" + DateTime.Now + "' and real_start_time is null";
                        DataTable dtf = data.DBQuery.OpenTable1(sqlf);
                        string sql3 = "select m_delay_name from MATERIAL_DELAY_PRO where pro_name='" + dt.Rows[i][0].ToString() + "' and work_order_type='" + dt.Rows[i][5].ToString() + "' and work_order_NO='" + dt.Rows[i][6].ToString() + "' and p_line_name='" + dt.Rows[i][1].ToString() + "'";
                        DataTable dt3 = data.DBQuery.OpenTable1(sql3);
                        if (dtf.Rows.Count > 0)
                        {

                            if (dt3.Rows.Count > 0)
                            {
                                arr[i, 5] = "N";
                            }

                            else
                                arr[i, 5] = "D";
                        }
                        else
                        {
                            if (dt3.Rows.Count > 0)
                            {
                                arr[i, 5] = "N";
                            }

                            else
                                arr[i, 5] = "Y";
                        }
                    }
                    if ((dt.Rows.Count) > 6)
                    {

                        int drawRowsCount = dt.Rows.Count - StartRow;
                        if ((dt.Rows.Count - StartRow) > 6)
                        {
                            drawRowsCount = 6;
                        }
                        for (int x = 0; x < drawRowsCount; x++)
                        {
                            if (arr[x + StartRow, 5].ToString() == "N")
                            {
                                g.DrawString(arr[x + StartRow, 0].ToString().Trim(), font1, new SolidBrush(Color.Red), new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(6.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf1);
                                g.DrawString(arr[x + StartRow, 1].ToString().Trim(), font2, new SolidBrush(Color.Red), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(6.0 / 14)), location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(4.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf1);
                                g.DrawString(arr[x + StartRow, 2].ToString().Trim(), font3, new SolidBrush(Color.Red), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(10.0 / 14)), location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(1.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf);
                                g.DrawString(arr[x + StartRow, 3].ToString().Trim(), font4, new SolidBrush(Color.Red), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(11.0 / 14)), location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(1.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf);
                                g.DrawString(arr[x + StartRow, 4].ToString().Trim(), font5, new SolidBrush(Color.Red), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(12.0 / 14)), location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(1.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf);
                                g.DrawString(arr[x + StartRow, 5].ToString().Trim(), font6, new SolidBrush(Color.Red), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(13.0 / 14)), location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(1.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf);
                            }
                            else if (arr[x + StartRow, 5].ToString() == "D")
                            {
                                g.DrawString(arr[x + StartRow, 0].ToString().Trim(), font1, new SolidBrush(Color.Gold ), new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(6.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf1);
                                g.DrawString(arr[x + StartRow, 1].ToString().Trim(), font2, new SolidBrush(Color.Gold), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(6.0 / 14)), location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(4.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf1);
                                g.DrawString(arr[x + StartRow, 2].ToString().Trim(), font3, new SolidBrush(Color.Gold), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(10.0 / 14)), location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(1.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf);
                                g.DrawString(arr[x + StartRow, 3].ToString().Trim(), font4, new SolidBrush(Color.Gold), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(11.0 / 14)), location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(1.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf);
                                g.DrawString(arr[x + StartRow, 4].ToString().Trim(), font5, new SolidBrush(Color.Gold), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(12.0 / 14)), location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(1.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf);
                                g.DrawString(arr[x + StartRow, 5].ToString().Trim(), font6, new SolidBrush(Color.Gold), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(13.0 / 14)), location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(1.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf);
                            }
                            else
                            {
                                g.DrawString(arr[x + StartRow, 0].ToString().Trim(), font1, new SolidBrush(stringColor), new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(6.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf1);
                                g.DrawString(arr[x + StartRow, 1].ToString().Trim(), font2, new SolidBrush(stringColor), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(6.0 / 14)), location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(4.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf1);
                                g.DrawString(arr[x + StartRow, 2].ToString().Trim(), font3, new SolidBrush(stringColor), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(10.0 / 14)), location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(1.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf);
                                g.DrawString(arr[x + StartRow, 3].ToString().Trim(), font4, new SolidBrush(stringColor), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(11.0 / 14)), location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(1.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf);
                                g.DrawString(arr[x + StartRow, 4].ToString().Trim(), font5, new SolidBrush(stringColor), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(12.0 / 14)), location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(1.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf);
                                g.DrawString(arr[x + StartRow, 5].ToString().Trim(), font6, new SolidBrush(stringColor), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(13.0 / 14)), location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(1.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf);
                            }

                        }


                        if ((dt.Rows.Count - StartRow) > 6)
                        {
                            StartRow += drawRowsCount;
                        }
                        else
                        {
                            StartRow = 0;

                        }
                    }

                    else     //如果行数小于7，则直接执行显示
                    {
                        int drawRowsCount = dt.Rows.Count;
                        for (int x = 0; x < drawRowsCount; x++)
                        {
                            if (arr[x, 5].ToString() == "N")
                            {
                                g.DrawString(arr[x, 0].ToString().Trim(), font1, new SolidBrush(Color.Red), new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(6.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf1);
                                g.DrawString(arr[x, 1].ToString().Trim(), font2, new SolidBrush(Color.Red), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(6.0 / 14)), location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(4.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf1);
                                g.DrawString(arr[x, 2].ToString().Trim(), font3, new SolidBrush(Color.Red), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(10.0 / 14)), location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(1.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf);
                                g.DrawString(arr[x, 3].ToString().Trim(), font4, new SolidBrush(Color.Red), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(11.0 / 14)), location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(1.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf);
                                g.DrawString(arr[x, 4].ToString().Trim(), font5, new SolidBrush(Color.Red), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(12.0 / 14)), location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(1.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf);
                                g.DrawString(arr[x, 5].ToString().Trim(), font6, new SolidBrush(Color.Red), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(13.0 / 14)), location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(1.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf);
                            }
                            else if (arr[x, 5].ToString() == "D")
                            {
                                g.DrawString(arr[x, 0].ToString().Trim(), font1, new SolidBrush(Color.Gold), new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(6.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf1);
                                g.DrawString(arr[x, 1].ToString().Trim(), font2, new SolidBrush(Color.Gold), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(6.0 / 14)), location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(4.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf1);
                                g.DrawString(arr[x, 2].ToString().Trim(), font3, new SolidBrush(Color.Gold), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(10.0 / 14)), location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(1.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf);
                                g.DrawString(arr[x, 3].ToString().Trim(), font4, new SolidBrush(Color.Gold), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(11.0 / 14)), location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(1.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf);
                                g.DrawString(arr[x, 4].ToString().Trim(), font5, new SolidBrush(Color.Gold), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(12.0 / 14)), location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(1.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf);
                                g.DrawString(arr[x, 5].ToString().Trim(), font6, new SolidBrush(Color.Gold), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(13.0 / 14)), location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(1.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf);
                            }
                            else
                            {
                                g.DrawString(arr[x, 0].ToString().Trim(), font1, new SolidBrush(stringColor), new RectangleF(new System.Drawing.Point(location.X, location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(6.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf1);
                                g.DrawString(arr[x, 1].ToString().Trim(), font2, new SolidBrush(stringColor), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(6.0 / 14)), location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(4.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf1);
                                g.DrawString(arr[x, 2].ToString().Trim(), font3, new SolidBrush(stringColor), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(10.0 / 14)), location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(1.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf);
                                g.DrawString(arr[x, 3].ToString().Trim(), font4, new SolidBrush(stringColor), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(11.0 / 14)), location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(1.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf);
                                g.DrawString(arr[x, 4].ToString().Trim(), font5, new SolidBrush(stringColor), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(12.0 / 14)), location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(1.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf);
                                g.DrawString(arr[x, 5].ToString().Trim(), font6, new SolidBrush(stringColor), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(13.0 / 14)), location.Y + (int)(size.Height * x * (float)(1.0 / 6))), new Size((int)(size.Width * (float)(1.0 / 14)), (int)(size.Height * (float)(1.0 / 6)))), sf);
                            }

                        }

                    }

                }
            }
            catch (Exception ex)
            {
            }
        }




        IController IControllable.GetController()
        {
            if (controller == null)
                controller = new CommentBoxController(this);
            return controller;
        }

    }
}
