using System;
using Repower.Common.Validations;

namespace Validations
{
    /// <summary>
    /// 
    /// </summary>
    public interface IComplexOperation<TLeft, TRight>
        : IOperationInfo
        where TLeft : class
        where TRight : class
    {
        /// <summary>
        /// 
        /// </summary>
        Func<TLeft, TRight, bool> Verifier { get; }
    }
}
