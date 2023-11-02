using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;

namespace Dalssoft.DiagramNet
{
    [Serializable]
    public class WorkCenter : BaseElement, IControllable
    {
        private direction Direction = direction.上下;
        [NonSerialized]
        private RectangleController controller;

        [Category("三角形")]
        [Description("箭头方向")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual direction 箭头方向
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

        [Category("三角形")]
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

        [Category("三角形")]
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

        public WorkCenter()
            : this(0, 0, 100, 100)
        { }

        public WorkCenter(Rectangle rec)
            : this(rec.Location, rec.Size)
        { elementMonitoredType = MonitoredType.工位; }

        public WorkCenter(Point l, Size s)
            : this(l.X, l.Y, s.Width, s.Height)
        { }

        public WorkCenter(int top, int left, int width, int height)
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

            if (Direction == direction.右左 )
            {
                g.DrawLine(new Pen(borderColor, borderWidth), location.X, location.Y + (int)(size.Height / 2), location.X + (int)(size.Width), location.Y);
                g.DrawLine(new Pen(borderColor, borderWidth), location.X, location.Y + (int)(size.Height / 2), location.X + (int)(size.Width), location.Y + (int)(size.Height));
                g.DrawLine(new Pen(borderColor, borderWidth), location.X + (int)(size.Width), location.Y, location.X + (int)(size.Width), location.Y + (int)(size.Height));
            }
            else if (Direction == direction.左右)
            {
                g.DrawLine(new Pen(borderColor,borderWidth),location.X,location.Y,location.X+(int)(size.Width),location.Y+(int)(size.Height/2));
                g.DrawLine(new Pen(borderColor, borderWidth), location.X, location.Y+(int)(size.Height), location.X + (int)(size.Width), location.Y + (int)(size.Height / 2));
                g.DrawLine(new Pen(BorderColor, borderWidth), location.X, location.Y, location.X, location.Y + (int)(size.Height));
            }
            else if (Direction == direction.上下)
            {
                g.DrawLine(new Pen(borderColor, borderWidth), location.X, location.Y, location.X + (int)(size.Width/2), location.Y + (int)(size.Height));
                g.DrawLine(new Pen(borderColor, borderWidth), location.X+(int)(size.Width), location.Y , location.X + (int)(size.Width/2), location.Y + (int)(size.Height));
                g.DrawLine(new Pen(BorderColor, borderWidth), location.X, location.Y, location.X+(int)(size.Width), location.Y);
            }
            else if (Direction == direction.下上)
            {
                g.DrawLine(new Pen(borderColor, borderWidth), location.X+(int)(size.Width/2), location.Y, location.X, location.Y + (int)(size.Height));
                g.DrawLine(new Pen(borderColor, borderWidth), location.X + (int)(size.Width/2), location.Y, location.X + (int)(size.Width), location.Y + (int)(size.Height));
                g.DrawLine(new Pen(BorderColor, borderWidth), location.X, location.Y + (int)(size.Height), location.X + (int)(size.Width), location.Y + (int)(size.Height));
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
