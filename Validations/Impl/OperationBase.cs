
using Validations.Exceptions;

namespace Validations.Impl
{
    /// <summary>
    /// A basic implementation of operation which is used by validators.
    /// </summary>
    public abstract class OperationBase
        : IOperationInfo
    {
        private readonly string target;
        private readonly string messageOnError;

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationBase"/> class.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <exception cref="Validations.Exceptions.OnBuildingOperationException">target;Operation target cannot be null or empty.</exception>
        protected OperationBase(string target, string errorMessage)
        {
            if (string.IsNullOrEmpty(target))
                throw new OnBuildingOperationException("target", "Operation target cannot be null or empty.");

            this.target = target.Trim();
            if (errorMessage == null || (errorMessage = errorMessage.Trim()).Equals(string.Empty))
                this.messageOnError = "No error message set";
            else
                this.messageOnError = errorMessage;
        }

        /// <summary>
        /// Gets the label which identifies the calling operation.
        /// </summary>
        /// <value>The target.</value>
        public string Target
        {
            get { return this.target; }
        }

        /// <summary>
        /// Gets a message which is used by operation result when the calling operation is failed.
        /// </summary>
        /// <value>The message on error.</value>
        public string MessageOnError
        {
            get { return this.messageOnError; }
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is OperationBase)
                return this.GetHashCode() == obj.GetHashCode();
            return false;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return target.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return string.Format("Operation: {0}", this.target);
        }
    }
}
