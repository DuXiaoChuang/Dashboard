using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Dalssoft.DiagramNet;
using System.Drawing.Design;
using System.ComponentModel;

namespace Dalssoft.DiagramNet
{
    [Serializable]
    public class xiaoxi : BaseElement, IControllable, ILabelElement
    {
        [NonSerialized]
        private RectangleController controller;
        protected LabelElement label = new LabelElement();
        private static string guname1 = "";

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
        public xiaoxi()
            : this(0, 0, 100, 100)
        { }

        public xiaoxi(Rectangle rec)
            : this(rec.Location, rec.Size)
        { elementMonitoredType = MonitoredType.LED股名; }

        public xiaoxi(Point l, Size s)
            : this(l.X, l.Y, s.Width, s.Height)
        { }

        public xiaoxi(int top, int left, int width, int height)
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
            label.Text = guname;
        }

        IController IControllable.GetController()
        {
            if (controller == null)
                controller = new CommentBoxController(this);
            return controller;
        }

    }
}
