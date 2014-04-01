﻿using System;
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
    public class AggregateValidator<TEntity, TBase>
        : SimpleValidator<TEntity>
        where TEntity : class, TBase
        
    {
        private readonly ISimpleValidator<TBase> validatorBase;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operations"></param>
        /// <param name="validatorBase"></param>
        public AggregateValidator(IEnumerable<ISimpleOperation<TEntity>> operations, ISimpleValidator<TBase> validatorBase)
            : base(operations)
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
        public override IEnumerable<IOperationResult> Validate(TEntity instance)
        {
            List<IOperationResult> results = new List<IOperationResult>();
            try
            {
                results.AddRange(this.validatorBase.Validate(instance));
                results.AddRange(base.Validate(instance));
            }
            catch (Exception ex)
            {
                throw new WrongParameterException("instance", ex.Message);
            }
            return results;
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

            if (obj is AggregateValidator<TEntity, TBase>)
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
            return string.Format("Aggregate validator of <{0}>", typeof(TEntity).Name);
        }
        #endregion
    }
}
