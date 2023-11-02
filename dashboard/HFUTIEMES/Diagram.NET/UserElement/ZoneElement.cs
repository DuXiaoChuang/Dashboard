using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Collections.Generic;

namespace Dalssoft.DiagramNet
{
    [Serializable]
    public class ZoneElement : BaseElement, IControllable, ILabelElement
    {
        [NonSerialized]
        private RectangleController controller;
        protected Color fillColor = Color.Red;
        protected Image image = null;
        protected LabelElement label = new LabelElement();
        private volatile bool IsTwinkle = false;

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
                    string text = "";
                    switch (Convert.ToInt32(textShownMode))
                    {
                        case 0:
                            text = "";
                            break;
                        case 1:
                            text = attributes.nameCollection[index].ToString();
                            break;
                        case 2:
                            text = attributes.codeCollection[index].ToString();
                            break;
                    }
                    label.Text = text;
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


        public virtual Color AlertColor
        {
            get
            {
                return fillColor;
            }
            set
            {
                fillColor = value;
                OnAppearanceChanged(new EventArgs());
            }
        }

        public ZoneElement(): this(0, 0, 100, 100)
        { }

        public ZoneElement(Rectangle rec): this(rec.Location, rec.Size)
        {
            elementMonitoredType = MonitoredType.区域;
            borderColor = Color.Red;
            BorderWidth = 3;
        }

        public ZoneElement(Point l, Size s): this(l.X, l.Y, s.Width, s.Height)
        { }

        public ZoneElement(int top, int left, int width, int height)
        {
            location = new Point(top, left);
            size = new Size(width, height);
        }

        protected virtual Brush GetBrush(Rectangle r)
        {
            //Fill rectangle
            Color fill1;
            Brush b;
            if (opacity == 100)
            {
                fill1 = fillColor;
            }
            else
            {
                fill1 = Color.FromArgb((int)(255.0f * (opacity / 100.0f)), fillColor);
            }
            b = new SolidBrush(fill1);
            return b;
        }

        internal override void Draw(Graphics g)
        {
            IsInvalidated = false;
            Rectangle r = GetUnsignedRectangle();

            DrawBorder(g, r);
            switch ((int)state)
            {
                case 0:
                    {
                        fillColor = Color.Transparent;
                        opacity = 100;
                    }
                    break;
                case 1://报警
                    {
                        IsTwinkle = !IsTwinkle;
                        if (!IsTwinkle)
                        {
                            fillColor = Color.Red;
                            opacity = 40;
                        }
                        else
                        {
                            fillColor = Color.Transparent;
                            opacity = 100;
                        }
                        break;
                    }
                case 2:
                    {
                        fillColor = Color.Transparent;
                        opacity = 100;
                    }
                    break;
            }

            if (image != null)
            {
                g.DrawImage(image, r);
            }
            else
            {
                Brush b = GetBrush(r);
                g.FillRectangle(b, r);
                b.Dispose();
            }
        }

        internal override void DrawAlert(Graphics g)
        {
            IsInvalidated = false;

            Rectangle r = GetUnsignedRectangle();

            DrawBorder(g, r);

            if (image != null)
            {
                g.DrawImage(image, r);
            }
        }

        protected virtual void DrawBorder(Graphics g, Rectangle r)
        {
            //Border
            Pen p = new Pen(borderColor, borderWidth);
            g.DrawRectangle(p, r);
            p.Dispose();

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
