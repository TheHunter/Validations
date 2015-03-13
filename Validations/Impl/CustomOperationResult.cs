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
        /// Initializes a new instance of the <see cref="CustomOperationResult"/> class.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public CustomOperationResult(string target, string message, Exception ex)
            : base(target, message)
        {
            this.RuntimeException = ex;
            this.State = ResultState.RuntimeError;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomOperationResult"/> class.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="message">The message.</param>
        /// <param name="state">The state.</param>
        /// <exception cref="OnBuildingOperationException">state;The operation result cannot be set to RuntimeError, in this case It would be used the appropriate constructor.</exception>
        public CustomOperationResult(string target, string message, ResultState state)
            : base(target, message)
        {
            if (state == ResultState.RuntimeError)
                throw new OnBuildingOperationException("state", "The operation result cannot be set to RuntimeError, in this case It would be used the appropriate constructor.");

            this.State = state;
        }
    }
}
