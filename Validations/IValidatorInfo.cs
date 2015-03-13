using System.Collections.Generic;

namespace Validations
{
    /// <summary>
    /// Interface IValidatorInfo
    /// </summary>
    public interface IValidatorInfo
    {
        /// <summary>
        /// Gets a target which identify the current Validator.
        /// </summary>
        /// <value>The target of the calling validator.</value>
        string TypeInfo { get; }

        /// <summary>
        /// Gets the operations.
        /// </summary>
        /// <value>The operations.</value>
        IEnumerable<IOperationInfo> Operations { get; }
    }
}
