namespace Validations.Exceptions
{
    /// <summary>
    /// Class OnBuildingOperationException.
    /// </summary>
    public class OnBuildingOperationException
        : WrongParameterException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WrongParameterException" /> class.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="message">The message.</param>
        public OnBuildingOperationException(string parameterName, string message)
            : base(parameterName, message)
        {
        }
    }
}
