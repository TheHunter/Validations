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
        private readonly string stringExpression;

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
            this.stringExpression = verifier.Body.ToString();
        }

        /// <summary>
        /// Gets the verifier.
        /// </summary>
        /// <value>The verifier.</value>
        public Func<TLeft, TRight, bool> Verifier
        {
            get { return verifier; }
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
