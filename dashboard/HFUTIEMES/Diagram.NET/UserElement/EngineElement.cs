using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace Dalssoft.DiagramNet
{
    [Serializable]
    public class EngineElement : BaseElement, IControllable, ILabelElement
    {
        [NonSerialized]
        private RectangleController controller;

        protected Image image = Diagram.NET.Resource.EnginePNG;
        protected LabelElement label = new LabelElement();
        protected volatile int goalx, goaly = 0;//目标坐标
        protected MovementDirection elementDirection = MovementDirection.向右;
        protected string nextNode = "";

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
                    //DynamicProps.ListAttribute attributes = new DynamicProps.ListAttribute(SQL);
                    //int index = attributes.codeNameCollection.IndexOf(this.监控对象);
                    //monitoredObjectID = attributes.idCollection[index].ToString();
                    //monitoredObjectCode = attributes.codeCollection[index].ToString();
                    //monitoredObjectName = attributes.nameCollection[index].ToString();
                    //TextAutoSize(label, this);
                    OnAppearanceChanged(new EventArgs());
                }
            }
        }

        [CategoryAttribute("监控信息")]
        [DescriptionAttribute("所在节点")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual string 所在节点
        {
            get
            {
                return nextNode;
            }
            set
            {
                if (value != string.Empty)
                {
                    nextNode = value;
                    OnAppearanceChanged(new EventArgs());
                }
            }
        }

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


        [ReadOnly(false)]
        [Browsable(false)]
        public virtual int XLimit
        {
            get
            {
                return goalx;
            }
            set
            {
                goalx = value;
                OnAppearanceChanged(new EventArgs());
            }
        }

        [ReadOnly(false)]
        [Browsable(false)]
        public virtual int YLimit
        {
            get
            {
                return goaly;
            }
            set
            {
                goaly = value;
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
        public virtual Image 图片
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                OnAppearanceChanged(new EventArgs());
            }
        }

        public EngineElement(): this(0, 0, 100, 100)
        {
            goalx = 1024;
        }

        public EngineElement(Rectangle rec): this(rec.Location, rec.Size)
        { 
            elementMonitoredType = MonitoredType.发动机;
        }

        public EngineElement(Point l, Size s): this(l.X, l.Y, s.Width, s.Height)
        { }

        public EngineElement(int top, int left, int width, int height)
        {
            location = new Point(top, left);
            size = new Size(width, height);
        }

        internal override void Draw(Graphics g)
        {
            IsInvalidated = false;
            Image tmpImage = image;
            Rectangle r = GetUnsignedRectangle(new Rectangle(location.X, location.Y,size.Width, size.Height));
            g.DrawImage(tmpImage, r);


            switch ((int)state)
            {
                case 0:
                    {
                        MoveEngine(tmpImage, g, r);
                        //Brush brush = Brushes.Transparent;
                        //g.FillRectangle(brush, r);
                    }
                    break;
                case 1:
                    {
                        Brush brush = Brushes.Transparent;
                        g.FillRectangle(brush, r);
                    }
                    break;
                case 2:
                    {
                        MoveEngine(tmpImage, g, r);
                    }
                    break;
            }
        }

        private void MoveEngine(Image tmpImage, Graphics g, Rectangle r)
        {
            if (tmpImage != null)
            {

                if (移动方向 == MovementDirection.向右)
                {
                    if (Location.X < goalx)
                    {
                        location.X += 5;
                    }
                }
                else if (移动方向 == MovementDirection.向左)
                {
                    if (location.X > goalx)
                    {
                        location.X -= 5;
                    }
                }
                else if (移动方向 == MovementDirection.向下)
                {
                    if (location.Y < goaly)
                    {
                        location.Y += 5;
                    }
                }
                else if (移动方向 == MovementDirection.向上)
                {
                    if (location.Y > goaly)
                    {
                        location.Y -= 3;
                    }
                }
            }
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
