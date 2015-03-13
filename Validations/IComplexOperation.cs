using System;

namespace Validations
{
    /// <summary>
    /// Interface IComplexOperation
    /// </summary>
    /// <typeparam name="TLeft">The type of the t left.</typeparam>
    /// <typeparam name="TRight">The type of the t right.</typeparam>
    public interface IComplexOperation<in TLeft, in TRight>
        : IOperationInfo
        where TLeft : class
        where TRight : class
    {
        /// <summary>
        /// Gets the verifier.
        /// </summary>
        /// <value>The verifier.</value>
        Func<TLeft, TRight, bool> Verifier { get; }
    }
}
