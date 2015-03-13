using System;

namespace Validations.Exceptions
{
    /// <summary>
    /// Class WrongParameterException.
    /// </summary>
    public class WrongParameterException
        : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WrongParameterException"/> class.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="message">The message.</param>
        public WrongParameterException(string parameterName, string message)
            : base(message, parameterName)
        {   
        }
    }
}
