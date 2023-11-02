using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing.Text;


namespace Dalssoft.DiagramNet
{
    [
     Serializable,
     TypeConverter(typeof(ExpandableObjectConverter))
     ]
    public class StraightLine : BaseElement, IControllable
    {
        [NonSerialized]
        private RectangleController controller;
        private direction Direction = direction.上下;
        protected Color linecolor = Color.Red;


        [Category("画线")]
        [Description("颜色")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual Color Linecolor
        {
            get
            {
                return linecolor;
            }
            set
            {
                linecolor = value;
                OnAppearanceChanged(new EventArgs());
            }
        }

        [Category("画线")]
        [Description("线宽")]
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
        [Category("画线")]
        [Description("方向")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual direction 方向
        {
            get
            {
                return Direction;
            }
            set
            {
                Direction = value;
            }

        }







        public StraightLine()
            : this(0, 0, 100, 100)
        { }

        public StraightLine(Rectangle rec)
            : this(rec.Location, rec.Size)
        { elementMonitoredType = MonitoredType.StraightLine; }

        public StraightLine(Point l, Size s)
            : this(l.X, l.Y, s.Width, s.Height)
        { }

        public StraightLine(int top, int left, int width, int height)
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

            if (Direction == direction.上下 || Direction == direction.下上)
            {
                g.DrawLine(new Pen(linecolor, borderWidth), location.X + size.Width / 2, location.Y, location.X + size.Width / 2, location.Y + size.Height);
            }
            else if (Direction == direction.左右 || Direction == direction.右左)
            {
                g.DrawLine(new Pen(linecolor, borderWidth), location.X, location.Y + size.Height / 2, location.X + (int)(size.Width), location.Y + size.Height / 2);

            }


        }



        IController IControllable.GetController()
        {
            if (controller == null)
                controller = new RectangleController(this);
            return controller;
        }
    }
}
