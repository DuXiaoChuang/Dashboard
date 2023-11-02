using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Dalssoft.DiagramNet;
using System.Drawing.Design;
using System.ComponentModel;

namespace Dalssoft.DiagramNet
{
	[Serializable,
    TypeConverter(typeof(ExpandableObjectConverter))]
	public class CommentBoxElement: RectangleElement, IControllable, ILabelElement
	{
		[NonSerialized]
		private RectangleController controller;
        private int mCornerRadius = 0;
		protected Size foldSize = new Size(10, 15);

		public CommentBoxElement(): this(0, 0, 100, 100)
		{}

		public CommentBoxElement(Rectangle rec): this(rec.Location, rec.Size)
        { elementMonitoredType = MonitoredType.ÏûÏ¢¿ò; }

		public CommentBoxElement(Point l, Size s): this(l.X, l.Y, s.Width, s.Height) 
		{}

		public CommentBoxElement(int top, int left, int width, int height): base(top, left, width, height)
		{
            fillColor1 = Color.Transparent;
            fillColor2 = Color.Transparent;

            label.Opacity = 100;
		}

		public override Point Location
		{
			get
			{
				return base.Location;
			}
			set
			{
				base.Location = value;
			}
		}


		public override Size Size
		{
			get
			{
				return base.Size;
			}
			set
			{
				base.Size = value;
			}
		}

        [Category("Íâ¹Û")]
        [DefaultValue(0)]
        [Description("Ô²½Ç")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual int Ô²½Ç
        {
            get
            {
                return mCornerRadius;
            }
            set
            {
                if (value > Math.Min(this.Size.Height / 2, this.Size.Width / 2))
                {
                    value = Math.Min(this.Size.Height / 2, this.Size.Width / 2);
                }

                mCornerRadius = value;
                OnAppearanceChanged(new EventArgs());
            }
        }

        private GraphicsPath RoundRect(RectangleF r, float r1, float r2, float r3, float r4)
        {
            float x = r.X, y = r.Y, w = r.Width, h = r.Height;
            GraphicsPath rr = new GraphicsPath();
            rr.AddBezier(x, y + r1, x, y, x + r1, y, x + r1, y);
            rr.AddLine(x + r1, y, x + w - r2, y);
            rr.AddBezier(x + w - r2, y, x + w, y, x + w, y + r2, x + w, y + r2);
            rr.AddLine(x + w, y + r2, x + w, y + h - r3);
            rr.AddBezier(x + w, y + h - r3, x + w, y + h, x + w - r3, y + h, x + w - r3, y + h);
            rr.AddLine(x + w - r3, y + h, x + r4, y + h);
            rr.AddBezier(x + r4, y + h, x, y + h, x, y + h - r4, x, y + h - r4);
            rr.AddLine(x, y + h - r4, x, y + r1);
            return rr;
        }

        protected void DrawBorder(Graphics g, GraphicsPath r)
        {
            Pen p = new Pen(borderColor, borderWidth);
            g.DrawPath(p, r);
            p.Dispose();
        }

		internal override void Draw(Graphics g)
		{
			IsInvalidated = false;
			Rectangle r = BaseElement.GetUnsignedRectangle(new Rectangle(location, size));
            GraphicsPath rr = RoundRect(r, this.Ô²½Ç, this.Ô²½Ç, this.Ô²½Ç, this.Ô²½Ç);
            Brush b = GetBrush(r);
            g.FillPath(b, rr);
            DrawBorder(g, rr);
            b.Dispose();
		}

		IController IControllable.GetController()
		{
			if (controller == null)
				controller = new CommentBoxController(this);
			return controller;
		}

	}
}
