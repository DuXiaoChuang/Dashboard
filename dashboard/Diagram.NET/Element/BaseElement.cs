using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Xml;

namespace Dalssoft.DiagramNet
{
	/// <summary>
	/// This is the base for all element the will be draw on the
	/// document.
	/// </summary>
	[Serializable]
    public abstract class BaseElement: ICustomTypeDescriptor
	{
		protected Point location;
		protected Size size;
		protected bool visible = true;
		protected Color borderColor = Color.Black;
        protected int borderWidth = 1;
		protected int opacity = 100;
		internal protected Rectangle invalidateRec = Rectangle.Empty;
        public bool IsInvalidated = true;
        
        protected string id = "";
        protected ObjectState state = ObjectState.无;
        protected ObjectState2 state2 = ObjectState2.image1不亮image2不亮;
        protected string monitorObject = "";
        protected MonitoredType elementMonitoredType = MonitoredType.无;
        protected TextShownMode textShownMode = TextShownMode.编号;
        //protected andon  elementgu_name1 = andon.无  ;
        protected gu_name elementgu_name = gu_name.无;
        protected string monitoredObjectID = "";
        protected string monitoredObjectCode = "";
        protected string monitoredObjectName = "";
        
        protected string sql = "";
        protected string sql1 = "";
        //protected Image person = Diagram.NET.Resource.personnel;

        #region 监控信息
        [Browsable(false)]
        public virtual string SQL
        {
            get
            {
                // 1 设备，27 工位模型，3 ANDON，35 工位ANDON，4 ANDON统计，6 人员,7 区域，12 工控机,20 andon控件,28 股 ,37 设备状态，40 上线数量,43-刀具状态；44-刀槽
                int index = Convert.ToInt32(elementMonitoredType);
                if (index == 1)
                {
                    sql = "select distinct equip_key,equip_code,equip_name from EQUIPMENT ";
                }
                else if (index == 37 || index == 43 || index == 44 )
                {
                    sql = "select distinct EquipId,EquipCode,EquipName from Equipment_B ";
                }
                else if (index == 35 || index == 36)
                {
                    sql = "select distinct station_key,station_code,station_name from STATION";
                }
                else if (index == 40 || index == 38 || index == 39)
                {
                    sql = "select distinct ProlineId,ProlineCode,ProlineName from ProductionLine_B ";
                }
                else if (index == 7)
                {
                    sql = "select distinct canvas_ID,[canvas_code],[canvas_name] from [AT_CANVAS_B]";
                }

                else if (index == 28)
                {
                    sql = "select distinct p_line_key,p_line_code,p_line_name from PRODUCTION_LINE ";
                }
                return sql;
            }
            set
            {
                sql = value;
            }
        }
         
        [Browsable(false)]
        [RefreshProperties(RefreshProperties.All)]
        public virtual bool CanBeMonitored
        {
            get
            {
                return Convert.ToInt32(elementMonitoredType) != 0;
            }
        }

        [CategoryAttribute("监控信息")]
        [DescriptionAttribute("监控对象")]
        [DynamicProps.DynamicList("SQL")]
        [TypeConverterAttribute(typeof(DynamicProps.NameConverter))]
        [DynamicProps.DynamicBrowsable("CanBeMonitored")]
        [RefreshProperties(RefreshProperties.All)]
        public virtual string 监控对象
        {

            get
            {
                return monitorObject;
            }

            set
            {
                monitorObject = value;
                OnAppearanceChanged(new EventArgs());
            }
        }

        [ReadOnly(true), Category("监控信息")]
        [Description("监控类型")]
        [DefaultValue(Dalssoft.DiagramNet.MonitoredType.无)]
        [RefreshProperties(RefreshProperties.All)]
        public virtual Dalssoft.DiagramNet.MonitoredType 监控类型
        {
            get
            {
                return elementMonitoredType;
            }
            set
            {
                elementMonitoredType = value;
                OnAppearanceChanged(new EventArgs());
            }
        }
        //[ReadOnly(true), Category("监控信息")]
        //[Description("股名称")]
        //[DefaultValue(Dalssoft.DiagramNet.gu_name.无  )]
        //[RefreshProperties(RefreshProperties.All)]
        //public virtual Dalssoft.DiagramNet.gu_name 股名称
        //{
        //    get
        //    {
        //        return elementgu_name;
        //    }
        //    set
        //    {
        //        elementgu_name = value;
        //        OnAppearanceChanged(new EventArgs());
        //    }
        //}

        [Browsable(false)]
        public virtual string MoniteredObjectID
        {
            get
            {
                if (monitorObject == string.Empty)
                {
                    monitoredObjectID = string.Empty;
                }
                return monitoredObjectID;
            }
            set
            {
                monitoredObjectID = value;
            }
        }

        [Browsable(false)]
        public virtual string MoniteredObjectCode
        {
            get
            {
                if (monitorObject == string.Empty)
                {
                    monitoredObjectCode = string.Empty;
                }
                return monitoredObjectCode;
            }
            set
            {
                monitoredObjectCode = value;
            }
        }

        [Browsable(false)]
        public virtual string MoniteredObjectName
        {
            get
            {
                if (monitorObject == string.Empty)
                {
                    monitoredObjectName = string.Empty;
                }
                return monitoredObjectName;
            }
            set
            {
                monitoredObjectName = value;
            }
        }
        
       
        #endregion

        #region 设计
        [Browsable(false)]
        public virtual ObjectState State
        {
            get
            {

                return state;
            }
            set
            {
                state = value;
                //OnAppearanceChanged(new EventArgs());
            }
        }
        [Browsable(false)]
        public virtual ObjectState2 State2
        {
            get
            {

                return state2;
            }
            set
            {
                state2 = value;
                //OnAppearanceChanged(new EventArgs());
            }
        }


        [ReadOnly(true)]
        [Category("设计")]
        [Description("唯一标识")]
        public virtual string ID
        {
            get
            {

                return id;
            }
            set
            {

                id = value;
                OnAppearanceChanged(new EventArgs());
            }
        }

        [Category("外观")]
        [Description("起始位置")]
        public virtual Point Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
                OnAppearanceChanged(new EventArgs());
            }
        }

        [Category("外观")]
        [Description("大小")]
        public virtual Size Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
                OnAppearanceChanged(new EventArgs());
            }
        }

        [Category("设计")]
        [Description("可见性")]
        [DefaultValue(true)]
        [ReadOnly(false)]
        public virtual bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                visible = value;
                OnAppearanceChanged(new EventArgs());
            }
        }

        [Category("外观")]
        [Description("线条颜色")]
        public virtual Color BorderColor
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

        [Category("外观")]
        [Description("线宽")]
        [DefaultValue(1)]
        public virtual int BorderWidth
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
      
        [Category("外观")]
        [Description("设置透明度,取值范围:0-100。")]
        [DefaultValue(0)]
        public virtual int Opacity
        {
            get
            {
                if (opacity > 100)
                    return 100;
                else if (opacity < 0)
                    return 0;
                else
                    return opacity;
            }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                else if (value > 100)
                {
                    value = 100;
                }
                opacity = value;
                OnAppearanceChanged(new EventArgs());
            }
        }
        #endregion 

        public BaseElement()
		{
		}

		protected BaseElement(int top, int left, int width, int height)
		{
			location  = new Point(top, left);
			size = new Size(width, height);
		}

		internal virtual void Draw(Graphics g)
		{
			IsInvalidated = false;
		}

        internal virtual void DrawAlert(Graphics g)//有传进来的参数，但返回值为空
        {
            IsInvalidated = false;
        }

		public virtual void Invalidate()
		{
			if (IsInvalidated)
				invalidateRec = Rectangle.Union(invalidateRec, GetUnsignedRectangle());
			else
				invalidateRec = GetUnsignedRectangle();

			IsInvalidated = true;
		}

		public virtual Rectangle GetRectangle()
		{
			return new Rectangle(this.Location, this.Size);
		}

		public virtual Rectangle GetUnsignedRectangle()
		{
			
			return GetUnsignedRectangle(GetRectangle());
		}

        internal static Rectangle GetUnsignedRectangle(Rectangle rec)
        {
            Rectangle retRectangle = rec;
            if (rec.Width < 0)
            {
                retRectangle.X = rec.X + rec.Width;
                retRectangle.Width = -rec.Width;
            }

            if (rec.Height < 0)
            {
                retRectangle.Y = rec.Y + rec.Height;
                retRectangle.Height = -rec.Height;
            }

            return retRectangle;
        }

		#region Events
		[field: NonSerialized]
		public event EventHandler AppearanceChanged;

		protected virtual void OnAppearanceChanged(EventArgs e)
		{
			if (AppearanceChanged != null)
				AppearanceChanged(this, e);
		}
		#endregion

        #region ICustomTypeDescriptor Members

        public TypeConverter GetConverter()
        {
            // TODO:  Add PropertyClassDescriptor.GetConverter implementation
            return TypeDescriptor.GetConverter(this, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            // TODO:  Add PropertyClassDescriptor.GetEvents implementation
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            // TODO:  Add PropertyClassDescriptor.System.ComponentModel.ICustomTypeDescriptor.GetEvents implementation
            return TypeDescriptor.GetEvents(this, true);
        }

        public string GetComponentName()
        {
            // TODO:  Add PropertyClassDescriptor.GetComponentName implementation
            return TypeDescriptor.GetComponentName(this, true);
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            // TODO:  Add PropertyClassDescriptor.GetPropertyOwner implementation
            return this;
        }

        public AttributeCollection GetAttributes()
        {
            // TODO:  Add PropertyClassDescriptor.GetAttributes implementation
            return TypeDescriptor.GetAttributes(this, true);
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            // TODO:  Add PropertyClassDescriptor.GetProperties implementation
            return this.GetProperties();
        }

        public PropertyDescriptorCollection GetProperties()
        {
            // use new implemented class to get properties
            return DynamicProps.DynamicTypeDescriptor.GetProperties(this);
        }

        public object GetEditor(Type editorBaseType)
        {
            // TODO:  Add PropertyClassDescriptor.GetEditor implementation
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            // TODO:  Add PropertyClassDescriptor.GetDefaultProperty implementation
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            // TODO:  Add PropertyClassDescriptor.GetDefaultEvent implementation
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public string GetClassName()
        {
            // TODO:  Add PropertyClassDescriptor.GetClassName implementation
            return TypeDescriptor.GetClassName(this, true);
        }

        #endregion
	}
}
