using System;
using System.Collections.Generic;
using System.Text;
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

    public class WorkOrderTile : BaseElement, IControllable
    {
        [NonSerialized]
        private RectangleController controller;
        public string[,] arr;
        public int StartRow = 0;
        protected Font font = new Font(FontFamily.GenericSansSerif, 10);
        //protected Color borderColor = Color.White;
        protected Color fontcolor = Color.White;
        protected Color stringColor = Color.Black;
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

        [Category("lED")]
        [Description("字体")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual Font 字体
        {
            get
            {
                return font;
            }
            set
            {
                font = value;
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
        public WorkOrderTile()
            : this(0, 0, 100, 100)
        { }

        public WorkOrderTile(Rectangle rec)
            : this(rec.Location, rec.Size)
        { elementMonitoredType = MonitoredType.生产计划表头; }

        public WorkOrderTile(Point l, Size s)
            : this(l.X, l.Y, s.Width, s.Height)
        { }

        public WorkOrderTile(int top, int left, int width, int height)
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
            g.DrawLine(new Pen(borderColor, borderWidth), location.X, location.Y, location.X + (int)(size.Width), location.Y);
            g.DrawLine(new Pen(borderColor, borderWidth), location.X, location.Y + (int)(size.Height), location.X + size.Width, location.Y + (int)(size.Height));
            g.DrawLine(new Pen(borderColor, borderWidth), location.X, location.Y, location.X, location.Y + (int)(size.Height));
            //g.DrawLine(new Pen(borderColor, borderWidth), location.X + (int)(size.Width * (float)(2.0 / 14)), location.Y, location.X + (int)(size.Width * (float)(2.0 / 14)), location.Y + size.Height);
            g.DrawLine(new Pen(borderColor, borderWidth), location.X + (int)(size.Width * (float)(6.0 / 14)), location.Y, location.X + (int)(size.Width * (float)(6.0 / 14)), location.Y + size.Height);
            g.DrawLine(new Pen(borderColor, borderWidth), location.X + (int)(size.Width * (float)(10.0 / 14)), location.Y, location.X + (int)(size.Width * (float)(10.0 / 14)), location.Y + size.Height);
            g.DrawLine(new Pen(borderColor, borderWidth), location.X + (int)(size.Width * (float)(11.0 / 14)), location.Y, location.X + (int)(size.Width * (float)(11.0 / 14)), location.Y + size.Height);
            g.DrawLine(new Pen(borderColor, borderWidth), location.X + (int)(size.Width * (float)(12.0 / 14)), location.Y, location.X + (int)(size.Width * (float)(12.0 / 14)), location.Y + size.Height);
            g.DrawLine(new Pen(borderColor, borderWidth), location.X + (int)(size.Width * (float)(13.0 / 14)), location.Y, location.X + (int)(size.Width * (float)(13.0 / 14)), location.Y + size.Height);
            g.DrawLine(new Pen(borderColor, borderWidth), location.X + size.Width, location.Y, location.X + size.Width, location.Y + size.Height);




            //g.DrawString("股别", font, new SolidBrush(stringColor), new RectangleF(new System.Drawing.Point(location.X, location.Y), new Size((int)(size.Width * (float)(2.0 / 14)), (int)(size.Height))), sf);

            g.DrawString("机型+名称", font, new SolidBrush(stringColor), new RectangleF(new System.Drawing.Point(location.X , location.Y), new Size((int)(size.Width * (float)(6.0 / 14)), (int)(size.Height))), sf);

            g.DrawString("工序名称", font, new SolidBrush(stringColor), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(6.0 / 14)), location.Y), new Size((int)(size.Width * (float)(4.0 / 14)), (int)(size.Height))), sf);

            //g.DrawString("计划数量", font, new SolidBrush(Color.Red), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(6.0 / 11)), location.Y), new Size((int)(size.Width * (float)(1.0 / 11)), (int)(size.Height))), sf);

            g.DrawString("实际", font, new SolidBrush(stringColor), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(10.0 / 14)), location.Y), new Size((int)(size.Width * (float)(1.0 / 14)), (int)(size.Height))), sf);

            g.DrawString("计划", font, new SolidBrush(stringColor), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(11.0 / 14)), location.Y), new Size((int)(size.Width * (float)(1.0 / 14)), (int)(size.Height))), sf);

            g.DrawString("CR", font, new SolidBrush(stringColor), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(12.0 / 14)), location.Y), new Size((int)(size.Width * (float)(1.0 / 14)), (int)(size.Height))), sf);

            g.DrawString("状态", font, new SolidBrush(stringColor), new RectangleF(new System.Drawing.Point(location.X + (int)(size.Width * (float)(13.0 / 14)), location.Y), new Size((int)(size.Width * (float)(1.0 / 14)), (int)(size.Height))), sf);

        }

        IController IControllable.GetController()
        {
            if (controller == null)
                controller = new CommentBoxController(this);
            return controller;
        }

    }
}
