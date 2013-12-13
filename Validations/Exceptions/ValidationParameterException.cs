using System.Collections.Generic;

namespace Validations.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidationParameterException
        : WrongParameterException
    {
        private readonly IList<string> wrongParameters;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ValidationParameterException(string message)
            : base("instance", message)
        {
            this.wrongParameters = new List<string>();
            this.wrongParameters.Add("instance");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="wrongParameters"></param>
        public ValidationParameterException(string message, IEnumerable<string> wrongParameters)
            :base("instancesToValidate", message)
        {
            this.wrongParameters = new List<string>(wrongParameters);
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<string> WrongParameters
        {
            get { return this.wrongParameters; }
        }
    }
}
