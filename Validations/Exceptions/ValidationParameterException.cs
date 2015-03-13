using System.Collections.Generic;

namespace Validations.Exceptions
{
    /// <summary>
    /// Class ValidationParameterException.
    /// </summary>
    public class ValidationParameterException
        : WrongParameterException
    {
        private readonly IList<string> wrongParameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationParameterException" /> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public ValidationParameterException(string message)
            : base("instance", message)
        {
            this.wrongParameters = new List<string> { "instance" };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationParameterException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="wrongParameters">The wrong parameters.</param>
        public ValidationParameterException(string message, IEnumerable<string> wrongParameters)
            : base("instancesToValidate", message)
        {
            this.wrongParameters = new List<string>(wrongParameters);
        }

        /// <summary>
        /// Gets the wrong parameters.
        /// </summary>
        /// <value>The wrong parameters.</value>
        public IEnumerable<string> WrongParameters
        {
            get { return this.wrongParameters; }
        }
    }
}
