using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Collections.Generic;

namespace Dalssoft.DiagramNet
{
    [Serializable]
    public class AddressElement : BaseElement, IControllable, ILabelElement
    {
        [NonSerialized]
        private RectangleController controller;

        protected Image imageDefault = Diagram.NET.Resource.AndonOff;
        protected Image imageWorking = Diagram.NET.Resource.AndonOn;
        protected Image imageAlert = Diagram.NET.Resource.AndonOn;
        protected LabelElement label = new LabelElement();


        [Category("监控信息")]
        [Description("监控对象")]
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


        public AddressElement(): this(0, 0, 100, 100)
        { }

        public AddressElement(Rectangle rec): this(rec.Location, rec.Size)
        { elementMonitoredType = MonitoredType.控制地址; }

        public AddressElement(Point l, Size s): this(l.X, l.Y, s.Width, s.Height)
        { }

        public AddressElement(int top, int left, int width, int height)
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
                    tmpImage = imageAlert;//报警也默认为off
                    break;
                case 2:
                    tmpImage = imageWorking;//工作即点亮
                    break;
            }
            if (tmpImage != null)
            {
                g.DrawImage(tmpImage, r.Location.X, r.Location.Y, r.Size.Width, r.Size.Height);
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
