using System.Linq;

namespace Validations.Exceptions
{
    /// <summary>
    /// Class OnBuildingValidatorException.
    /// </summary>
    public class OnBuildingValidatorException
        : WrongParameterException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WrongParameterException" /> class.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="message">The message.</param>
        public OnBuildingValidatorException(string parameterName, string message)
            : base(parameterName, message)
        {
        }
    }
}
