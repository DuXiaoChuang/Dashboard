using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Drawing.Design;

namespace Dalssoft.DiagramNet
{
    [Serializable]
    public class RfidElement : BaseElement, IControllable, ILabelElement
    {
        [NonSerialized]
        private RectangleController controller;

        protected Image imageDefault = Diagram.NET.Resource.RfidOff;
        protected Image imageWorking = Diagram.NET.Resource.RfidOn;
        protected Image imageAlert = Diagram.NET.Resource.RfidOn;
        protected LabelElement label = new LabelElement();
        protected string nextNode = "";
        public string nextNodeObjectID = "";
        public string nextNodeObjectCode = "";
        public string nextNodeObjectName = "";
        protected MovementDirection elementDirection = MovementDirection.向右;

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

        //[CategoryAttribute("监控信息")]
        //[DescriptionAttribute("前节点")]
        //[DynamicProps.DynamicList("SQL")]
        //[TypeConverterAttribute(typeof(DynamicProps.NameConverter))]
        //[RefreshProperties(RefreshProperties.All)]
        //public virtual string 后节点
        //{
        //    get
        //    {
        //        return nextNode;
        //    }
        //    set
        //    {
        //        if (value != string.Empty)
        //        {
        //            nextNode = value;
        //            DynamicProps.ListAttribute attributes = new DynamicProps.ListAttribute(SQL);
        //            int index = attributes.codeNameCollection.IndexOf(this.后节点);
        //            nextNodeObjectID = attributes.idCollection[index].ToString();
        //            nextNodeObjectCode = attributes.codeCollection[index].ToString();
        //            nextNodeObjectName = attributes.nameCollection[index].ToString();
        //            TextAutoSize(label, this);
        //            OnAppearanceChanged(new EventArgs());
        //        }
        //    }
        //}

        [ReadOnly(false)]
        [DefaultValue(Dalssoft.DiagramNet.MovementDirection.向右)]
        [RefreshProperties(RefreshProperties.All)]
        public virtual Dalssoft.DiagramNet.MovementDirection 移动方向
        {
            get
            {
                return elementDirection;
            }
            set
            {
                elementDirection = value;

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

        [Category("外观")]
        [Description("图片")]
        [RefreshProperties(RefreshProperties.All)]
        [Editor(typeof(ImageEditer), typeof(UITypeEditor))]
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

        [Category("外观")]
        [Description("图片")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual Image 报警图片
        {
            get
            {
                return imageAlert;
            }
            set
            {
                imageAlert = value;
                OnAppearanceChanged(new EventArgs());
            }
        }

        [Category("外观")]
        [Description("图片")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual Image 正常工作图片
        {
            get
            {
                return imageWorking;
            }
            set
            {
                imageWorking = value;
                OnAppearanceChanged(new EventArgs());
            }
        }

        public RfidElement() : this(0, 0, 100, 100)
        { }

        public RfidElement(Rectangle rec) : this(rec.Location, rec.Size)
        { elementMonitoredType = MonitoredType.RFID; }

        public RfidElement(Point l, Size s) : this(l.X, l.Y, s.Width, s.Height)
        { }

        public RfidElement(int top, int left, int width, int height)
        {
            location = new Point(top, left);
            size = new Size(width, height);
        }

        internal override void Draw(Graphics g)
        {
            IsInvalidated = false;
            Image tmpImage = imageDefault;
            Rectangle r = GetUnsignedRectangle(
                new Rectangle(
                location.X, location.Y,
                size.Width, size.Height));


            #region 图片
            switch ((int)state)
            {
                case 0:
                    tmpImage = imageDefault;
                    break;
                case 1:
                    tmpImage = imageAlert;
                    break;
                case 2:
                    tmpImage = imageWorking;
                    break;
            }
            if (tmpImage != null)
            {
                g.DrawImage(tmpImage, r.Location.X, r.Location.Y, r.Size.Width, r.Size.Height);
            }

            #endregion

        }

        internal override void DrawAlert(Graphics g)
        {
            IsInvalidated = false;
            Image tmpImage = imageDefault;
            Rectangle r = GetUnsignedRectangle(
                new Rectangle(
                location.X, location.Y,
                size.Width, size.Height));


            #region 图片
            switch ((int)state)
            {
                case 0:
                    tmpImage = imageDefault;
                    break;
            }
            if (tmpImage != null)
            {
                g.DrawImage(tmpImage, r.Location.X, r.Location.Y, r.Size.Width, r.Size.Height);
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
