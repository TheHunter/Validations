using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repower.Common.Validations.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    public class DynamicProperty
    {
        private readonly string _Alias = null;
        private readonly Type _PropertyType = null;
        private readonly Action<object, object> _Setter = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="propertyType"></param>
        /// <param name="setter"></param>
        internal protected DynamicProperty(string alias, Type propertyType, Action<object, object> setter)
        {
            if (string.IsNullOrEmpty(alias))
                throw new ArgumentNullException("Name of the current dynamic property cannot be null.");

            if (propertyType == null)
                throw new ArgumentNullException("Property type cannot be null.");

            if (setter == null)
                throw new ArgumentNullException("Setter delegate cannot be null.");

            this._Alias = alias;
            this._PropertyType = propertyType;
            this._Setter = setter;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Alias
        {
            get { return this._Alias; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Type PropertyType
        {
            get { return this._PropertyType; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Action<object, object> Setter
        {
            get { return this._Setter; }
        }

        #region overriding object methods.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
        #endregion

        
    }
}
