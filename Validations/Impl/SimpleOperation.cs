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
        private readonly string stringExpression;

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
            this.stringExpression = verifier.Body.ToString();
        }

        /// <summary>
        /// Gets the verifier.
        /// </summary>
        /// <value>The verifier.</value>
        public Func<T, bool> Verifier
        {
            get { return this.verifier; }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return string.Format("{0} - Expression: {1}", base.ToString(), this.stringExpression);
        }
    }
}
