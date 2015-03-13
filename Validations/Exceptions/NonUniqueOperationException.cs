using System.Collections.Generic;

namespace Validations.Exceptions
{
    /// <summary>
    /// Class NonUniqueOperationException.
    /// </summary>
    public class NonUniqueOperationException
        : WrongParameterException
    {
        private readonly IEnumerable<KeyValuePair<string, int>> parameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="NonUniqueOperationException"/> class.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="message">The message.</param>
        public NonUniqueOperationException(IEnumerable<KeyValuePair<string, int>> parameters, string message)
            : base("parameters", message)
        {
            this.parameters = parameters;
        }

        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <value>The parameters.</value>
        public IEnumerable<KeyValuePair<string, int>> Parameters
        {
            get { return this.parameters; }
        }
    }
}
