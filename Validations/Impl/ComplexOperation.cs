using System;
using System.Linq.Expressions;
using Validations.Exceptions;

namespace Validations.Impl
{
    /// <summary>
    /// 
    /// </summary>
    public class ComplexOperation<TLeft, TRight>
        : OperationBase, IComplexOperation<TLeft, TRight>
        where TLeft: class
        where TRight: class

    {
        /// <summary>
        /// 
        /// </summary>
        private readonly Func<TLeft, TRight, bool> verifier;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="verifier"></param>
        public ComplexOperation(string target, Expression<Func<TLeft, TRight, bool>> verifier)
            : this(target, null, verifier)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="errorMessage"></param>
        /// <param name="verifier"></param>
        public ComplexOperation(string target, string errorMessage, Expression<Func<TLeft, TRight, bool>> verifier)
            :base(target, errorMessage)
        {
            
            if (verifier == null)
                throw new OnBuildingOperationException("verifier", "The lambda expression which serves for validating instance cannot be null.");

            this.verifier = verifier.Compile();
        }

        /// <summary>
        /// 
        /// </summary>
        public Func<TLeft, TRight, bool> Verifier
        {
            get { return verifier; }
        }
    }
}
