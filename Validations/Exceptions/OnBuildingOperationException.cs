namespace Validations.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class OnBuildingOperationException
        : WrongParameterException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="message"></param>
        public OnBuildingOperationException(string parameterName, string message)
            : base(parameterName, message)
        {
        }
    }
}
