using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Dalssoft.DiagramNet;
using System.Drawing.Design;
using System.ComponentModel;

namespace Dalssoft.DiagramNet
{
    [Serializable]
    public class TextTime : BaseElement, IControllable, ILabelElement
    {
        [NonSerialized]
        private RectangleController controller;
        protected LabelElement label = new LabelElement();



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


        public TextTime()
            : this(0, 0, 100, 100)
        { }

        public TextTime(Rectangle rec)
            : this(rec.Location, rec.Size)
        { elementMonitoredType = MonitoredType.TextTime; }

        public TextTime(Point l, Size s)
            : this(l.X, l.Y, s.Width, s.Height)
        { }

        public TextTime(int top, int left, int width, int height)
            
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
            int s = DateTime.Now.Second;

            int h = DateTime.Now.Hour;

            int m = DateTime.Now.Minute;

            //s++;

            string time = String.Format("{0:00}:{1:00}:{2:00}", h, m, s);
            label.Text = time;
        }

        IController IControllable.GetController()
        {
            if (controller == null)
                controller = new CommentBoxController(this);
            return controller;
        }

    }
}

