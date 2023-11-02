using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing.Text;


namespace Dalssoft.DiagramNet
{
    [
     Serializable,
     TypeConverter(typeof(ExpandableObjectConverter))
     ]
    public class LEDAndon : BaseElement, IControllable
    {
        protected Color fontcolor1 = Color.Red;
        protected Color fontcolor2 = Color.Red;
        protected Color fontcolor3 = Color.Red;

        protected Color backColor1 = Color.Empty;
        protected Color backColor2 = Color.Empty;



        [NonSerialized]
        private RectangleController controller;
        protected Image imageDefault1 = Diagram.NET.Resource.AndonOn;
        protected Image imageDefault2 = Diagram.NET.Resource.AndonOn;
        protected Image imageAlert1 = Diagram.NET.Resource.AndonAlert;
        protected Image imageAlert2 = Diagram.NET.Resource.AndonAlert;
        protected Font font1 = new Font(FontFamily.GenericSansSerif, 10);
        protected Font font2 = new Font(FontFamily.GenericSansSerif, 10);
        protected Font font3 = new Font(FontFamily.GenericSansSerif, 10);
        //protected string text1 = "";
        protected string text2 = "";
        protected string text3 = "";
        protected bool autoSize = false;
        [NonSerialized]
        private StringFormat format = new StringFormat(StringFormatFlags.NoWrap);



        protected LabelElement label = new LabelElement();

        [Category("监控信息")]
        [Description("监控对象")]
        [DynamicProps.DynamicList("SQL")]
        [TypeConverter(typeof(DynamicProps.NameConverter))]
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
                    int index = attributes.codeNameCollection.IndexOf(value);
                    monitoredObjectID = attributes.idCollection[index].ToString();
                    monitoredObjectCode = attributes.codeCollection[index].ToString();
                    monitoredObjectName = attributes.nameCollection[index].ToString();
                    TextAutoSize(label, this);
                    OnAppearanceChanged(new EventArgs());
                }
            }
        }
        [Category("总背景")]
        [Description("背景颜色")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual Color BackColor1
        {
            get
            {
                return backColor1;
            }
            set
            {
                backColor1 = value;
                OnAppearanceChanged(new EventArgs());
            }
        }
        [Category("总背景")]
        [Description("背景颜色")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual Color BackColor2
        {
            get
            {
                return backColor2;
            }
            set
            {
                backColor2 = value;
                OnAppearanceChanged(new EventArgs());
            }
        }
        [Category("总背景")]
        [Description("线条颜色")]
        [RefreshProperties(RefreshProperties.All)]
        public override Color BorderColor
        {
            get
            {
                return borderColor;
            }
            set
            {
                borderColor = value;
                OnAppearanceChanged(new EventArgs());
            }
        }

        [Category("总背景")]
        [Description("线宽")]
        [DefaultValue(1)]
        public override int BorderWidth
        {
            get
            {
                return borderWidth;
            }
            set
            {
                borderWidth = value;
                OnAppearanceChanged(new EventArgs());
            }
        }
        [Category("andon")]
        [Description("工作图片")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual Image 默认图片1
        {
            get
            {
                return imageDefault1;
            }
            set
            {
                imageDefault1 = value;  //使用value来传递要写入的数据
                OnAppearanceChanged(new EventArgs());
            }
        }
        [Category("andon")]
        [Description("工作图片")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual Image 默认图片2
        {
            get
            {
                return imageDefault2;
            }
            set
            {
                imageDefault2 = value;
                OnAppearanceChanged(new EventArgs());
            }
        }
        [Category("报警")]
        [Description("报警图片")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual Image ANDON按下图片1
        {
            get
            {
                return imageAlert1;
            }
            set
            {
                imageAlert1 = value;
                OnAppearanceChanged(new EventArgs());
            }
        }
        [Category("报警")]
        [Description("报警图片")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual Image ANDON按下图片2
        {
            get
            {
                return imageAlert2;
            }
            set
            {
                imageAlert2 = value;
                OnAppearanceChanged(new EventArgs());
            }
        }
        [Category("标题")]
        [Description("字体")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual Font Font1
        {
            get
            {
                return font1;
            }
            set
            {
                font1 = value;
                if (autoSize) DoAutoSize();
                OnAppearanceChanged(new EventArgs());

            }
        }
        [Category("标题")]
        [Description("字体颜色")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual Color Fontcolor1
        {
            get
            {
                return fontcolor1;
            }
            set
            {
                fontcolor1 = value;
                OnAppearanceChanged(new EventArgs());
            }
        }
        //[Category("标题")]
        //[Description("文本")]
        //[RefreshProperties(RefreshProperties.All)]
        //public virtual string Text1
        //{
        //    get
        //    {
        //        return text1;
        //    }
        //    set
        //    {
        //        text1 = value;
        //        if (autoSize) DoAutoSize();
        //        OnAppearanceChanged(new EventArgs());
        //    }
        //}



        [Category("物料andon")]
        [Description("字体")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual Font Font2
        {
            get
            {
                return font2;
            }
            set
            {
                font2 = value;
                if (autoSize) DoAutoSize();
                OnAppearanceChanged(new EventArgs());

            }
        }
        [Category("物料andon")]
        [Description("字体颜色")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual Color Fontcolor2
        {
            get
            {
                return fontcolor2;
            }
            set
            {
                fontcolor2 = value;
                OnAppearanceChanged(new EventArgs());
            }
        }
        [Category("物料andon")]
        [Description("文本")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual string Text2
        {
            get
            {
                return text2;
            }
            set
            {
                text2 = value;
                if (autoSize) DoAutoSize();
                OnAppearanceChanged(new EventArgs());
            }
        }
        internal StringFormat Format
        {
            get
            {
                return format;
            }
        }
        [Category("质量andon")]
        [Description("字体")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual Font Font3
        {
            get
            {
                return font3;
            }
            set
            {
                font3 = value;
                if (autoSize) DoAutoSize();
                OnAppearanceChanged(new EventArgs());

            }
        }
        [Category("质量andon")]
        [Description("字体颜色")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual Color Fontcolor3
        {
            get
            {
                return fontcolor3;
            }
            set
            {
                fontcolor3 = value;
                OnAppearanceChanged(new EventArgs());
            }
        }
        [Category("质量andon")]
        [Description("文本")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual string Text3
        {
            get
            {
                return text3;
            }
            set
            {
                text3 = value;
                if (autoSize) DoAutoSize();
                OnAppearanceChanged(new EventArgs());
            }
        }


        public void DoAutoSize()
        {
            //if (text1.Length == 0) return;

            //Bitmap bmp1 = new Bitmap(1, 1);
            //Graphics g1 = Graphics.FromImage(bmp1);//创建一个bitmap对象作为输入参数并返回一个Graphics对象的静态方法，即获得一个Graphics对象
            //SizeF sizeF1 = g1.MeasureString(text1, font1, size.Width, format);//测量指定的font绘制的字符串
            //Size sizeTmp1 = Size.Round(sizeF1);//四舍五入

            //if (size.Height < sizeTmp1.Height)
            //    size.Height = sizeTmp1.Height;

            if (text2.Length == 0) return;

            Bitmap bmp2 = new Bitmap(1, 1);
            Graphics g2 = Graphics.FromImage(bmp2);
            SizeF sizeF2 = g2.MeasureString(text2, font2, size.Width, format);
            Size sizeTmp2 = Size.Round(sizeF2);

            if (size.Height < sizeTmp2.Height)
                size.Height = sizeTmp2.Height;


            if (text3.Length == 0) return;

            Bitmap bmp3 = new Bitmap(1, 1);
            Graphics g3 = Graphics.FromImage(bmp3);
            SizeF sizeF3 = g2.MeasureString(text3, font3, size.Width, format);
            Size sizeTmp3 = Size.Round(sizeF3);

            if (size.Height < sizeTmp3.Height)
                size.Height = sizeTmp3.Height;
        }
        protected virtual Brush GetBrushBackColor(Rectangle r)
        {
            //Fill rectangle
            Color fill1;
            Color fill2;
            Brush b;
            if (opacity == 100)
            {
                fill1 = backColor1;
                fill2 = backColor2;
            }
            else
            {
                fill1 = Color.FromArgb((int)(255.0f * (opacity / 100.0f)), backColor1); //double类型强制转换为float类型.
                fill2 = Color.FromArgb((int)(255.0f * (opacity / 100.0f)), backColor2);//alpha取值范围为0~255
            }

            if (backColor2 == Color.Empty)
                b = new SolidBrush(fill1);//b用来填充矩形
            else
            {
                Rectangle rb = new Rectangle(r.X, r.Y, r.Width + 1, r.Height + 1);
                b = new LinearGradientBrush(
                    rb,
                    fill1,
                    fill2,
                    LinearGradientMode.Horizontal);//使用线性渐变绘制区域
            }

            return b;
        }

        public LEDAndon()
            : this(0, 0, 100, 100)
        { }

        public LEDAndon(Rectangle rec)
            : this(rec.Location, rec.Size)
        { elementMonitoredType = MonitoredType.andon控件; }

        public LEDAndon(Point l, Size s)
            : this(l.X, l.Y, s.Width, s.Height)
        { }

        public LEDAndon(int top, int left, int width, int height)
        {
            location = new Point(top, left);
            size = new Size(width, height);
        }




        internal override void Draw(Graphics g)
        {
            if (size.Height > size.Width * 2)
                size.Height = size.Width;

            IsInvalidated = false;
            Image tmpImage1 = imageDefault1;
            Image tmpImage2 = imageDefault2;

            Rectangle r = GetUnsignedRectangle(
               new Rectangle(
               location.X, location.Y,
               size.Width, size.Height));

            Rectangle r1 = GetUnsignedRectangle(
                new Rectangle(location.X + (int)(size.Width * 0.5), location.Y + (int)(size.Height * 0.1), (int)(size.Height * 0.9), (int)(size.Height * 0.9)));
            Rectangle r2 = GetUnsignedRectangle(
               new Rectangle(location.X + (int)(size.Width * 0.75), location.Y + (int)(size.Height * 0.1), (int)(size.Height * 0.9), (int)(size.Height * 0.9)));
            Rectangle r3 = GetUnsignedRectangle(
                new Rectangle(location.X, location.Y, (int)(size.Width * 0.4), (int)(size.Height)
                ));
            //Rectangle r4 = GetUnsignedRectangle(
            //    new Rectangle(location.X+(int)(size.Width * 0.4), location.Y+(int)(size.Height * 0.28), (int)(size.Width * 0.6), (int)(size.Height * 0.4)));
            //Rectangle r5 = GetUnsignedRectangle(
            //   new Rectangle(location.X + (int)(size.Width * 0.4), location.Y + (int)(size.Height * 0.63), (int)(size.Width * 0.6), (int)(size.Height * 0.4)));


            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;

            Brush b = GetBrushBackColor(r);//画笔b从GetBrushForeColor方法获得
            g.FillRectangle(b, r);//画笔填充矩形

            g.DrawString(monitoredObjectCode, font1, new SolidBrush(fontcolor1), r3, sf);

            Pen p = new Pen(borderColor, borderWidth);
            g.DrawRectangle(p, r);//钢笔绘制矩形
            p.Dispose();//释放

            //g.DrawString(text2, font2, new SolidBrush(fontcolor2), r4, sf);

            //g.DrawString(text3, font3, new SolidBrush(fontcolor3), r5, sf);


            #region 图片
            switch ((int)state2)
            {
                case 0:
                    tmpImage1 = imageDefault1;
                    tmpImage2 = imageDefault2;
                    break;
                case 1:
                    tmpImage1 = imageAlert1;//报警
                    tmpImage2 = imageDefault2;
                    break;
                case 2:
                    tmpImage1 = imageDefault1;
                    tmpImage2 = imageAlert2;
                    break;
                case 3:
                    tmpImage1 = imageAlert1;
                    tmpImage2 = imageAlert2;
                    break;
            }
            if (tmpImage1 != null && tmpImage2 != null)
            {
                g.DrawImage(tmpImage1, r1.Location.X, r1.Location.Y, r1.Size.Width, r1.Size.Height);
                g.DrawImage(tmpImage2, r2.Location.X, r2.Location.Y, r2.Size.Width, r2.Size.Height);
            }

            #endregion
        }
        //protected virtual void DrawBorder(Graphics g, Rectangle r3)
        //{
        //    //Border
        //    Pen p = new Pen(borderColor, borderWidth);
        //    g.DrawRectangle(p, r3);
        //    p.Dispose();
        //}
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
        IController IControllable.GetController()
        {
            if (controller == null)
                controller = new RectangleController(this);
            return controller;
        }
    }
}
