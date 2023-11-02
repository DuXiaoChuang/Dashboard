using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Dalssoft.DiagramNet;
using System.Drawing.Design;
using System.ComponentModel;
using System.Data;

namespace Dalssoft.DiagramNet
{
    [Serializable]
    public class OnlineNumber : BaseElement, IControllable, ILabelElement
    {
        [NonSerialized]
        private RectangleController controller;
        protected LabelElement label = new LabelElement();
        protected Statistics_type statisticstyle = Statistics_type.无;

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
        public virtual Statistics_type 计数类型
        {
            get
            {
                return statisticstyle;
            }
            set
            {
                statisticstyle = value;
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
                RectangleElement.TextAutoSize(label, this);
                OnAppearanceChanged(new EventArgs());
            }
        }


        [Category("外观")]
        [Description("label")]
        public virtual LabelElement Label
        {
            get
            {
                return label;
            }
            set
            {
                label = value;
                OnAppearanceChanged(new EventArgs());
            }
        }
        public OnlineNumber()
            : this(0, 0, 100, 100)
        { }

        public OnlineNumber(Rectangle rec)
            : this(rec.Location, rec.Size)
        {
            elementMonitoredType = MonitoredType.统计数量;
            borderColor = Color.Red;
        }

        public OnlineNumber(Point l, Size s)
            : this(l.X, l.Y, s.Width, s.Height)
        { }

        public OnlineNumber(int top, int left, int width, int height)
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
            DrawBorder(g, r);
            int a =0;
            string sql = "";
            if (MoniteredObjectID != "")
            {
                if (statisticstyle.ToString() == "无")
                    return;
                else if (statisticstyle.ToString() == "上线数量")
                    sql = "select ProductRouteID from Product_Online_P  where Starttime>'" + DateTime.Now.Date + "' and  reserve5='" + monitoredObjectID + "'";
                else if (statisticstyle.ToString() == "下线数量")
                    sql = "select ProductRouteID from Product_Online_P  where remark='是' and Endtime>'" + DateTime.Now.Date + "' and  reserve5='" + monitoredObjectID + "'";
                else if (statisticstyle.ToString() == "线上数量")
                    sql = "select ProductRouteID from Product_Online_P  where remark='否' and reserve5='" + monitoredObjectID + "'";
                DataTable dt = DbHelperSQL.OpenTable(sql);
                a = dt.Rows.Count;
            }
            else
            {
                if (statisticstyle.ToString() == "无")
                    return;
                else if (statisticstyle.ToString() == "上线数量")
                    sql = "select ProductRouteID from Product_Online_P  where Starttime>'" + DateTime.Now.Date + "'";
                else if (statisticstyle.ToString() == "下线数量")
                    sql = "select ProductRouteID from Product_Online_P  where remark='是' and Endtime>'" + DateTime.Now.Date + "'";
                else if (statisticstyle.ToString() == "线上数量")
                    sql = "select ProductRouteID from Product_Online_P  where remark='否'";
                DataTable dt = DbHelperSQL.OpenTable(sql);
                a = dt.Rows.Count;
            }
            label.Text = a.ToString ();
        }
        protected virtual void DrawBorder(Graphics g, Rectangle r)
        {
            //Border
            Pen p = new Pen(borderColor, borderWidth);
            g.DrawRectangle(p, r);
            p.Dispose();

        }
        IController IControllable.GetController()
        {
            if (controller == null)
                controller = new CommentBoxController(this);
            return controller;
        }

    }
}
