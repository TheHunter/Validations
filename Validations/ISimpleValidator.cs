using System.Collections.Generic;

namespace Validations
{
    /// <summary>
    /// Interface ISimpleValidator
    /// </summary>
    /// <typeparam name="TSource">The type of the t source.</typeparam>
    public interface ISimpleValidator<in TSource>
        : IValidatorInfo
    {

        /// <summary>
        /// Validates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>IEnumerable&lt;IOperationResult&gt;.</returns>
        IEnumerable<IOperationResult> Validate(TSource instance);
    }

}
