using System;
using System.Collections.Generic;
using System.Linq;
using Validations.Exceptions;

namespace Validations.Impl
{
    /// <summary>
    /// Defines a set of policies in order to verify property values a runtime.
    /// </summary>
    /// <typeparam name="TEntity">the type of validator.</typeparam>
    public class SimpleValidator<TEntity>
        : ValidatorInfo, ISimpleValidator<TEntity>
        where TEntity : class
    {
        
        private readonly HashSet<ISimpleOperation<TEntity>> operations;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleValidator{TEntity}"/> class.
        /// </summary>
        /// <param name="operations">The operations.</param>
        /// <exception cref="Validations.Exceptions.OnBuildingValidatorException">operations;The set of operations for simple validator cannot be empty or null.</exception>
        /// <exception cref="Validations.Exceptions.NonUniqueOperationException">Operations target for simple validators must be unique.</exception>
        public SimpleValidator(IEnumerable<ISimpleOperation<TEntity>> operations)
            : base(string.Format("<{0}>", typeof(TEntity).Name))
        {
            if (operations == null || !operations.Any())
            {
                throw new OnBuildingValidatorException(
                    "operations",
                    "The set of operations for simple validator cannot be empty or null.");
            }

            var group = operations
                .GroupBy(n => n.Target)
                .Where(n => n.Count() > 1)
                .Select(n => new KeyValuePair<string, int>(n.Key, n.Count())).ToList()
                ;

            if (group.Any())
                throw new NonUniqueOperationException(group, "Operations target for simple validators must be unique.");

            this.operations = new HashSet<ISimpleOperation<TEntity>>(operations);
        }

        /// <summary>
        /// Gets the operations.
        /// </summary>
        /// <value>The operations.</value>
        public override IEnumerable<IOperationInfo> Operations
        {
            get { return operations.Select<ISimpleOperation<TEntity>, IOperationInfo>(n => n); }
        }

        /// <summary>
        /// Validates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>IEnumerable&lt;IOperationResult&gt;.</returns>
        /// <exception cref="Validations.Exceptions.ValidationParameterException">Instance to evaluate cannot be null.</exception>
        public virtual IEnumerable<IOperationResult> Validate(TEntity instance)
        {
            if (instance == null)
                throw new ValidationParameterException("Instance to evaluate cannot be null.");

            return new List<IOperationResult>(operations.Select(n => Compute(n, instance)));
        }

        #region overriding object methods

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is SimpleValidator<TEntity>)
                return this.GetHashCode() == obj.GetHashCode();
            
            return false;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return 7 * typeof(TEntity).GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return string.Format("Validator of <{0}>", typeof(TEntity).Name);
        }
        #endregion

        /// <summary>
        /// Computes the specified operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="instance">The instance.</param>
        /// <returns>IOperationResult.</returns>
        private static IOperationResult Compute(ISimpleOperation<TEntity> operation, TEntity instance)
        {
            ResultState state;
            Exception exception = null;
            string message;
            try
            {
                state = operation.Verifier(instance) ? ResultState.Successfully : ResultState.Unsuccessfully;
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

    }

}
