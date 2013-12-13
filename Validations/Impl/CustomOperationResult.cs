using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Validations.Exceptions;

namespace Validations.Impl
{
    /// <summary>
    /// A custom operation result that can be used by validator users.
    /// </summary>
    public class CustomOperationResult
        : OperationResultInfo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public CustomOperationResult(string target, string message, Exception ex)
            :base(target, message)
        {
            this.RuntimeException = ex;
            this.State = ResultState.RuntimeError;
        }

        /// <summary>
        /// Instance a new operation result with the given parameters.
        /// </summary>
        /// <exception cref="WrongParameterException">throws an exception if the state is set to RuntimeError.</exception>
        /// <param name="target"></param>
        /// <param name="message"></param>
        /// <param name="state"></param>
        public CustomOperationResult(string target, string message, ResultState state)
            : base(target, message)
        {
            if (state == ResultState.RuntimeError)
                throw new WrongParameterException("state", "The operation result cannot be set to RuntimeError, in this case It would be used the appropriate constructor.");

            this.State = state;
        }
    }
}
