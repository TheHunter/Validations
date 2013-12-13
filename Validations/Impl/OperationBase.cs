
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
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="errorMessage"></param>
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
        /// A label which indentifies the operation.
        /// </summary>
        public string Target
        {
            get { return this.target; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string MessageOnError
        {
            get { return this.messageOnError; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is OperationBase)
                return this.GetHashCode() == obj.GetHashCode();
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return target.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Operation: {0}", this.target);
        }
    }
}
