using System.Collections.Generic;
using Validations.Exceptions;

namespace Validations.Impl
{
    /// <summary>
    /// Class ValidatorInfo.
    /// </summary>
    public abstract class ValidatorInfo
        : IValidatorInfo
    {
        private readonly string typeInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorInfo"/> class.
        /// </summary>
        /// <param name="typeInfo">The type information.</param>
        /// <exception cref="OnBuildingValidatorException">typeInfo;The type info cannot be empty or null.</exception>
        protected ValidatorInfo(string typeInfo)
        {
            if (string.IsNullOrEmpty(typeInfo))
                throw new OnBuildingValidatorException("typeInfo", "The type info cannot be empty or null.");

            this.typeInfo = typeInfo;
        }

        /// <summary>
        /// Gets a target which identify the current Validator.
        /// </summary>
        /// <value>The target of the calling validator.</value>
        public string TypeInfo
        { 
            get {return this.typeInfo;}
        }

        /// <summary>
        /// Gets the operations.
        /// </summary>
        /// <value>The operations.</value>
        public abstract IEnumerable<IOperationInfo> Operations { get; }
    }
}
