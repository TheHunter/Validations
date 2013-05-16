using Repower.Common.Validations;

namespace Validations.Impl
{
    /// <summary>
    /// 
    /// </summary>
    public class OperationResultInfo
        : ResultInfo, IOperationResult
    {
        private readonly string target;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="message"></param>
        internal protected OperationResultInfo(string target, string message)
            :base(message)
        {
            this.target = target;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Target
        {
            get { return this.target; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Target: {0} - {1}", this.target, base.ToString());
        }
    }
}
