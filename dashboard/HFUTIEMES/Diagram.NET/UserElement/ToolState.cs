using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Collections.Generic;

namespace Dalssoft.DiagramNet
{
    [Serializable,
    TypeConverter(typeof(ExpandableObjectConverter))]
    public class ToolState : BaseElement, IControllable, ILabelElement
    {
        [NonSerialized]
        private RectangleController controller;
        protected LabelElement label = new LabelElement();
        private volatile bool IsTwinkle = false;
        protected Font font = new Font(FontFamily.GenericSansSerif, 10);
        public int statue;
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
        public ToolState()
            : this(0, 0, 100, 100)
        { }

        public ToolState(Rectangle rec)
            : this(rec.Location, rec.Size)
        {
            elementMonitoredType = MonitoredType.刀具状态;
        }

        public ToolState(Point l, Size s)
            : this(l.X, l.Y, s.Width, s.Height)
        { }

        public ToolState(int top, int left, int width, int height)
        {
            location = new Point(top, left);
            size = new Size(width, height);
        }

        internal override void Draw(Graphics g)
        {
            IsInvalidated = false;
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Near;
            Rectangle r = GetUnsignedRectangle(new Rectangle(location.X, location.Y, size.Width, size.Height));
            #region 根据刀具寿命比较运行状态着色//0-超期；1-预警；2-正常
            switch (statue)
            {
                case 0:
                    {
                        IsTwinkle = !IsTwinkle;
                        if (!IsTwinkle)
                            g.DrawRectangle(new Pen(Color.Gray, 4), r);
                        else
                            g.DrawRectangle(new Pen(Color.Red, 5), r);
                    }
                    break;
                case 1:
                    {
                        g.DrawRectangle(new Pen(Color.Orange , 5), r);
                    }
                    break;
                case 2:
                    {
                        g.DrawRectangle(new Pen(Color.Green, 5), r);
                    }
                    break;
            }
            #endregion
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
