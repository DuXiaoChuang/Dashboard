using System;
using System.Collections;
using System.ComponentModel;

namespace HFUTIEMES
{
	/// <summary>
	/// CustomClass (Which is binding to property grid)
	/// </summary>
	public class MonitoredInfoList: CollectionBase,ICustomTypeDescriptor
	{
		/// <summary>
		/// Add CustomProperty to Collectionbase List
		/// </summary>
		/// <param name="Value"></param>
        public void Add(MonitoredInfo Value)
		{
			base.List.Add(Value);
		}

       
		/// <summary>
		/// Remove item from List
		/// </summary>
		/// <param name="Name"></param>
		public void Remove(string Name)
		{
            foreach (MonitoredInfo prop in base.List)
			{
				if(prop.Name == Name)
				{
					base.List.Remove(prop);
					return;
				}
			}
		}

        /// <summary>
        /// Remove item from List
        /// </summary>
        /// <param name="Name"></param>
        public void RemoveCategory(string categoryName)
        {
            
            for (int i = 0; i < base.List.Count; i++)
            {
                MonitoredInfo prop = (MonitoredInfo)List[i];
                if (prop.CategoryName.Contains(categoryName))
                {
                    Remove(prop.Name);
                    i = -1;
                }
                
            }
        }

        public bool Contains(string categoryName,string Name)
        {
            foreach (MonitoredInfo prop in base.List)
            {
                if (prop.CategoryName==categoryName && prop.Name == Name)
                {
                    return true;
                }
            }
            return false;

        }

		/// <summary>
		/// Indexer
		/// </summary>
        public MonitoredInfo this[int index] 
		{
			get 
			{
                return (MonitoredInfo)base.List[index];
			}
			set
			{
                base.List[index] = (MonitoredInfo)value;
			}
		}

		#region "TypeDescriptor Implementation"
		/// <summary>
		/// Get Class Name
		/// </summary>
		/// <returns>String</returns>
		public String GetClassName()
		{
			return TypeDescriptor.GetClassName(this,true);
		}

		/// <summary>
		/// GetAttributes
		/// </summary>
		/// <returns>AttributeCollection</returns>
		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this,true);
		}

		/// <summary>
		/// GetComponentName
		/// </summary>
		/// <returns>String</returns>
		public String GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		/// <summary>
		/// GetConverter
		/// </summary>
		/// <returns>TypeConverter</returns>
		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		/// <summary>
		/// GetDefaultEvent
		/// </summary>
		/// <returns>EventDescriptor</returns>
		public EventDescriptor GetDefaultEvent() 
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		/// <summary>
		/// GetDefaultProperty
		/// </summary>
		/// <returns>PropertyDescriptor</returns>
		public PropertyDescriptor GetDefaultProperty() 
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		/// <summary>
		/// GetEditor
		/// </summary>
		/// <param name="editorBaseType">editorBaseType</param>
		/// <returns>object</returns>
		public object GetEditor(Type editorBaseType) 
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		public EventDescriptorCollection GetEvents(Attribute[] attributes) 
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			PropertyDescriptor[] newProps = new PropertyDescriptor[this.Count];
			for (int i = 0; i < this.Count; i++)
			{

                MonitoredInfo prop = (MonitoredInfo)this[i];
				newProps[i] = new CustomPropertyDescriptor(ref prop, attributes);
			}

			return new PropertyDescriptorCollection(newProps);
		}

		public PropertyDescriptorCollection GetProperties()
		{
			
			return TypeDescriptor.GetProperties(this, true);
			
		}

		public object GetPropertyOwner(PropertyDescriptor pd) 
		{
			return this;
		}
		#endregion
	
	}

	/// <summary>
	/// Custom property class 
	/// </summary>
	public class MonitoredInfo
	{
		private string sName = string.Empty;
		private bool bReadOnly = false;
		private bool bVisible = true;
		private object objValue = null;
        private string categoryName = string.Empty;

        public MonitoredInfo( string categoryName,string sName ,object value)
		{
			this.sName = sName;
			this.objValue = value;
            this.categoryName = categoryName;
		}

		public bool ReadOnly
		{
			get
			{
				return bReadOnly;
			}
		}
        public string CategoryName
        {
            get
            {
                return categoryName;
            }
        }

		public string Name
		{
			get
			{
				return sName;
			}
		}

		public bool Visible
		{
			get
			{
				return bVisible;
			}
		}

		public object Value
		{
			get
			{
				return objValue;
			}
			set
			{
				objValue = value;
			}
		}

	}

	/// <summary>
	/// Custom PropertyDescriptor
	/// </summary>
	public class CustomPropertyDescriptor: PropertyDescriptor
	{
        MonitoredInfo m_Property;
        public CustomPropertyDescriptor(ref MonitoredInfo myProperty, Attribute[] attrs)
            : base(myProperty.Name, attrs)
		{
			m_Property = myProperty;
		}

		#region PropertyDescriptor specific
		
		public override bool CanResetValue(object component)
		{
			return false;
		}

		public override Type ComponentType
		{
			get 
			{
				return null;
			}
		}

		public override object GetValue(object component)
		{
			return m_Property.Value;
		}

		public override string Description
		{
			get
			{
				return m_Property.Name;
			}
		}
		
		public override string Category
		{
			get
			{
				return m_Property.CategoryName ;
			}
		}

		public override string DisplayName
		{
			get
			{
				return m_Property.Name;
			}
			
		}

	

		public override bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		public override void ResetValue(object component)
		{
			//Have to implement
		}

		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		public override void SetValue(object component, object value)
		{
			m_Property.Value = value;
		}

		public override Type PropertyType
		{
			get { return m_Property.Value.GetType(); }
		}

		#endregion

			
	}
}
