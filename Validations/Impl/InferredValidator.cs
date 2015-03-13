using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Validations.Exceptions;

namespace Validations.Impl
{
    /// <summary>
    /// Class InferredValidator.
    /// </summary>
    /// <typeparam name="TEntity">The type of the t entity.</typeparam>
    /// <typeparam name="TBase">The type of the t base.</typeparam>
    public class InferredValidator<TEntity, TBase>
        : ValidatorInfo, ISimpleValidator<TEntity>
        where TEntity : class, TBase
        where TBase : class
    {
        private readonly ISimpleValidator<TBase> validatorBase;

        /// <summary>
        /// Initializes a new instance of the <see cref="InferredValidator{TEntity, TBase}"/> class.
        /// </summary>
        /// <param name="operations">The operations.</param>
        public InferredValidator(IEnumerable<ISimpleOperation<TBase>> operations)
            : base(string.Format("<{0}>", typeof(TEntity).Name))
        {
            this.validatorBase = new SimpleValidator<TBase>(operations);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InferredValidator{TEntity, TBase}"/> class.
        /// </summary>
        /// <param name="validatorBase">The validator base.</param>
        /// <exception cref="Validations.Exceptions.OnBuildingValidatorException">validatorBase;Instance to evaluate cannot be null.</exception>
        public InferredValidator(ISimpleValidator<TBase> validatorBase)
            : base(string.Format("<{0}>", typeof(TEntity).Name))
        {
            if (validatorBase == null)
                throw new OnBuildingValidatorException("validatorBase", "Instance to evaluate cannot be null.");

            this.validatorBase = validatorBase;
        }

        /// <summary>
        /// Gets the operations.
        /// </summary>
        /// <value>The operations.</value>
        public override IEnumerable<IOperationInfo> Operations
        {
            get { return validatorBase.Operations; }
        }

        /// <summary>
        /// Validates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>IEnumerable&lt;IOperationResult&gt;.</returns>
        public IEnumerable<IOperationResult> Validate(TEntity instance)
        {
            return this.validatorBase.Validate(instance);
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

            if (obj is InferredValidator<TEntity, TBase>)
                return this.GetHashCode() == obj.GetHashCode();

            return false;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return 7 * (typeof(TEntity).GetHashCode() - typeof(TBase).GetHashCode());
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return string.Format("Inferred validator of <{0}>", typeof(TEntity).Name);
        }
        #endregion
    }
}
