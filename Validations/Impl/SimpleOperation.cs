using System;
using System.Linq.Expressions;
using Validations.Exceptions;

namespace Validations.Impl
{
    /// <summary>
    /// Class SimpleOperation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SimpleOperation<T>
        : OperationBase, ISimpleOperation<T>
        where T : class 
    {
        
        private readonly Func<T, bool> verifier;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleOperation{T}"/> class.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="verifier">The verifier.</param>
        public SimpleOperation(string target, Expression<Func<T, bool>> verifier)
            : this(target, null, verifier)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleOperation{T}"/> class.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="verifier">The verifier.</param>
        /// <exception cref="Validations.Exceptions.OnBuildingOperationException">verifier;The expression which serves for validating instance cannot be null.</exception>
        public SimpleOperation(string target, string errorMessage, Expression<Func<T, bool>> verifier)
            : base(target, errorMessage)
        {
            if (verifier == null)
                throw new OnBuildingOperationException("verifier", "The expression which serves for validating instance cannot be null.");

            this.verifier = verifier.Compile();
        }

        /// <summary>
        /// Gets the verifier.
        /// </summary>
        /// <value>The verifier.</value>
        public Func<T, bool> Verifier
        {
            get { return this.verifier; }
        }
    }
}
