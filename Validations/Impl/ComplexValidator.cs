﻿using System;
using System.Collections.Generic;
using System.Linq;
using Repower.Common.Validations;
using Repower.Common.Validations.Exceptions;

namespace Validations.Impl
{
    /// <summary>
    /// 
    /// </summary>
    public class ComplexValidator<TLeft, TRight>
        :  ValidatorInfo, IComplexValidator<TLeft, TRight>
        where TLeft : class
        where TRight : class
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly HashSet<ComplexOperation<TLeft, TRight>> operations;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operations"></param>
        public ComplexValidator(IEnumerable<ComplexOperation<TLeft, TRight>> operations)
            : base(string.Format("<{0}-{1}>", typeof(TLeft).Name, typeof(TRight).Name))
        {
            var group = operations
                .GroupBy(n => n.Target)
                .Where(n => n.Count() > 1)
                .Select(n => new KeyValuePair<string, int>(n.Key, n.Count()))
                ;

            if (group.Any())
                throw new OnBuildingValidatorException("operations", "The operations must have an unique Target");

            this.operations = new HashSet<ComplexOperation<TLeft, TRight>>(operations);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        private static IOperationResult Compute(ComplexOperation<TLeft, TRight> operation, TLeft left, TRight right)
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

        /// <summary>
        /// 
        /// </summary>
        public override IEnumerable<IOperationInfo> Operations
        {
            get { return this.operations.Select<ComplexOperation<TLeft, TRight>, IOperationInfo>(n => n); }
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
