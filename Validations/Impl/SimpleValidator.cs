using System;
using System.Collections.Generic;
using System.Linq;
using Repower.Common.Validations;
using Repower.Common.Validations.Exceptions;

namespace Validations.Impl
{
    /// <summary>
    /// Defines a set of policies in order to verify property values a runtime.
    /// </summary>
    /// <typeparam name="TEntity">the type of validator.</typeparam>
    public class SimpleValidator<TEntity>
        : ValidatorInfo, ISimpleValidator<TEntity>
        where TEntity: class
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly HashSet<SimpleOperation<TEntity>> operations;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operations"></param>
        public SimpleValidator(IEnumerable<SimpleOperation<TEntity>> operations)
            : base(string.Format("<{0}>", typeof(TEntity).Name))
        {
            if (operations == null || !operations.Any())
                throw new OnBuildingValidatorException("operations",
                                                       "The set of operations for validator cannot be empty or null.");

            var group = operations
                .GroupBy(n => n.Target)
                .Where(n => n.Count() > 1)
                .Select(n => new KeyValuePair<string, int>(n.Key, n.Count())).ToList()
                ;

            if (group.Any())
                throw new NonUniqueOperationException(group, "The operations must have an unique Target");

            this.operations = new HashSet<SimpleOperation<TEntity>>(operations);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns>returns the result of validation.</returns>
        public IEnumerable<IOperationResult> Validate(TEntity instance)
        {
            if (instance == null)
                throw new ValidationParameterException("Instance to evaluate cannot be null.");

            return new List<IOperationResult>(operations.Select(n => Compute(n, instance)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        private static IOperationResult Compute(SimpleOperation<TEntity> operation, TEntity instance)
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

        /// <summary>
        /// 
        /// </summary>
        public override IEnumerable<IOperationInfo> Operations
        {
            get { return operations.Select<SimpleOperation<TEntity>, IOperationInfo>(n => n); }
        }

        #region overriding object methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is SimpleValidator<TEntity>)
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
            return base.GetHashCode() * 7 - typeof(TEntity).GetHashCode() ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Validator of <{0}>", typeof(TEntity).Name);
        }
        #endregion

    }

}
