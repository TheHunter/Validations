using System;

namespace Validations
{
    /// <summary>
    /// Interface ISimpleOperation
    /// </summary>
    /// <typeparam name="TSource">The type of the t source.</typeparam>
    public interface ISimpleOperation<TSource>
        : IOperationInfo
        where TSource : class 
    {
        /// <summary>
        /// Gets the verifier.
        /// </summary>
        /// <value>The verifier.</value>
        Func<TSource, bool> Verifier { get; }
    }
}
