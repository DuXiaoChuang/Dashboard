using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Collections.Generic;

namespace Dalssoft.DiagramNet
{
    [Serializable,
    TypeConverter(typeof(ExpandableObjectConverter))]
    public class EquipmentElement : BaseElement, IControllable, ILabelElement
    {
        [NonSerialized]
        private RectangleController controller;
        protected Image imageDefault = Diagram.NET.Resource.equipOff;
        protected Image imageWorking = Diagram.NET.Resource.equipOn;
        protected Image imageAlert = Diagram.NET.Resource.equipAlert;
        protected Image imageNetBreak = Diagram.NET.Resource.equipAlert;
        protected LabelElement label = new LabelElement();
        private volatile bool IsTwinkle = false;
        protected string ipAddress = "";
        protected equip_style elementEquip_style = equip_style.区域设备;

        //[CategoryAttribute("监控信息")]
        //[DescriptionAttribute("设备类型")]
        //[RefreshProperties(RefreshProperties.All)]
        //public virtual equip_style 设备类型
        //{
        //    get
        //    {
        //        return elementEquip_style;
        //    }
        //    set
        //    {
        //        elementEquip_style = value;
        //        if (elementEquip_style == equip_style.单台设备)
        //        {
        //            SQL = "select distinct a.equip_key,a.equip_name,b.equip_mc_u_S from EQUIPMENT a,UDA_Equipment b where a.equip_key = b.object_key";
        //        }
        //        else
        //        {
        //            SQL = "select distinct atr_key,zone_code_S,zone_name_S from AT_QUE_ZONE_B";
        //        }

        //        DynamicProps.ListAttribute attributes = new DynamicProps.ListAttribute(SQL);
        //        this.监控对象 = attributes.codeNameCollection[0].ToString();

        //        OnAppearanceChanged(new EventArgs());
        //    }
        //}

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
        //[DescriptionAttribute("IP地址")]
        //[RefreshProperties(RefreshProperties.All)]
        //public virtual string IP地址
        //{
        //    get
        //    {
        //        return ipAddress;
        //    }
        //    set
        //    {
        //        string pattern = @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$";
        //        if (System.Text.RegularExpressions.Regex.IsMatch(value, pattern))
        //        {
        //            if (ClassLib.IsIPAddressExisted(value))
        //            {
        //                throw new Exception(@"'" + value + "'--IP地址已存在,请重新配置! ");
        //            }
        //            else
        //            {
        //                ipAddress = value;
        //                OnAppearanceChanged(new EventArgs());
        //            }
        //        }
        //        else
        //        {
        //            throw new Exception(@"'" + value + "'不是有效的IP地址格式! ");

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

        public EquipmentElement(): this(0, 0, 100, 100)
        { }

        public EquipmentElement(Rectangle rec)
            : this(rec.Location, rec.Size)
        {
            elementMonitoredType = MonitoredType.设备;
        }

        public EquipmentElement(Point l, Size s): this(l.X, l.Y, s.Width, s.Height)
        { }

        public EquipmentElement(int top, int left, int width, int height)
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
                    {
                        tmpImage = imageDefault;
                        g.DrawImage(tmpImage, r.Location.X, r.Location.Y, r.Size.Width, r.Size.Height);
                    }
                    break;
                case 1:
                    {
                        IsTwinkle = !IsTwinkle;
                        if (!IsTwinkle)
                        {
                            tmpImage = imageDefault;
                            g.DrawImage(tmpImage, r.Location.X, r.Location.Y, r.Size.Width, r.Size.Height);
                        }
                        else
                        {
                            tmpImage = imageAlert;
                            g.DrawImage(tmpImage, r.Location.X, r.Location.Y, r.Size.Width, r.Size.Height);
                            if (borderWidth != 0)
                                g.DrawRectangle(new Pen(Color.Red, borderWidth), r);
                        }
                        break;
                    }
                case 2:
                    {
                        tmpImage = imageWorking;
                        g.DrawImage(tmpImage, r.Location.X, r.Location.Y, r.Size.Width, r.Size.Height);
                        if (borderWidth != 0)
                            g.DrawRectangle(new Pen(Color.Green, borderWidth), r);
                    }
                    break;
            //    case 3:
            //        {
            //            IsTwinkle = !IsTwinkle;
            //            if (!IsTwinkle)
            //            {
            //                tmpImage = imageDefault;
            //                g.DrawImage(tmpImage, r.Location.X, r.Location.Y, r.Size.Width, r.Size.Height);
            //            }
            //            else
            //            {
            //                tmpImage = imageAlert;
            //                g.DrawImage(tmpImage, r.Location.X, r.Location.Y, r.Size.Width, r.Size.Height);
            //                if (borderWidth != 0)
            //                    g.DrawRectangle(new Pen(Color.Blue, borderWidth), r);
            //            }
            //        }
            //        break;
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
                    tmpImage = Diagram.NET.Resource.equipOff;
                    break;
                //case 1:
                //    tmpImage = Diagram.NET.Resource.equipAlert;
                //    break;
                //case 2:
                //    tmpImage = Diagram.NET.Resource.equipOn;
                //    break;
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
