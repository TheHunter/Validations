using System.Collections.Generic;

namespace Validations
{
    /// <summary>
    /// Interface IComplexValidator
    /// </summary>
    /// <typeparam name="TLeft">The type of the TLeft.</typeparam>
    /// <typeparam name="TRight">The type of the t right.</typeparam>
    public interface IComplexValidator<in TLeft, in TRight>
        : IValidatorInfo
    {
        /// <summary>
        /// Validates the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>IEnumerable&lt;IOperationResult&gt;.</returns>
        IEnumerable<IOperationResult> Validate(TLeft left, TRight right);
    }

}
