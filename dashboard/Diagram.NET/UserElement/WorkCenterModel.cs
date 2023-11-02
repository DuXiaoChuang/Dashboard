using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Collections.Generic;

namespace Dalssoft.DiagramNet
{
    [Serializable,
    TypeConverter(typeof(ExpandableObjectConverter))]
    public class WorkCenterModel : BaseElement, IControllable, ILabelElement
    {
        [NonSerialized]
        private RectangleController controller;
        private volatile bool IsTwinkle = false;
        protected LabelElement label = new LabelElement();
        private MovementDirection Direction = MovementDirection.向右;
        protected Color fillColor1 = Color.GhostWhite;
        protected Color fillColor2 = Color.GhostWhite;
        protected GradientMode gradientMode = GradientMode.水平;


        //[CategoryAttribute("监控信息")]
        //[DescriptionAttribute("监控对象")]
        //[DynamicProps.DynamicList("SQL")]
        //[TypeConverterAttribute(typeof(DynamicProps.NameConverter))]
        //[RefreshProperties(RefreshProperties.All)]
        //public override string 监控对象
        //{
        //    get
        //    {
        //        return monitorObject;
        //    }
        //    set
        //    {
        //        if (value != string.Empty)
        //        {
        //            monitorObject = value;
        //            DynamicProps.ListAttribute attributes = new DynamicProps.ListAttribute(SQL);
        //            int index = attributes.codeNameCollection.IndexOf(this.监控对象);
        //            monitoredObjectID = attributes.idCollection[index].ToString();
        //            monitoredObjectCode = attributes.codeCollection[index].ToString();
        //            monitoredObjectName = attributes.nameCollection[index].ToString();
        //            TextAutoSize(label, this);
        //            OnAppearanceChanged(new EventArgs());
        //        }
        //    }
        //}


        [Category("模型信息")]
        [Description("托盘放行方向")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual MovementDirection 运动方向
        {
            get
            {
                return Direction;
            }
            set
            {
                Direction = value;
            }

        }


        public virtual Color 填充色1
        {
            get
            {
                return fillColor1;
            }
            set
            {
                fillColor1 = value;
                OnAppearanceChanged(new EventArgs());
            }
        }

        public virtual Color 填充色2
        {
            get
            {
                return fillColor2;
            }
            set
            {
                fillColor2 = value;
                OnAppearanceChanged(new EventArgs());
            }
        }

        public virtual GradientMode 渲染样式
        {
            get
            {
                return gradientMode;
            }
            set
            {
                gradientMode = value;
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

        public WorkCenterModel(): this(0, 0, 100, 100)
        { }

        public WorkCenterModel(Rectangle rec) : this(rec.Location, rec.Size)
        { elementMonitoredType = MonitoredType.工位模型; }

        public WorkCenterModel(Point l, Size s) : this(l.X, l.Y, s.Width, s.Height)
        { }

        public WorkCenterModel(int top, int left, int width, int height)
        {
            location = new Point(top, left);
            size = new Size(width, height);
        }

        protected virtual Brush GetBrush(Rectangle r)
        {
            //Fill rectangle
            Color fill1;
            Color fill2;
            Brush b = Brushes.Transparent;
            if (opacity == 100)
            {
                fill1 = fillColor1;
                fill2 = fillColor2;
            }
            else
            {
                fill1 = Color.FromArgb((int)(255.0f * (opacity / 100.0f)), fillColor1);
                fill2 = Color.FromArgb((int)(255.0f * (opacity / 100.0f)), fillColor2);
            }

            if (fillColor2 == Color.Empty)
                b = new SolidBrush(fill1);
            else
            {
                Rectangle rb = new Rectangle(r.X, r.Y, r.Width + 1, r.Height + 1);
                switch ((int)gradientMode)
                {
                    case 0:
                        b = new LinearGradientBrush(
                            rb,
                            fill1,
                            fill2,
                            LinearGradientMode.Horizontal);
                        break;
                    case 1:

                        b = new LinearGradientBrush(
                            rb,
                            fill1,
                            fill2,
                            LinearGradientMode.Vertical);
                        break;
                    case 2:
                        b = new LinearGradientBrush(
                            rb,
                            fill1,
                            fill2,
                            LinearGradientMode.BackwardDiagonal);
                        break;
                }
            }

            return b;
        }

        protected virtual void DrawBorder(Graphics g, Rectangle r)
        {
            //Border
            Pen p = new Pen(borderColor, borderWidth);
            g.DrawRectangle(p, r);
            p.Dispose();
        }

        internal override void Draw(Graphics g)
        {
            IsInvalidated = false;

            if (Direction == MovementDirection.无)
            {


                Rectangle rectLeft = GetUnsignedRectangle(new Rectangle(location.X + (int)(size.Width / 8), location.Y, (int)(size.Width / 8), size.Height));
                Brush b = GetBrush(rectLeft);
                g.FillRectangle(b, rectLeft);
                DrawBorder(g, rectLeft);

                Rectangle rectRight = GetUnsignedRectangle(new Rectangle(location.X - 2 * (int)(size.Width / 8) + size.Width, location.Y, (int)(size.Width / 8), size.Height));
                b = GetBrush(rectRight);
                g.FillRectangle(b, rectRight);
                DrawBorder(g, rectRight);

                Rectangle rectUp = GetUnsignedRectangle(new Rectangle(location.X, location.Y, size.Width, (int)(size.Height / 9)));
                b = GetBrush(rectUp);
                g.FillRectangle(b, rectUp);
                DrawBorder(g, rectUp);

                Rectangle rectDown = GetUnsignedRectangle(new Rectangle(location.X, location.Y + size.Height - (int)(size.Height / 9), size.Width, (int)(size.Height / 9)));
                b = GetBrush(rectDown);
                g.FillRectangle(b, rectDown);
                DrawBorder(g, rectDown);

                Rectangle rectCenter = GetUnsignedRectangle(new Rectangle(location.X, location.Y + (int)(size.Height * 0.2), size.Width, (int)(size.Height * 0.6)));
                b = GetBrush(rectCenter);
                g.FillRectangle(b, rectCenter);
                DrawBorder(g, rectCenter);
            }
            if (Direction == MovementDirection.向上)
            {

                Rectangle rectUp = GetUnsignedRectangle(new Rectangle(location.X, location.Y + (int)(size.Height / 8), size.Width, (int)(size.Height / 8)));
                Brush b = GetBrush(rectUp);
                g.FillRectangle(b, rectUp);
                DrawBorder(g, rectUp);

                Rectangle rectDown = GetUnsignedRectangle(new Rectangle(location.X, location.Y - 2 * (int)(size.Height / 8) + size.Height, size.Width, (int)(size.Height / 8)));
                b = GetBrush(rectDown);
                g.FillRectangle(b, rectDown);
                DrawBorder(g, rectDown);

                Rectangle rectLeft = GetUnsignedRectangle(new Rectangle(location.X, location.Y, (int)(size.Width/9), size.Height));
                b = GetBrush(rectLeft);
                g.FillRectangle(b, rectLeft);
                DrawBorder(g, rectLeft);

                Rectangle rectRight = GetUnsignedRectangle(new Rectangle(location.X + size.Width - (int)(size.Width / 9), location.Y, (int)(size.Width / 9), size.Height));
                b = GetBrush(rectRight);
                g.FillRectangle(b, rectRight);
                DrawBorder(g, rectRight);

                Rectangle rectCenter = GetUnsignedRectangle(new Rectangle(location.X + (int)(size.Width * 0.2), location.Y, (int)(size.Width*0.6), size.Height));
                b = GetBrush(rectCenter);
                g.FillRectangle(b, rectCenter);
                DrawBorder(g, rectCenter);

                g.DrawLine(new Pen(borderColor, borderWidth), location.X + (int)(size.Width * 0.5), location.Y + (int)(size.Height / 18), location.X + (int)(size.Width* 0.4 ), location.Y + 3*(int)(size.Height/18));
                g.DrawLine(new Pen(borderColor, borderWidth), location.X + (int)(size.Width * 0.5), location.Y + (int)(size.Height / 18), location.X + (int)(size.Width * 0.6), location.Y + 3 * (int)(size.Height / 18));
                g.DrawLine(new Pen(BorderColor, borderWidth), location.X + (int)(size.Width * 0.4), location.Y + 3 * (int)(size.Height / 18), location.X + (int)(size.Width * 0.6), location.Y + 3 * (int)(size.Height / 18));


                g.DrawLine(new Pen(borderColor, borderWidth), location.X + (int)(size.Width * 0.5), location.Y + 15 * (int)(size.Height / 18), location.X + (int)(size.Width * 0.4), location.Y +17* (int)(size.Height/18));
                g.DrawLine(new Pen(borderColor, borderWidth), location.X + (int)(size.Width * 0.5), location.Y + 15 * (int)(size.Height / 18), location.X + (int)(size.Width * 0.6), location.Y + 17 * (int)(size.Height / 18));
                g.DrawLine(new Pen(BorderColor, borderWidth), location.X + (int)(size.Width * 0.4), location.Y + 17 * (int)(size.Height / 18), location.X + (int)(size.Width * 0.6), location.Y + 17 * (int)(size.Height / 18));
            }
            else if (Direction == MovementDirection.向下)
            {
                Rectangle rectUp = GetUnsignedRectangle(new Rectangle(location.X, location.Y + (int)(size.Height / 8), size.Width, (int)(size.Height / 8)));
                Brush b = GetBrush(rectUp);
                g.FillRectangle(b, rectUp);
                DrawBorder(g, rectUp);

                Rectangle rectDown = GetUnsignedRectangle(new Rectangle(location.X, location.Y - 2 * (int)(size.Height / 8) + size.Height, size.Width, (int)(size.Height / 8)));
                b = GetBrush(rectDown);
                g.FillRectangle(b, rectDown);
                DrawBorder(g, rectDown);

                Rectangle rectLeft = GetUnsignedRectangle(new Rectangle(location.X, location.Y, (int)(size.Width / 9), size.Height));
                b = GetBrush(rectLeft);
                g.FillRectangle(b, rectLeft);
                DrawBorder(g, rectLeft);

                Rectangle rectRight = GetUnsignedRectangle(new Rectangle(location.X + size.Width - (int)(size.Width / 9), location.Y, (int)(size.Width / 9), size.Height));
                b = GetBrush(rectRight);
                g.FillRectangle(b, rectRight);
                DrawBorder(g, rectRight);

                Rectangle rectCenter = GetUnsignedRectangle(new Rectangle(location.X + (int)(size.Width * 0.2), location.Y, (int)(size.Width * 0.6), size.Height));
                b = GetBrush(rectCenter);
                g.FillRectangle(b, rectCenter);
                DrawBorder(g, rectCenter);

                g.DrawLine(new Pen(borderColor, borderWidth), location.X + (int)(size.Width * 0.5), location.Y + 17*(int)(size.Height / 18), location.X + (int)(size.Width * 0.4), location.Y + 15 * (int)(size.Height / 18));
                g.DrawLine(new Pen(borderColor, borderWidth), location.X + (int)(size.Width * 0.5), location.Y + 17*(int)(size.Height / 18), location.X + (int)(size.Width * 0.6), location.Y + 15 * (int)(size.Height / 18));
                g.DrawLine(new Pen(BorderColor, borderWidth), location.X + (int)(size.Width * 0.4), location.Y + 15 * (int)(size.Height / 18), location.X + (int)(size.Width * 0.6), location.Y + 15 * (int)(size.Height / 18));


                g.DrawLine(new Pen(borderColor, borderWidth), location.X + (int)(size.Width * 0.5), location.Y + 3*(int)(size.Height / 18), location.X + (int)(size.Width * 0.4), location.Y + (int)(size.Height / 18));
                g.DrawLine(new Pen(borderColor, borderWidth), location.X + (int)(size.Width * 0.5), location.Y + 3*(int)(size.Height / 18), location.X + (int)(size.Width * 0.6), location.Y + (int)(size.Height / 18));
                g.DrawLine(new Pen(BorderColor, borderWidth), location.X + (int)(size.Width * 0.4), location.Y + (int)(size.Height / 18), location.X + (int)(size.Width * 0.6), location.Y +(int)(size.Height / 18));
            }
            else if (Direction == MovementDirection.向右)
            {
                Rectangle rectLeft = GetUnsignedRectangle(new Rectangle(location.X + (int)(size.Width / 8), location.Y, (int)(size.Width / 8), size.Height));
                Brush b = GetBrush(rectLeft);
                g.FillRectangle(b, rectLeft);
                DrawBorder(g, rectLeft);

                Rectangle rectRight = GetUnsignedRectangle(new Rectangle(location.X - 2 * (int)(size.Width / 8) + size.Width, location.Y, (int)(size.Width / 8), size.Height));
                b = GetBrush(rectRight);
                g.FillRectangle(b, rectRight);
                DrawBorder(g, rectRight);

                Rectangle rectUp = GetUnsignedRectangle(new Rectangle(location.X, location.Y, size.Width, (int)(size.Height / 9)));
                b = GetBrush(rectUp);
                g.FillRectangle(b, rectUp);
                DrawBorder(g, rectUp);

                Rectangle rectDown = GetUnsignedRectangle(new Rectangle(location.X, location.Y + size.Height - (int)(size.Height / 9), size.Width, (int)(size.Height / 9)));
                b = GetBrush(rectDown);
                g.FillRectangle(b, rectDown);
                DrawBorder(g, rectDown);

                Rectangle rectCenter = GetUnsignedRectangle(new Rectangle(location.X, location.Y + (int)(size.Height * 0.2), size.Width, (int)(size.Height * 0.6)));
                b = GetBrush(rectCenter);
                g.FillRectangle(b, rectCenter);
                DrawBorder(g, rectCenter);

                g.DrawLine(new Pen(borderColor, borderWidth), location.X + (int)(size.Width / 18), location.Y + (int)(size.Height * 0.4), location.X + (int)(3 * size.Width / 18), location.Y + (int)(size.Height * 0.5));
                g.DrawLine(new Pen(borderColor, borderWidth), location.X + (int)(size.Width / 18), location.Y + (int)(size.Height * 0.6), location.X + (int)(3 * size.Width / 18), location.Y + (int)(size.Height * 0.5));
                g.DrawLine(new Pen(BorderColor, borderWidth), location.X + (int)(size.Width / 18), location.Y + (int)(size.Height * 0.4), location.X + (int)(size.Width / 18), location.Y + (int)(size.Height * 0.6));


                g.DrawLine(new Pen(borderColor, borderWidth), location.X + 8 * (int)(size.Width / 9), location.Y + (int)(size.Height * 0.4), location.X + 9 * (int)(size.Width / 9), location.Y + (int)(size.Height * 0.5));
                g.DrawLine(new Pen(borderColor, borderWidth), location.X + 8 * (int)(size.Width / 9), location.Y + (int)(size.Height * 0.6), location.X + 9 * (int)(size.Width / 9), location.Y + (int)(size.Height * 0.5));
                g.DrawLine(new Pen(BorderColor, borderWidth), location.X + 8 * (int)(size.Width / 9), location.Y + (int)(size.Height * 0.4), location.X + 8 * (int)(size.Width / 9), location.Y + (int)(size.Height * 0.6));
            }
            else if (Direction == MovementDirection.向左)
            {

                Rectangle rectLeft = GetUnsignedRectangle(new Rectangle(location.X + (int)(size.Width / 8), location.Y, (int)(size.Width / 8), size.Height));
                Brush b = GetBrush(rectLeft);
                g.FillRectangle(b, rectLeft);
                DrawBorder(g, rectLeft);

                Rectangle rectRight = GetUnsignedRectangle(new Rectangle(location.X - 2 * (int)(size.Width / 8) + size.Width, location.Y, (int)(size.Width / 8), size.Height));
                b = GetBrush(rectRight);
                g.FillRectangle(b, rectRight);
                DrawBorder(g, rectRight);

                Rectangle rectUp = GetUnsignedRectangle(new Rectangle(location.X, location.Y, size.Width, (int)(size.Height / 9)));
                b = GetBrush(rectUp);
                g.FillRectangle(b, rectUp);
                DrawBorder(g, rectUp);

                Rectangle rectDown = GetUnsignedRectangle(new Rectangle(location.X, location.Y + size.Height - (int)(size.Height / 9), size.Width, (int)(size.Height / 9)));
                b = GetBrush(rectDown);
                g.FillRectangle(b, rectDown);
                DrawBorder(g, rectDown);

                Rectangle rectCenter = GetUnsignedRectangle(new Rectangle(location.X, location.Y + (int)(size.Height * 0.2), size.Width, (int)(size.Height * 0.6)));
                b = GetBrush(rectCenter);
                g.FillRectangle(b, rectCenter);
                DrawBorder(g, rectCenter);

                g.DrawLine(new Pen(borderColor, borderWidth), location.X + (int)(17 * size.Width / 18), location.Y + (int)(size.Height * 0.4), location.X + (int)(15 * size.Width / 18), location.Y + (int)(size.Height * 0.5));
                g.DrawLine(new Pen(borderColor, borderWidth), location.X + (int)(17 * size.Width / 18), location.Y + (int)(size.Height * 0.6), location.X + (int)(15 * size.Width / 18), location.Y + (int)(size.Height * 0.5));
                g.DrawLine(new Pen(BorderColor, borderWidth), location.X + (int)(17 * size.Width / 18), location.Y + (int)(size.Height * 0.4), location.X + (int)(17 * size.Width / 18), location.Y + (int)(size.Height * 0.6));


                g.DrawLine(new Pen(borderColor, borderWidth), location.X + 3 * (int)(size.Width / 18), location.Y + (int)(size.Height * 0.4), location.X + (int)(size.Width / 18), location.Y + (int)(size.Height * 0.5));
                g.DrawLine(new Pen(borderColor, borderWidth), location.X + 3 * (int)(size.Width / 18), location.Y + (int)(size.Height * 0.6), location.X + (int)(size.Width / 18), location.Y + (int)(size.Height * 0.5));
                g.DrawLine(new Pen(BorderColor, borderWidth), location.X + 3 * (int)(size.Width / 18), location.Y + (int)(size.Height * 0.4), location.X + 3 * (int)(size.Width / 18), location.Y + (int)(size.Height * 0.6));
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
