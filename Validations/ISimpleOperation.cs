using System;

namespace Validations
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISimpleOperation<TSource>
        : IOperationInfo
        where TSource : class 
    {
        /// <summary>
        /// 
        /// </summary>
        Func<TSource, bool> Verifier { get; }
    }
}
