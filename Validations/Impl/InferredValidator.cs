using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Validations.Exceptions;

namespace Validations.Impl
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TBase"></typeparam>
    public class InferredValidator<TEntity, TBase>
        : ValidatorInfo, ISimpleValidator<TEntity>
        where TEntity : class, TBase
        where TBase : class
    {
        private readonly ISimpleValidator<TBase> validatorBase;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operations"></param>
        public InferredValidator(IEnumerable<ISimpleOperation<TBase>> operations)
            : base(string.Format("<{0}>", typeof(TEntity).Name))
        {
            this.validatorBase = new SimpleValidator<TBase>(operations);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="validatorBase"></param>
        public InferredValidator(ISimpleValidator<TBase> validatorBase)
            : base(string.Format("<{0}>", typeof(TEntity).Name))
        {
            if (validatorBase == null)
                throw new OnBuildingValidatorException("validatorBase", "Instance to evaluate cannot be null.");

            this.validatorBase = validatorBase;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public IEnumerable<IOperationResult> Validate(TEntity instance)
        {
            return this.validatorBase.Validate(instance);
        }

        /// <summary>
        /// 
        /// </summary>
        public override IEnumerable<IOperationInfo> Operations
        {
            get { return validatorBase.Operations; }
        }

        #region overriding object methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is InferredValidator<TEntity, TBase>)
                return this.GetHashCode() == obj.GetHashCode();

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return 7 * (typeof(TEntity).GetHashCode() - typeof(TBase).GetHashCode());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Inferred validator of <{0}>", typeof(TEntity).Name);
        }
        #endregion
    }
}
