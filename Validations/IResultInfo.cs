using System;

namespace Validations
{
    /// <summary>
    /// Interface IResultInfo
    /// </summary>
    public interface IResultInfo
    {
        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>The message.</value>
        string Message { get; }

        /// <summary>
        /// Gets the runtime exception.
        /// </summary>
        /// <value>The runtime exception.</value>
        Exception RuntimeException { get; }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>The state.</value>
        ResultState State { get; }
    }
}
