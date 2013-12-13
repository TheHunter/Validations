using System.Collections.Generic;
using Validations.Exceptions;

namespace Validations.Impl
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ValidatorInfo
        : IValidatorInfo
    {
        private readonly string typeInfo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeInfo"></param>
        protected ValidatorInfo(string typeInfo)
        {
            if (string.IsNullOrEmpty(typeInfo))
                throw new OnBuildingValidatorException("typeInfo", "The type info cannot be empty or null.");

            this.typeInfo = typeInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        public string TypeInfo
        { 
            get {return this.typeInfo;}
        }

        /// <summary>
        /// 
        /// </summary>
        public abstract IEnumerable<IOperationInfo> Operations { get; }

    }
}
