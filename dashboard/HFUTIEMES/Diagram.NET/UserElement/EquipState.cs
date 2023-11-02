using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Collections.Generic;

namespace Dalssoft.DiagramNet
{
    [Serializable,
    TypeConverter(typeof(ExpandableObjectConverter))]
    public class EquipState : BaseElement, IControllable, ILabelElement
    {
        [NonSerialized]
        private RectangleController controller;
        protected LabelElement label = new LabelElement();
        private volatile bool IsTwinkle = false;
        protected Font font = new Font(FontFamily.GenericSansSerif, 11,FontStyle.Bold);
        public int statue;
        public string productcode = "";
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
                    TextAutoSize(label, this);
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
        public EquipState()
            : this(0, 0, 100, 100)
        { }

        public EquipState(Rectangle rec)
            : this(rec.Location, rec.Size)
        {
            elementMonitoredType = MonitoredType.设备状态;
        }

        public EquipState(Point l, Size s)
            : this(l.X, l.Y, s.Width, s.Height)
        { }

        public EquipState(int top, int left, int width, int height)
        {
            location = new Point(top, left);
            size = new Size(width, height);
        }

        internal override void Draw(Graphics g)
        {
            IsInvalidated = false;
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center  ;
            Rectangle r = GetUnsignedRectangle( new Rectangle(location.X, location.Y, size.Width, size.Height));
            Rectangle r1 = GetUnsignedRectangle( new Rectangle( location.X, (int)(location.Y-40), size.Width, 40));
            g.DrawString(productcode, font, new SolidBrush(borderColor),r1,sf );//显示当前加工产品信息
            #region 根据设备运行状态着色//0-关机；1-正常；2-故障；3-急停;4-其他状态
            switch (statue)
            {
                case 0:
                    {
                        g.DrawRectangle(new Pen(Color.Black, 5), r);
                    }
                    break;
                case 1:
                    {
                        g.DrawRectangle(new Pen(Color.Green , 5), r);
                    }
                    break;
                case 2:
                    {
                        IsTwinkle = !IsTwinkle;
                        if (!IsTwinkle)
                            g.DrawRectangle(new Pen(Color.Gray, 4), r);
                        else
                            g.DrawRectangle(new Pen(Color.Red, 5), r);
                    }
                    break;
                case 3:
                    {
                        g.DrawRectangle(new Pen(Color.DarkOrange, 5), r);
                    }
                    break;
                case 4:
                    {
                        g.DrawRectangle(new Pen(Color.Gray, 4), r);
                    }
                    break;
            }
            #endregion
        }

        public static void TextAutoSize(LabelElement lbl, BaseElement el)
        {
            if (lbl.Text == string.Empty) return;

            Bitmap bmp = new Bitmap(el.Size.Width, 1);
            Graphics g = Graphics.FromImage(bmp);
            Size sizeTmp = Size.Empty;

            sizeTmp = DiagramUtil.MeasureString(lbl.Text, lbl.Font, el.Size.Width, lbl.Format);
            sizeTmp.Width += 30;
            sizeTmp.Height += 30;


            lbl.Size = sizeTmp;

            lbl.PositionBySite(el);

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
