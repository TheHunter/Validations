using System;
using System.Collections.Generic;
using System.Linq;
using Validations.Exceptions;

namespace Validations.Impl
{
    /// <summary>
    /// Class ComplexValidator.
    /// </summary>
    /// <typeparam name="TLeft">The type of the t left.</typeparam>
    /// <typeparam name="TRight">The type of the t right.</typeparam>
    public class ComplexValidator<TLeft, TRight>
        : ValidatorInfo, IComplexValidator<TLeft, TRight>
        where TLeft : class
        where TRight : class
    {
        /// <summary>
        /// The operations
        /// </summary>
        private readonly HashSet<IComplexOperation<TLeft, TRight>> operations;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComplexValidator{TLeft, TRight}"/> class.
        /// </summary>
        /// <param name="operations">The operations.</param>
        /// <exception cref="Validations.Exceptions.OnBuildingValidatorException">operations;The set of operations for complex validator cannot be empty or null.</exception>
        /// <exception cref="Validations.Exceptions.NonUniqueOperationException">Operations target for simple validators must be unique.</exception>
        public ComplexValidator(IEnumerable<IComplexOperation<TLeft, TRight>> operations)
            : base(string.Format("<{0}-{1}>", typeof(TLeft).Name, typeof(TRight).Name))
        {
            if (operations == null || !operations.Any())
            {
                throw new OnBuildingValidatorException(
                    "operations",
                    "The set of operations for complex validator cannot be empty or null.");
            }

            var group = operations
                .GroupBy(n => n.Target)
                .Where(n => n.Count() > 1)
                .Select(n => new KeyValuePair<string, int>(n.Key, n.Count()))
                ;

            if (group.Any())
                throw new NonUniqueOperationException(group, "Operations target for simple validators must be unique.");

            this.operations = new HashSet<IComplexOperation<TLeft, TRight>>(operations);
        }

        /// <summary>
        /// Gets the operations.
        /// </summary>
        /// <value>The operations.</value>
        public override IEnumerable<IOperationInfo> Operations
        {
            get { return this.operations.Select<IComplexOperation<TLeft, TRight>, IOperationInfo>(n => n); }
        }

        /// <summary>
        /// Validates the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>IEnumerable&lt;IOperationResult&gt;.</returns>
        /// <exception cref="Validations.Exceptions.ValidationParameterException">All instances to validate must be referenced.</exception>
        public IEnumerable<IOperationResult> Validate(TLeft left, TRight right)
        {
            List<string> col = new List<string>();

            if (left == null) col.Add("left");
            if (right == null) col.Add("right");

            if (col.Any())
                throw new ValidationParameterException("All instances to validate must be referenced.", col);

            return new List<IOperationResult>(operations.Select(n => Compute(n, left, right)));
        }

        /// <summary>
        /// Computes the specified operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>IOperationResult.</returns>
        private static IOperationResult Compute(IComplexOperation<TLeft, TRight> operation, TLeft left, TRight right)
        {
            ResultState state;
            Exception exception = null;
            string message;
            try
            {
                state = operation.Verifier(left, right) ? ResultState.Successfully : ResultState.Unsuccessfully;
                if (state == ResultState.Unsuccessfully)
                    message = operation.MessageOnError ?? ResultInfo.GetDefaultMessage(state);
                else
                    message = ResultInfo.GetDefaultMessage(state);
            }
            catch (Exception ex)
            {
                state = ResultState.RuntimeError;
                exception = ex;
                message = ResultInfo.GetDefaultMessage(state);
            }
            return new OperationResultInfo(operation.Target, message) { State = state, RuntimeException = exception };
        }

        #region overriding object methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is ComplexValidator<TLeft, TRight>)
            {
                return this.GetHashCode() == obj.GetHashCode();
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (base.GetHashCode() * 11) - (typeof(TLeft).GetHashCode() + typeof(TRight).GetHashCode());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Validator of <{0}, {1}>", typeof(TLeft).Name, typeof(TRight).Name);
        }
        #endregion
    }

}
