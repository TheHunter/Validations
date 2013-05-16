using System;
using System.Linq.Expressions;
using Repower.Common.Validations;
using Repower.Common.Validations.Exceptions;

namespace Validations.Impl
{
    /// <summary>
    /// 
    /// </summary>
    public class SimpleOperation<T>
        : OperationBase, ISimpleOperation<T>
        where T : class 
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly Func<T, bool> verifier;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="verifier"></param>
        public SimpleOperation(string target, Expression<Func<T, bool>> verifier)
            :this(target, null, verifier)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="errorMessage"></param>
        /// <param name="verifier"></param>
        public SimpleOperation(string target, string errorMessage, Expression<Func<T, bool>> verifier)
            :base(target, errorMessage)
        {
            if (verifier == null)
                throw new OnBuildingOperationException("verifier", "The expression which serves for validating instance cannot be null.");

            this.verifier = verifier.Compile();
        }

        /// <summary>
        /// 
        /// </summary>
        public Func<T, bool> Verifier
        {
            get { return this.verifier; }
        }
    }
}
