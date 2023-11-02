using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Dalssoft.DiagramNet;
using System.Drawing.Design;
using System.ComponentModel;

namespace Dalssoft.DiagramNet
{
    [Serializable]
    public class TextWeek : BaseElement, IControllable, ILabelElement
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

		public TextWeek(): this(0, 0, 100, 100)
		{}

		public TextWeek(Rectangle rec): this(rec.Location, rec.Size)
        { elementMonitoredType = MonitoredType.星期; }

		public TextWeek(Point l, Size s): this(l.X, l.Y, s.Width, s.Height) 
		{}

        public TextWeek(int top, int left, int width, int height)
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
            label.Text = DateTime.Today.ToShortDateString();
        
            switch (DateTime.Today.DayOfWeek.ToString())
            {
                case "Monday":
                    label.Text = "星期一";
                    break;
                case "Tuesday":
                    label.Text = "星期二";
                    break;
                case "Wednesday":
                    label.Text = "星期三";
                    break;
                case "Thursday":
                    label.Text = "星期四";
                    break;
                case "Friday":
                    label.Text = "星期五";
                    break;
                case "Saturday":
                    label.Text = "星期六";
                    break;
                case "Sunday":
                    label.Text = "星期日";
                    break;
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
