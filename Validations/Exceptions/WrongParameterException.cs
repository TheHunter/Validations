using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repower.Common.Validations.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class WrongParameterException
        : ArgumentException
    {
        private string parameterName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="message"></param>
        public WrongParameterException(string parameterName, string message)
            :base(message, parameterName)
        {   
        }
    }
}
