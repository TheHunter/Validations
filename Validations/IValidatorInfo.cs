using System.Collections.Generic;

namespace Validations
{
    /// <summary>
    /// 
    /// </summary>
    public interface IValidatorInfo
    {
        /// <summary>
        /// A target which identify the current Validator.
        /// </summary>
        /// <value>The target of the calling validator.</value>
        string TypeInfo { get; }

        /// <summary>
        /// 
        /// </summary>
        IEnumerable<IOperationInfo> Operations { get; }
    }
}
