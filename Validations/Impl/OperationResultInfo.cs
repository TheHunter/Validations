namespace Validations.Impl
{
    /// <summary>
    /// Class OperationResultInfo.
    /// </summary>
    public class OperationResultInfo
        : ResultInfo, IOperationResult
    {
        private readonly string target;

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationResultInfo"/> class.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="message">The message.</param>
        protected internal OperationResultInfo(string target, string message)
            : base(message)
        {
            this.target = target;
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
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return string.Format("Target: {0} - {1}", this.target, base.ToString());
        }
    }
}
