//using System;
//using System.Drawing;
//using System.Drawing.Drawing2D;
//using Dalssoft.DiagramNet;
//using System.Drawing.Design;
//using System.ComponentModel;
//using System.Data;

//namespace Dalssoft.DiagramNet
//{
//    [
//   Serializable,
//   TypeConverter(typeof(ExpandableObjectConverter))
//   ]

//    public class 时间 : BaseElement, IControllable
//    {
//        [NonSerialized]
//        private RectangleController controller;
//        protected Color clockBackColor = Color.Black;
//        protected Color clockFadeColor = Color.Transparent;
//        protected Color clockForeColor = Color.Red;


//        const int DEFAULT_SCREENMARGIN = 5;
//        const int DEFAULT_NUMBERMARGIN = 2;
//        const int DEFAULT_NUMBERPENWIDTH = 10;
//        const int DEFAULT_NUMBERWIDTH = 40;
//        const int DEFAULT_STARTNUMBER = 0;
//        readonly Color DEFAULT_BACKCOLOR = Color.Black;
//        readonly Color DEFAULT_FORECOLOR = Color.Red;
//        readonly Color DEFAULT_FADECOLOR = Color.FromArgb(100, Color.Red);
//        public 时间()
//            {
//                ScreenMargin = DEFAULT_SCREENMARGIN;
//                NumberMargin = DEFAULT_NUMBERMARGIN;
//                NumberWidth = DEFAULT_NUMBERWIDTH;
//                NumberPenWidth = DEFAULT_NUMBERPENWIDTH;
//                StartNumber = DEFAULT_STARTNUMBER;
//                ScreenNumber = DEFAULT_STARTNUMBER;
//                m_BackColor = DEFAULT_BACKCOLOR;
//                m_ForeColor = DEFAULT_FORECOLOR;
//                m_FadeColor = DEFAULT_FADECOLOR;
//                m_IsPerchShowed = true;
//                m_BackImage = new Bitmap(NumberWidth + ScreenMargin * 2, NumberWidth * 2 + NumberMargin * 4 + 2 * ScreenMargin);
//                m_Gra = Graphics.FromImage(m_BackImage);
//                m_Brush = new SolidBrush(m_ForeColor);

//                m_Gra.Clear(m_BackColor);
//            }



//        protected LabelElement label = new LabelElement();
//        private DateTime currentTime;
//        int ValidDigit;
//        int[] Numbers = null;
//        Bitmap MainScreenImage;
//        ClockStyle ThisKind=ClockStyle.Hour_Minute_Second;



//          [Category("外观")]
//          [Description("显示类型")]
//          [RefreshProperties(RefreshProperties.All)]
//          public ClockStyle Kind
//        {
//            get
//            {
//                return ThisKind;
//            }
//            set
//            {
//                ThisKind = value;
//            }
//        }

//          [Category("外观")]
//          [Description("屏幕边缘与数字边缘之间的距离")]
//          [RefreshProperties(RefreshProperties.All)]
//          public int m_ScreenMargin
//          {
//              get { return ScreenMargin; }
//              set
//              {
                  
//                  ScreenMargin = value;
                 
//              }
//          }

//          [Category("外观")]
//          [Description("数字各部分接合处的缝隙宽度")]
//          [RefreshProperties(RefreshProperties.All)]
//          public int m_ScreenMargin
//          {
//              get { return NumberMargin; }
//              set
//              {

//                  NumberMargin = value;

//              }
//          }
//          [Category("外观")]
//          [Description("数字宽度")]
//          [RefreshProperties(RefreshProperties.All)]
//          public int m_NumberWidth
//          {
//              get
//              {
//                  return NumberWidth;
//              }
//              set
//              {
//                  NumberWidth = value;
//                  NumberPenWidth = NumberWidth / 3;
//                  OnAppearanceChanged(new EventArgs());
//              }
//          }
//          [Category("外观")]
//          [Description("屏幕外缘宽度")]
//          [RefreshProperties(RefreshProperties.All)]
//          public int m_ScreenWidth
//          {
//              get { 
//                  return  (NumberWidth + 2 * ScreenMargin); 

//                   }
//          }

//          [Category("外观")]
//          [Description("屏幕外缘高度")]
//          [RefreshProperties(RefreshProperties.All)]
//          public int m_ScreenHeight
//          {
//              get { return (2 * NumberWidth + 2 * ScreenMargin + 4 * NumberMargin); }
//          }
      
//          [Category("外观")]
//          [Description("将要显示的数字")]
//          public int m_ScreenNumber
//          {
//              get { return ScreenNumber; }
//              set
//              {
//                  ScreenNumber = value;
//              }
//          }

//          [Category("外观")]
//          [Description("屏幕背景色")]
//          [RefreshProperties(RefreshProperties.All)]
//          public Color ClockBackColor//获取或设置屏幕背景色
//          {
//              get
//              {
//                  return clockBackColor;
//              }
//              set
//              {
//                  clockBackColor = value;

//              }
//          }
//          [Category("外观")]
//          [Description("数字显示后残留颜色")]
//          [RefreshProperties(RefreshProperties.All)]
//          public Color ClockFadeColor//获取或设置数字显示后的残迹颜色
//          {
//              get
//              {
//                  return clockFadeColor;
//              }
//              set
//              {
//                  clockFadeColor = value;

//              }
//          }
//          [Category("外观")]
//          [Description("数字颜色")]
//          [RefreshProperties(RefreshProperties.All)]
//          public Color ClockForeColor//获取或设置数字的前景色
//          {
//              get
//              {
//                  return clockForeColor;
//              }
//              set
//              {
//                  clockForeColor = value;

//              }
//          }
//        [Category("外观")]
//        [Description("label")]
//        public virtual LabelElement Label
//        {
//            get
//            {
//                return label;
//            }
//            set
//            {
//                label = value;
//                OnAppearanceChanged(new EventArgs());
//            }
//        }


//        public 时间()
//            : this(0, 0, 100, 100)
//        { }

//        public 时间(Rectangle rec)
//            : this(rec.Location, rec.Size)
//        { elementMonitoredType = MonitoredType.时间; }

//        public 时间(Point l, Size s)
//            : this(l.X, l.Y, s.Width, s.Height)
//        { }

//        public 时间(int top, int left, int width, int height)
//        {
//            location = new Point(top, left);
//            size = new Size(width, height);
//        }


//        internal override void Draw(Graphics g)
//        {
//            IsInvalidated = false;
//            Rectangle r = GetUnsignedRectangle(
//                new Rectangle(
//                location.X, location.Y,
//                size.Width, size.Height));
//            currentTime = DateTime.Now;
//            Numbers[0] = currentTime.Hour;
//            Numbers[1] = currentTime.Minute;
//            Numbers[2] = currentTime.Second;
//            string Style = "";
//            switch (Kind)
//            {
//                case ClockStyle.Hour: Style = "1000"; break;
//                case ClockStyle.Hour_Minute: Style = "1100"; break;
//                case ClockStyle.Hour_Minute_Second: Style = "1110"; break;
//                case ClockStyle.Hour_Minute_Second_MilliSecond: Style = "1111"; break;

//                case ClockStyle.Minute: Style = "0100"; break;
//                case ClockStyle.Minute_Second: Style = "0110"; break;
//                case ClockStyle.Minute_Second_MilliSecond: Style = "0111"; break;

//                case ClockStyle.Second: Style = "0010"; break;
//                case ClockStyle.Second_MilliSecond: Style = "0011"; break;

//                case ClockStyle.MilliSecond: Style = "0001"; break;
//            }

//            ValidDigit = 0;
//            for (int i = 0, max = Style.Length; i < max; i++)
//            {
//                if (Style[i] == '1')
//                    ValidDigit++;
//            }

//            DigitalScr.m_BackColor = ClockBackColor;
//            DigitalScr.m_FadeColor = ClockFadeColor;
//            DigitalScr.m_ForeColor = ClockForeColor;

//            System.Drawing.Bitmap bmpNum = DigitalScr.m_GetNumberImage(0, 2);
//            System.Drawing.Bitmap bmpSep = DigitalScr.m_GetSeparatorImage(true);
//            MainScreenImage = new Bitmap(
//                    (ValidDigit) * (bmpNum.Width) + (ValidDigit - 1) * (bmpSep.Width),
//                    bmpNum.Height);
//            Gra = System.Drawing.Graphics.FromImage(MainScreenImage);

//            for (int i = 0, pos = 0; i < 4; i++)
//            {
//                if (Style[i] == '0')
//                    continue;

//                System.Drawing.Bitmap bmpnum = DigitalScr.m_GetNumberImage(Numbers[i], 2);
//                if (bmpnum == null)
//                    continue;
//                Gra.DrawImage(
//                    bmpnum,
//                    (pos++) * (bmpnum.Width + bmpSep.Width),
//                    0);
//            }

//            for (int i = 0, max = ValidDigit - 1; i < max; i++)
//            {
//                Gra.DrawImage(
//                    bmpSep,
//                    (i + 1) * (bmpNum.Width) + (i) * (bmpSep.Width),
//                    0);
//            }


//            g.DrawImage(
//                MainScreenImage,
//                0,
//                0,
//                Width,
//                Height);

//            return MainScreenImage;


//        }


//        void DrawNumber()
//        {

//            string NumStr;
//            switch (ScreenNumber)
//            {
//                case 0: NumStr = "1111110"; break;
//                case 1: NumStr = "0011000"; break;
//                case 2: NumStr = "0110111"; break;
//                case 3: NumStr = "0111101"; break;
//                case 4: NumStr = "1011001"; break;
//                case 5: NumStr = "1101101"; break;
//                case 6: NumStr = "1101111"; break;
//                case 7: NumStr = "0111000"; break;
//                case 8: NumStr = "1111111"; break;
//                case 9: NumStr = "1111101"; break;
//                default:
//                    { throw new Exception("要绘制的数字有误，请检查参数。"); };


//            }

//            if ((!m_IsPerchShowed) && (ScreenNumber == 0))
//            {
//                DrawLineTopLeft(false);
//                DrawLineTop(false);
//                DrawLineTopRight(false);
//                DrawLineDownRight(false);
//                DrawLineBottom(false);
//                DrawLineDownLeft(false);
//                DrawLineMiddle(false);
//                return;
//            }

//            DrawLineTopLeft((NumStr[0] == '1' ? true : false));
//            DrawLineTop((NumStr[1] == '1' ? true : false));
//            DrawLineTopRight((NumStr[2] == '1' ? true : false));
//            DrawLineDownRight((NumStr[3] == '1' ? true : false));
//            DrawLineBottom((NumStr[4] == '1' ? true : false));
//            DrawLineDownLeft((NumStr[5] == '1' ? true : false));
//            DrawLineMiddle((NumStr[6] == '1' ? true : false));
//        }


//        /// <summary>
//        /// 在初始化时指定的范围绘制当前的数字
//        /// </summary>
//        /// <param name="aNumber"></param>
//        void DrawNumber(int aNumber)
//        {
//            ScreenNumber = aNumber;
//            DrawNumber();
//        }

//        /// <summary>
//        /// 绘制数字“8”中间的一横
//        /// </summary>
//        /// <param name="IsSolid"></param>
//        void DrawLineMiddle(bool IsSolid)
//        {
//            Points = new Point[6];

//            Points[0] = new Point(ScreenMargin, ScreenMargin + 2 * NumberMargin + NumberWidth);
//            Points[1] = new Point(ScreenMargin + NumberPenWidth, Points[0].Y + (NumberPenWidth) / 2);
//            Points[2] = new Point(ScreenMargin + NumberWidth - NumberPenWidth, Points[1].Y);
//            Points[3] = new Point(ScreenMargin + NumberWidth, Points[0].Y);
//            Points[4] = new Point(Points[2].X, Points[3].Y - (NumberPenWidth) / 2);
//            Points[5] = new Point(Points[1].X, Points[4].Y);

//            if (!IsSolid)
//            {
//                m_Brush.Color = m_FadeColor;
//            }
//            else
//            {
//                m_Brush.Color = m_ForeColor;
//            }

//            m_Gra.FillPolygon(m_Brush, Points);
//        }
//        /// <summary>
//        /// 绘制数字“8”顶端的一横
//        /// </summary>
//        /// <param name="IsSolid"></param>
//        void DrawLineTop(bool IsSolid)
//        {
//            Points = new Point[4];

//            Points[0] = new Point(ScreenMargin, ScreenMargin);
//            Points[1] = new Point(ScreenMargin + NumberPenWidth, ScreenMargin + NumberPenWidth);
//            Points[2] = new Point(ScreenMargin + NumberWidth - NumberPenWidth, Points[1].Y);
//            Points[3] = new Point(ScreenMargin + NumberWidth, Points[0].Y);

//            if (!IsSolid)
//            {
//                m_Brush.Color = m_FadeColor;
//            }
//            else
//            {
//                m_Brush.Color = m_ForeColor;
//            }

//            m_Gra.FillPolygon(m_Brush, Points);
//        }
//        /// <summary>
//        /// 绘制数字“8”底端的一横
//        /// </summary>
//        /// <param name="IsSolid"></param>
//        void DrawLineBottom(bool IsSolid)
//        {
//            Points = new Point[4];

//            Points[0] = new Point(ScreenMargin, ScreenMargin + 4 * NumberMargin + 2 * NumberWidth);
//            Points[1] = new Point(ScreenMargin + NumberWidth, Points[0].Y);
//            Points[2] = new Point(Points[1].X - NumberPenWidth, Points[1].Y - NumberPenWidth);
//            Points[3] = new Point(Points[0].X + NumberPenWidth, Points[2].Y);

//            if (!IsSolid)
//            {
//                m_Brush.Color = m_FadeColor;
//            }
//            else
//            {
//                m_Brush.Color = m_ForeColor;
//            }

//            m_Gra.FillPolygon(m_Brush, Points);
//        }
//        /// <summary>
//        /// 绘制数字“8”左上方的一竖
//        /// </summary>
//        /// <param name="IsSolid"></param>
//        void DrawLineTopLeft(bool IsSolid)
//        {
//            Points = new Point[4];

//            Points[0] = new Point(ScreenMargin, ScreenMargin + NumberMargin);
//            Points[1] = new Point(Points[0].X, Points[0].Y + NumberWidth);
//            Points[2] = new Point(Points[1].X + NumberPenWidth, Points[1].Y - (NumberPenWidth) / 2);
//            Points[3] = new Point(Points[2].X, Points[2].Y - NumberWidth + (NumberPenWidth) * 3 / 2);

//            if (!IsSolid)
//            {
//                m_Brush.Color = m_FadeColor;
//            }
//            else
//            {
//                m_Brush.Color = m_ForeColor;
//            }
//            m_Gra.FillPolygon(m_Brush, Points);
//        }
//        /// <summary>
//        /// 绘制数字“8”左下方的一竖
//        /// </summary>
//        /// <param name="IsSolid"></param>
//        void DrawLineDownLeft(bool IsSolid)
//        {
//            Points = new Point[4];

//            Points[0] = new Point(ScreenMargin, ScreenMargin + 3 * NumberMargin + NumberWidth);
//            Points[1] = new Point(Points[0].X, Points[0].Y + NumberWidth);
//            Points[2] = new Point(Points[1].X + NumberPenWidth, Points[1].Y - (NumberPenWidth));
//            Points[3] = new Point(Points[2].X, Points[2].Y - NumberWidth + (NumberPenWidth) * 3 / 2);

//            if (!IsSolid)
//            {
//                m_Brush.Color = m_FadeColor;
//            }
//            else
//            {
//                m_Brush.Color = m_ForeColor;
//            }

//            m_Gra.FillPolygon(m_Brush, Points);
//        }
//        /// <summary>
//        /// 绘制数字“8”右上方的一竖
//        /// </summary>
//        /// <param name="IsSolid"></param>
//        void DrawLineTopRight(bool IsSolid)
//        {
//            Points = new Point[4];

//            Points[0] = new Point(ScreenMargin + NumberWidth, ScreenMargin + NumberMargin);
//            Points[1] = new Point(Points[0].X, Points[0].Y + NumberWidth);
//            Points[2] = new Point(Points[1].X - NumberPenWidth, Points[1].Y - (NumberPenWidth) / 2);
//            Points[3] = new Point(Points[2].X, Points[2].Y - NumberWidth + (NumberPenWidth) * 3 / 2);

//            if (!IsSolid)
//            {
//                m_Brush.Color = m_FadeColor;
//            }
//            else
//            {
//                m_Brush.Color = m_ForeColor;
//            }

//            m_Gra.FillPolygon(m_Brush, Points);
//        }
//        /// <summary>
//        /// 绘制数字“8”右下方的一竖
//        /// </summary>
//        /// <param name="IsSolid"></param>
//        void DrawLineDownRight(bool IsSolid)
//        {
//            Points = new Point[4];

//            Points[0] = new Point(ScreenMargin + NumberWidth, ScreenMargin + 3 * NumberMargin + NumberWidth);
//            Points[1] = new Point(Points[0].X, Points[0].Y + NumberWidth);
//            Points[2] = new Point(Points[1].X - NumberPenWidth, Points[1].Y - (NumberPenWidth));
//            Points[3] = new Point(Points[2].X, Points[2].Y - NumberWidth + (NumberPenWidth) * 3 / 2);

//            if (!IsSolid)
//            {
//                m_Brush.Color = m_FadeColor;
//            }
//            else
//            {
//                m_Brush.Color = m_ForeColor;
//            }

//            m_Gra.FillPolygon(m_Brush, Points);
//        }



//        public Bitmap m_GetNumberImage()
//        {
//            m_Gra.Clear(m_BackColor);
//            DrawNumber();
//            return m_BackImage;
//        }


//        /// <summary>
//        /// 指定m_ScreenNumber的值，同时返回该数字的图像
//        /// </summary>
//        /// <param name="aNumber"></param>
//        /// <returns></returns>
//        public Bitmap m_GetNumberImage(int aNumber)
//        {
//            if (aNumber < 0)
//                aNumber = 0;
//            if (aNumber > 9)
//                aNumber = 9;
//            m_Gra.Clear(m_BackColor);
//            DrawNumber(aNumber);
//            return m_BackImage;
//        }













//                  /// <summary>
//            /// 屏幕边缘与数字外缘之间的距离
//            /// </summary>
//            int ScreenMargin;
//            /// <summary>
//            /// 数字各部分接合处的缝隙宽度
//            /// </summary>
//            int NumberMargin;
//            /// <summary>
//            /// 数字笔画的宽度
//            /// </summary>
//            int NumberPenWidth;
//            /// <summary>
//            /// 数字的宽度
//            /// </summary>
//            int NumberWidth;
//            /// <summary>
//            /// 正在或即将显示的数字
//            /// </summary>
//            int ScreenNumber;
//            /// <summary>
//            /// 计数的起始数字
//            /// </summary>
//            int StartNumber;

//            Graphics m_Gra;
//            SolidBrush m_Brush;
//            /// <summary>
//            /// 屏幕的背景图像
//            /// </summary>
//            Bitmap m_BackImage;
//            Point[] Points = null;



//            /// <summary>
//            /// 用于屏幕的颜色
//            /// </summary>
//            public Color m_BackColor;
//            /// <summary>
//            /// 用于正在显示中的数字的颜色
//            /// </summary>
//            public Color m_ForeColor;
//            /// <summary>
//            /// 用于显示完成后已消退的数字的颜色
//            /// </summary>
//            public Color m_FadeColor;
//            /// <summary>
//            /// 若为true则显示的是数字，否则显示分隔符(":")
//            /// </summary>
//            public bool m_IsNumber;
//            /// <summary>
//            /// 是否显示无效数字，如数字"0123"的"0"
//            /// </summary>
//            public bool m_IsPerchShowed;




//            public Bitmap m_GetNumberImage(int aNumber, int aDigit)
//            {
//                string NumStr = aNumber.ToString();
//                if (NumStr.Length > aDigit)
//                    return null;

//                Bitmap image = new Bitmap(m_ScreenWidth * aDigit, m_ScreenHeight);
//                Graphics Gra = Graphics.FromImage(image);

//                int pos = 0;
//                for (int len = (aDigit - (NumStr.Length)); pos < len; pos++)
//                {
//                    Gra.DrawImage(
//                        m_GetNumberImage(0),
//                        m_ScreenWidth * pos,
//                        0);
//                }

//                bool temp_IsPerchShowed = m_IsPerchShowed;
//                m_IsPerchShowed = true;
//                for (int i = 0; i < NumStr.Length; i++, pos++)
//                {
//                    Gra.DrawImage(
//                        m_GetNumberImage(Int32.Parse(NumStr[i].ToString())),
//                        m_ScreenWidth * pos,
//                        0);
//                }
//                m_IsPerchShowed = temp_IsPerchShowed;

//                return image;
//            }
//            /// <summary>
//            /// 返回用于数字时钟的适合当前尺寸的分隔符(":")
//            /// </summary>
//            /// <param name="IsSolid"></param>
//            /// <returns></returns>
//            public Bitmap m_GetSeparatorImage(bool IsSolid)
//            {
//                Bitmap image = new Bitmap(ScreenMargin * 2 + NumberPenWidth, m_ScreenHeight);
//                Graphics Gra = Graphics.FromImage(image);
//                SolidBrush Bru = new SolidBrush(m_ForeColor);
//                Gra.Clear(m_BackColor);

//                if (!IsSolid)
//                {
//                    Bru.Color = m_FadeColor;
//                }

//                Gra.FillRectangle(
//                    Bru,
//                    ScreenMargin,
//                    (m_ScreenHeight - NumberPenWidth * 3) / 2,
//                    NumberPenWidth,
//                    NumberPenWidth);

//                Gra.FillRectangle(
//                    Bru,
//                    ScreenMargin,
//                    (m_ScreenHeight - NumberPenWidth * 3) / 2 + NumberPenWidth * 2,
//                    NumberPenWidth,
//                    NumberPenWidth);

//                return image;
//            }

 


//        IController IControllable.GetController()
//        {
//            if (controller == null)
//                controller = new RectangleController(this);
//            return controller;
//        }

//    }
//}

