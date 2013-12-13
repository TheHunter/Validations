namespace Validations.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class OnBuildingValidatorException
        : WrongParameterException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="message"></param>
        public OnBuildingValidatorException(string parameterName, string message)
            : base(parameterName, message)
        {
        }
    }
}
