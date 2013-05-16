using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace Repower.Common.Validations.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    public static class DelegateCompiler
    {

        /*
         *  Occorre gestire anche metodi statici, qui sotto un esempio di come si compila un MethodInfo
         *  in Delegate.
            
            Type ccc = typeof(CurrentSessionContext);
            MethodInfo info = ccc.GetMethod("Unbind", flags);
            var _UnBindAction = (Func<ISessionFactory, ISession>)Delegate.CreateDelegate(typeof(Func<ISessionFactory, ISession>), null, info);
            
         * */

        /// <summary>
        /// Gets an Actions which serves for setting properties values of isntances.
        /// </summary>
        /// <param name="setter"></param>
        /// <returns></returns>
        public static Action<object, object> BuildSetAccessor(MethodInfo setter)
        {
            var obj = Expression.Parameter(typeof(object), "obj");
            var value = Expression.Parameter(typeof(object), "value");

            Expression<Action<object, object>> expr =
                Expression.Lambda<Action<object, object>>
                (
                    Expression.Call
                    (
                        Expression.Convert(obj, setter.DeclaringType),
                        setter,
                        Expression.Convert(value, setter.GetParameters()[0].ParameterType)
                    ),
                    obj,
                    value
                );

            return expr.Compile();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="getter"></param>
        /// <returns></returns>
        public static Func<object, object> BuildGetAccessor(MethodInfo getter)
        {
            var obj = Expression.Parameter(typeof(object), "obj");

            Expression<Func<object, object>> expr =
                Expression.Lambda<Func<object, object>>
                (
                    Expression.Call
                    (
                        Expression.Convert(obj, getter.DeclaringType),
                        getter
                    ),
                    obj
                );
            return expr.Compile();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IDictionary<string, Action<object, object>> GetDelegateSettersOf<T>()
            where T: class, new()
        {
            Type type = typeof(T);
            var flags = BindingFlags.CreateInstance | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            PropertyInfo[] properties =  type.GetProperties(flags);

            SortedDictionary<string, Action<object, object>> setters = new SortedDictionary<string, Action<object, object>>();

            properties.All
                (
                    delegate(PropertyInfo current)
                    {
                        if (current.CanWrite)
                        {
                            try
                            {
                                setters.Add(current.Name, BuildSetAccessor(current.GetSetMethod()));
                            }
                            catch (Exception)
                            {
                                // se entra qui, significa che non è stato possibile trovare il metodo setter
                                // oppure cè un problema nel compilare il setter in un delegato.
                            }
                        }
                        return true;
                    }
                );
            return setters;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="onlySetters"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetPropertyInfo<T>(bool onlySetters)
        {
            Type type = typeof(T);
            var flags = BindingFlags.CreateInstance | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            PropertyInfo[] infos = type.GetProperties(flags);

            if (onlySetters)
                return infos.Where(n => n.CanWrite);
            else
                return infos;
        }

        
    }
}
