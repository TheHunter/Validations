using System.Collections.Generic;

namespace Validations
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    public interface ISimpleValidator<TSource>
        : IValidatorInfo
    {
        /// <summary>
        /// Validate the given argument using the current set of expressions managed by the calling validator.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        IEnumerable<IOperationResult> Validate(TSource instance);
    }

}
