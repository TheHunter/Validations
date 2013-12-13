using System.Collections.Generic;

namespace Validations.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class NonUniqueOperationException
        : WrongParameterException
    {
        private IEnumerable<KeyValuePair<string, int>> parameters;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="message"></param>
        public NonUniqueOperationException(IEnumerable<KeyValuePair<string, int>> parameters, string message)
            : base("parameters", message)
        {
            this.parameters = parameters;
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<KeyValuePair<string, int>> Parameters
        {
            get { return this.parameters; }
        }
    }
}
