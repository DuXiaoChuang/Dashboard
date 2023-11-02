using System;
using System.Reflection;
using System.ComponentModel;

namespace DynamicProps
{
	/// <summary>
	/// Base class for all dynamic attributes.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public abstract class DependsOnPropertyAttribute : Attribute
	{
		/// <summary>
		/// Create new instance of class
		/// </summary>
		/// <param name="expression">Property name</param>
		protected DependsOnPropertyAttribute(string property) : base()
		{
			_property = property;
			_index    = null;
		}
        //protected DependsOnPropertyAttribute(string property[])
        //    : base()
        //{
        //    _property = property;
        //    _index = null;
        //}
		/// <summary>
		/// Create new instance of class
		/// </summary>
		/// <param name="property">Property name</param>
		/// <param name="index">Property element index</param>
		protected DependsOnPropertyAttribute(string property, int index) 
		{
			_property = property;
			_index    = new object[] {index};
		}
        protected DependsOnPropertyAttribute(string[] MemberList, int index)
        {
            memberList = MemberList;
            _index = new object[] { index };
        }

        private string[] memberList;
		private string   _property;
		private object[] _index;
	
		/// <summary>
		/// Evaluate attribute using property container supplied
		/// </summary>
		/// <param name="container">Object that contains property to evaluate</param>
		/// <returns>Dynamically evaluated attribute</returns>
		public Attribute Evaluate(object container) 
		{
			return OnEvaluateCoplete(RuntimeEvaluator.Eval(container, _property, _index));
		}
		/// <summary>
		/// Specific dynamic attribute check implementation
		/// </summary>
		/// <param name="value">Evaluated value</param>
		/// <returns>Dynamically evaluated attribute</returns>
		protected abstract Attribute OnEvaluateCoplete(object value);

		private class RuntimeEvaluator 
		{
			public static object Eval(object container, string property, object[] index) 
			{
				PropertyInfo pInfo = container.GetType().GetProperty(property);
				if (pInfo != null)
					return pInfo.GetValue(container, index);
				
				return null;
			}
		}
	}
	public class DynamicReadonlyAttribute : DependsOnPropertyAttribute 
	{
		public DynamicReadonlyAttribute(string property) : base (property) {}
		public DynamicReadonlyAttribute(string property, int index) : base (property, index) {}
		protected override Attribute OnEvaluateCoplete(object value)
		{
			Attribute output;
			try
			{
				// check if value is provided
				if (value == null) 
					value = false; // asume default
				// create attribute
				output = new ReadOnlyAttribute((bool)value);
			}
			catch
			{
				output = new ReadOnlyAttribute(false);
			}
			return output;
		}

	}
	public class DynamicBrowsableAttribute : DependsOnPropertyAttribute 
	{
		public DynamicBrowsableAttribute(string property) : base (property) {}
		public DynamicBrowsableAttribute(string property, int index) : base (property, index) {}
		protected override Attribute OnEvaluateCoplete(object value)
		{
			Attribute output;
			try
			{
				// check if value is provided
				if (value == null) 
					value = true; // asume default
				// create attribute
				output = new BrowsableAttribute((bool)value);
			}
			catch
			{
				output = new ReadOnlyAttribute(true);
			}
			return output;
		}


	}
    public class DynamicListAttribute : DependsOnPropertyAttribute
    {
        public DynamicListAttribute(string property) : base(property) { }
        public DynamicListAttribute(string property, int index) : base(property, index) { }
        protected override Attribute OnEvaluateCoplete(object value)
        {
            Attribute output =null;
            try
            {
                if (value == null)
                    value = true; // asume default
                 
                output = new DynamicProps.ListAttribute(value.ToString());
            }
            catch
            {
                output = new DynamicProps.ListAttribute();
            }
            return output;
        }


    }
}
