using System;
using System.Linq.Expressions;
using Validations.Exceptions;

namespace Validations.Impl
{
    /// <summary>
    /// Class ComplexOperation.
    /// </summary>
    /// <typeparam name="TLeft">The type of the t left.</typeparam>
    /// <typeparam name="TRight">The type of the t right.</typeparam>
    public class ComplexOperation<TLeft, TRight>
        : OperationBase, IComplexOperation<TLeft, TRight>
        where TLeft: class
        where TRight: class
    {
        /// <summary>
        /// The verifier
        /// </summary>
        private readonly Func<TLeft, TRight, bool> verifier;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComplexOperation{TLeft, TRight}"/> class.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="verifier">The verifier.</param>
        public ComplexOperation(string target, Expression<Func<TLeft, TRight, bool>> verifier)
            : this(target, null, verifier)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComplexOperation{TLeft, TRight}"/> class.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="verifier">The verifier.</param>
        /// <exception cref="Validations.Exceptions.OnBuildingOperationException">verifier;The lambda expression which serves for validating instance cannot be null.</exception>
        public ComplexOperation(string target, string errorMessage, Expression<Func<TLeft, TRight, bool>> verifier)
            : base(target, errorMessage)
        {
            
            if (verifier == null)
                throw new OnBuildingOperationException("verifier", "The lambda expression which serves for validating instance cannot be null.");

            this.verifier = verifier.Compile();
        }

        /// <summary>
        /// Gets the verifier.
        /// </summary>
        /// <value>The verifier.</value>
        public Func<TLeft, TRight, bool> Verifier
        {
            get { return verifier; }
        }
    }
}
