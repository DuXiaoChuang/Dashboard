using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Dalssoft.DiagramNet;
using System.Drawing.Design;
using System.ComponentModel;
using System.Data;

namespace Dalssoft.DiagramNet
{
    [Serializable]
    public class Temperature : BaseElement, IControllable, ILabelElement
    {
        [NonSerialized]
        private RectangleController controller;
        protected LabelElement label = new LabelElement();
        protected Statistics_type statisticstyle = Statistics_type.无;
        [TypeConverterAttribute(typeof(DynamicProps.NameConverter))]
        [RefreshProperties(RefreshProperties.All)]
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
        [Category("外观")]
        [Description("label")]
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
        public Temperature()
            : this(0, 0, 100, 100)
        { }
        public Temperature(Rectangle rec)
            : this(rec.Location, rec.Size)
        {
            elementMonitoredType = MonitoredType.温度;
            borderColor = Color.LimeGreen;
            borderWidth = 3;
        }
        public Temperature(Point l, Size s)
            : this(l.X, l.Y, s.Width, s.Height)
        { }
        public Temperature(int top, int left, int width, int height)
        {
            location = new Point(top, left);
            size = new Size(width, height);
        }
        internal override void Draw(Graphics g)
        {
            IsInvalidated = false;
            Rectangle r = GetUnsignedRectangle(
                new Rectangle(
                location.X, location.Y,
                size.Width, size.Height));
            DrawBorder(g,r);
            Random ran = new Random();
            int a = ran.Next(20, 25);
            string b = a.ToString();
            label.Text = "" + b + "°C";
        }
        //    int[] arr = getRandomNum(12, 15 ,35); //从15至35中取出12个互不相同的随机数
        //    int i = 0;
        //    string temp = "";
        //    while (i <= arr.Length - 1)
        //    {
        //        temp += arr[i].ToString() + "\n";
        //        i++;
        //    }
        //    while (i<= arr.Length - 1)
        //    {
        //        Rectangle ri = GetUnsignedRectangle(
        //        new Rectangle(
        //        location.X, location.Y,
        //        size.Width, size.Height));
        //        DrawBorder(g, ri);
        //        label.Text = arr[i].ToString(); //显示在labe
        //    }
        //    //label.Text = temp; //显示在label1中
        //}
        //public int[] getRandomNum(int num, int minValue, int maxValue)

        //{

        //    Random ra = new Random(unchecked((int)DateTime.Now.Ticks));

        //    int[] arrNum = new int[num];

        //    int tmp = 0;

        //    for (int i = 0; i <= num - 1; i++)
        //    {

        //        tmp = ra.Next(minValue, maxValue); //随机取数

        //        arrNum[i] = getNum(arrNum, tmp, minValue, maxValue, ra); //取出值赋到数组中

        //    }

        //    return arrNum;

        //}
        //public int getNum(int[] arrNum, int tmp, int minValue, int maxValue, Random ra)
        //{

        //    int n = 0;

        //    while (n <= arrNum.Length - 1)

        //    {

        //        if (arrNum[n] == tmp) //利用循环判断是否有重复

        //        {

        //            tmp = ra.Next(minValue, maxValue); //重新随机获取。

        //            getNum(arrNum, tmp, minValue, maxValue, ra);//递归:如果取出来的数字和已取得的数字有重复就重新随机获取。

        //        }

        //        n++;

        //    }

        //    return tmp;

        //}

        protected virtual void DrawBorder(Graphics g, Rectangle r)
        {
            Pen p = new Pen(borderColor, borderWidth);
            g.DrawRectangle(p, r);
            p.Dispose();
        }
        IController IControllable.GetController()
        {
            if (controller == null)
                controller = new CommentBoxController(this);
            return controller;
        }
    }
}
