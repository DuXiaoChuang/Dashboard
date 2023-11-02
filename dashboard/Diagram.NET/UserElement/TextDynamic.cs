using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Dalssoft.DiagramNet;
using System.Drawing.Design;
using System.ComponentModel;
using System.Drawing.Text;

namespace Dalssoft.DiagramNet
{
    [Serializable]
    public class TextDynamic : BaseElement, IControllable
	{
		[NonSerialized]
		private RectangleController controller;
       
        
        protected Font font = new Font(FontFamily.GenericSansSerif, 10);
        private movedirection Movedirection = movedirection.向右;
        private string text = "";
        volatile int x, y = 0;//坐标


        [Category("外观")]
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

        [Category("外观")]
        [Description("移动方向")]
        [RefreshProperties(RefreshProperties.All)]
         public virtual movedirection 方向
        {
            get
            {
                return Movedirection;
            }
            set
            {
                Movedirection = value;
            }

        }


        [Category("外观")]
        [Description("文字")]
        [RefreshProperties(RefreshProperties.All)]
        public  string Text 
        {
            get
            {
                return text;
            }
            set
            {
                text= value;
            }

        }

       

		public TextDynamic(): this(0, 0, 100, 100)
		{}

		public TextDynamic(Rectangle rec): this(rec.Location, rec.Size)
        { elementMonitoredType = MonitoredType.TextDynamic; }

		public TextDynamic(Point l, Size s): this(l.X, l.Y, s.Width, s.Height) 
		{}

        public TextDynamic(int top, int left, int width, int height)
            : base(top, left, width, height)
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

            if (Size.Width >= Size.Height)//表示长度大于高度，应该移动横坐标
            {
                if (方向 == movedirection.向右)
                {
                    x += 3;
                    if ((x + (int)(size.Height * 1.3)) > (location.X + size.Width))
                    {
                        x = location.X;
                    }
                    Point p = new Point(x, location.Y);
                    Size s = new Size((int)(size.Height * 1.3), size.Height);
                    RectangleF r1 =new RectangleF (p,s);
                  
                    g.DrawString(text, font, new SolidBrush(Color.Red), (RectangleF) r1,sf
);
                }
                else
                {
                    x -= 3;
                    if (x < location.X)
                    {
                        x = location.X + size.Width;
                        
                    }
                    Point p = new Point(x, location.Y);
                    Size s = new Size((int)(size.Height * 1.3), size.Height);
                    RectangleF r1 = new RectangleF(p, s);
                    g.DrawString(text, font, new SolidBrush(Color.Red), (RectangleF)r1,sf);
                }
            }
            else
            {
                if (方向 == movedirection.向左)
                {
                    y += 3;
                    if ((y + (int)(size.Width * 0.77)) > (location.Y + size.Height))
                    {
                        y = location.Y;
                    }
                    Point p = new Point(location.X, y);
                    Size s = new Size(size.Width,(int)(size.Width * 0.77));
                    RectangleF r2 = new RectangleF(p, s);
                  
                    g.DrawString(text, font, new SolidBrush(Color.Red), (RectangleF)r2,sf);
                }
                else
                {
                    y -= 3;
                    if (y < location.Y)
                    {
                        y = location.Y + size.Height;
                    }
                    Point p = new Point(location.X, y);
                    Size s = new Size(size.Width, (int)(size.Width * 0.77));
                    RectangleF r2 = new RectangleF(p, s);
                 
                    g.DrawString(text, font, new SolidBrush(Color.Red), (RectangleF)r2,sf);
                }
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
