using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Repower.Common.Validations.Reflection
{
    /// <summary>
    /// A dynamic class which sets properties values a runtime.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DynamicSetter<T>
        where T: class, new()
    {

        private readonly HashSet<DynamicProperty> _PropSetters = null;

        /// <summary>
        /// 
        /// </summary>
        public DynamicSetter()
        {
            IEnumerable<PropertyInfo> infos = DynamicSetter<T>.GetPropertiesInfo();

            _PropSetters = new HashSet<DynamicProperty>();
            Action<object, object> action = null;
            MethodInfo setter = null;

            infos.All
                (
                    delegate(PropertyInfo info)
                    {
                        setter = info.GetSetMethod() ?? info.GetSetMethod(true);
                        action = DelegateCompiler.BuildSetAccessor(setter);
                        _PropSetters.Add(new DynamicProperty(info.Name, info.PropertyType, action));
                        return true;
                    }
                );
        }

        /// <summary>
        /// 
        /// </summary>
        public DynamicSetter(IDictionary<string, string> aliases)
        {
            IEnumerable<PropertyInfo> infos = DynamicSetter<T>.GetPropertiesInfo();

            DynamicSetter<T>.CheckAliases(aliases);

            _PropSetters = new HashSet<DynamicProperty>();
            string currentAlias = null;
            Action<object, object> action = null;
            MethodInfo setter = null;

            infos.All
                (
                    delegate(PropertyInfo info)
                    {
                        if (aliases.ContainsKey(info.Name))
                            currentAlias = aliases[info.Name];
                        else
                            currentAlias = info.Name;

                        setter = info.GetSetMethod() ?? info.GetSetMethod(true);
                        action = DelegateCompiler.BuildSetAccessor(setter);
                        _PropSetters.Add(new DynamicProperty(currentAlias, info.PropertyType, action));
                        return true;
                    }
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<PropertyInfo> GetPropertiesInfo()
        {
            IEnumerable<PropertyInfo> infos = DelegateCompiler.GetPropertyInfo<T>(true);
            if (infos.Count() == 0)
                throw new MissingMemberException("No properties members for this class, name: " + typeof(T).Name);

            return infos;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aliases"></param>
        private static void CheckAliases(IDictionary<string, string> aliases)
        {
            if (aliases == null || aliases.Count == 0)
                throw new ArgumentException("Aliases cannot be null or empty");

            var current = aliases.Where(n => string.IsNullOrEmpty(n.Value));

            if (current.Any())
                throw new ArgumentException(string.Format("Aliases cannot be empty or null, wrong aliases: {0}", string.Join(" - ", current.Select(n => string.Format("[{0}]", n.Key)).ToArray())));
        }

        /// <summary>
        /// Initialize a new object with all values indicated into dictionary.
        /// </summary>
        /// <param name="propertyValues"></param>
        /// <returns></returns>
        public T SetPropertiesFrom(IDictionary<string, object> propertyValues)
        {
            if (propertyValues == null)
                throw new ArgumentNullException();

            T instance = MakeInstance();
            DynamicProperty action = null;

            propertyValues.All
                (
                    delegate(KeyValuePair<string, object> current)
                    {
                        try
                        {
                            action = _PropSetters.FirstOrDefault(n => n.Alias.Equals(current.Key));
                            if (action != null)
                            {
                                action.Setter(instance, current.Value);
                            }
                            else
                            {
                                throw new MissingMemberException("Alias doesn't corrispond to any property setter, name: " + current.Key);
                            }
                        }
                        catch (MissingMemberException ex)
                        {
                            // significa che il nome della property non è corretto oppure non esiste.
                            throw ex;
                        }
                        catch (Exception)
                        {
                            //
                        }
                        return true;
                    }
                );

            return instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static T MakeInstance()
        {
            return new T();
        }
    }
}
