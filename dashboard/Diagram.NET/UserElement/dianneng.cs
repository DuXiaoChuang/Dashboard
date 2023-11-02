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
    public class dianneng : BaseElement, IControllable, ILabelElement
    {
        [NonSerialized]
        private RectangleController controller;
        protected LabelElement label = new LabelElement();
        protected Statistics_type statisticstyle = Statistics_type.无;
        [TypeConverterAttribute(typeof(DynamicProps.NameConverter))]
        [RefreshProperties(RefreshProperties.All)]
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
        public dianneng()
            : this(0, 0, 100, 100)
        { }
        public dianneng(Rectangle rec)
            : this(rec.Location, rec.Size)
        {
            elementMonitoredType = MonitoredType.湿度;
            borderColor = Color.Blue;
            borderWidth = 3;
        }
        public dianneng(Point l, Size s)
            : this(l.X, l.Y, s.Width, s.Height)
        { }
        public dianneng(int top, int left, int width, int height)
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
            Random ran = new Random();
            int a = ran.Next(20, 100);
            string b = a.ToString();
            label.Text = "" + b + "kwh";
        }
        protected virtual void DrawBorder(Graphics g, Rectangle r)
        {
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
