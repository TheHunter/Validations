using System.Collections.Generic;

namespace Validations
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Tleft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    public interface IComplexValidator<Tleft, TRight>
        : IValidatorInfo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        IEnumerable<IOperationResult> Validate(Tleft left, TRight right);
    }

}
