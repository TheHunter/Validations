using System;
using System.Collections.Generic;
using Validations.Exceptions;

namespace Validations.Impl
{
    /// <summary>
    /// Class ResultInfo.
    /// </summary>
    public class ResultInfo
        : IResultInfo
    {
        /// <summary>
        /// The result messages.
        /// </summary>
        private static readonly Dictionary<ResultState, string> ResultMessages;

        /// <summary>
        /// The message.
        /// </summary>
        private readonly string message;

        /// <summary>
        /// The state.
        /// </summary>
        private ResultState state = ResultState.NoDeterminated;
        
        /// <summary>
        /// Initializes static members of the <see cref="ResultInfo"/> class.
        /// </summary>
        static ResultInfo()
        {
            ResultMessages = new Dictionary<ResultState, string>
                                 {
                                     {
                                         ResultState.Successfully,
                                         "Operation successfully."
                                     },
                                     {
                                         ResultState.Unsuccessfully,
                                         "Operation with errors."
                                     },
                                     {
                                         ResultState.NoDeterminated,
                                         "Operation not verified."
                                     },
                                     {
                                         ResultState.RuntimeError,
                                         "Operation throws a exception."
                                     }
                                 };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultInfo"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <exception cref="WrongParameterException">
        /// message;The message to set at the current ResultInfo instance cannot be empty or null.
        /// </exception>
        protected internal ResultInfo(string message)
        {
            if (string.IsNullOrEmpty(message))
                throw new WrongParameterException("message", "The message to set at the current ResultInfo instance cannot be empty or null.");

            this.message = message;
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message
        {
            get { return this.message; }
        }

        /// <summary>
        /// Gets or sets the runtime exception.
        /// </summary>
        /// <value>The runtime exception.</value>
        public Exception RuntimeException { get; protected internal set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        public ResultState State
        {
            get { return this.state; }
            protected internal set { this.state = value; }
        }

        /// <summary>
        /// Gets the default message.
        /// </summary>
        /// <param name="state">
        /// The state.
        /// </param>
        /// <returns>
        /// System.String.
        /// </returns>
        public static string GetDefaultMessage(ResultState state)
        {
            return ResultMessages[state];
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">
        /// The object to compare with the current object.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (this.GetType() == obj.GetType())
            {
                return this.GetHashCode() == obj.GetHashCode();
            }

            return false;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return this.Message.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return string.Format("Message: {0} - Result State: {1}", this.Message,  Enum.GetName(this.state.GetType(), this.state));
        }
    }
}
