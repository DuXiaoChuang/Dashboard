using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Collections.Generic;

namespace Dalssoft.DiagramNet
{
    [Serializable]
    public class PersonnelElement : BaseElement, IControllable, ILabelElement
    {
        [NonSerialized]
        private RectangleController controller;

        protected Image image = Diagram.NET.Resource.personnel;
        protected LabelElement label = new LabelElement();

 
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
        //            string text = "";
        //            switch (Convert.ToInt32(textShownMode))
        //            {
        //                case 0:
        //                    text = "";
        //                    break;
        //                case 1:
        //                    text = attributes.nameCollection[index].ToString();
        //                    break;
        //                case 2:
        //                    text = attributes.codeCollection[index].ToString();
        //                    break;
        //            }
        //            label.Text = text;
        //            monitoredObjectID = attributes.idCollection[index].ToString();
        //            monitoredObjectCode = attributes.codeCollection[index].ToString();
        //            monitoredObjectName = attributes.nameCollection[index].ToString();
        //            TextAutoSize(label, this);
        //            OnAppearanceChanged(new EventArgs());
        //        }
        //    }
        //}


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

        public PersonnelElement(): this(0, 0, 100, 100)
		{}

		public PersonnelElement(Rectangle rec): this(rec.Location, rec.Size)
        { elementMonitoredType = MonitoredType.人员; }
		
		public PersonnelElement(Point l, Size s): this(l.X, l.Y, s.Width, s.Height) 
		{}

        public PersonnelElement(int top, int left, int width, int height)
		{
			location  = new Point(top, left);
			size = new Size(width, height);
        }

        internal override void Draw(Graphics g)
        {
            IsInvalidated = false;
            Image tmpImage = image;
            Rectangle r = GetUnsignedRectangle(
                new Rectangle(
                location.X, location.Y,
                size.Width, size.Height));


            #region 图片
            switch ((int)state)
            {
                case 0:
                    tmpImage = image;
                    break;
                case 1:
                    tmpImage = image;
                    break;
                case 2:
                    tmpImage = image;
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
            Image tmpImage = image;
            Rectangle r = GetUnsignedRectangle(
                new Rectangle(
                location.X, location.Y,
                size.Width, size.Height));


            #region 图片
            switch ((int)state)
            {
                case 0:
                    tmpImage = image;
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
