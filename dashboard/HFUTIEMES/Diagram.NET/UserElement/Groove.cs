using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Collections.Generic;

namespace Dalssoft.DiagramNet
{
    [Serializable,
    TypeConverter(typeof(ExpandableObjectConverter))]
    public class Groove : BaseElement, IControllable, ILabelElement
    {
        [NonSerialized]
        private RectangleController controller;
        public  string groove="";//刀槽号
        public string toolcode = "";//刀具编号
        public Color backcolor =Color .Transparent;//背景色
        protected Image imageDefault = Diagram.NET.Resource.tool ;
        protected LabelElement label = new LabelElement();
        protected Font font = new Font(FontFamily.GenericSansSerif, 10);
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

        [CategoryAttribute("监控信息")]
        [DescriptionAttribute("刀槽")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual string 刀槽
        {
            get
            {
                return groove;
            }
            set
            {
                groove = value;
                OnAppearanceChanged(new EventArgs());
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

        [Category("外观")]
        [Description("图片")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual Image 默认图片
        {
            get
            {
                return imageDefault;
            }
            set
            {
                imageDefault = value;
                OnAppearanceChanged(new EventArgs());
            }
        }

        public Groove()
            : this(0, 0, 100, 100)
        { }

        public Groove(Rectangle rec)
            : this(rec.Location, rec.Size)
        {
            elementMonitoredType = MonitoredType.刀槽 ;
        }

        public Groove(Point l, Size s)
            : this(l.X, l.Y, s.Width, s.Height)
        { }

        public Groove(int top, int left, int width, int height)
        {
            location = new Point(top, left);
            size = new Size(width, height);
        }

        internal override void Draw(Graphics g)
        {
            IsInvalidated = false;
             StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            Rectangle r = GetUnsignedRectangle( new Rectangle(location.X, location.Y,size.Width, size.Height));
            Rectangle r1 = GetUnsignedRectangle( new Rectangle(location.X, location.Y,(int)(size.Width/3), size.Height));
            Rectangle r2 = GetUnsignedRectangle(new Rectangle(location.X + (int)(size.Width / 3), location.Y, (int)(size.Width *(float )(2.0/ 3.0)), size.Height));
            Rectangle r3 = GetUnsignedRectangle(new Rectangle(location.X, location.Y - (int)(size.Height / 2), size.Width, (int)(size.Height / 2)));
            g.DrawRectangle(new Pen(Color.Gray, 2), r);
            g.FillRectangle(new SolidBrush (backcolor), r);
            g.DrawString(groove, font, new SolidBrush(Color.Black), r3, sf);
            g.DrawImage(imageDefault, r1);
            g.DrawString(toolcode , font, new SolidBrush(Color.Black), r2, sf);
                   
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
