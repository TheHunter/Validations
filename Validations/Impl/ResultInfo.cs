using System;
using System.Collections.Generic;
using Repower.Common.Validations;
using Repower.Common.Validations.Exceptions;

namespace Validations.Impl
{
    /// <summary>
    /// 
    /// </summary>
    public class ResultInfo
        : IResultInfo
    {
        private readonly string message;
        private Exception runtimeException;
        private ResultState state = ResultState.NoDeterminated;
        private static readonly Dictionary<ResultState, string> ResultMessages;

        /// <summary>
        /// 
        /// </summary>
        static ResultInfo()
        {
            ResultMessages = new Dictionary<ResultState, string>();
            ResultMessages.Add(ResultState.Successfully, "Operation successfully.");
            ResultMessages.Add(ResultState.Unsuccessfully, "Operation with errors.");
            ResultMessages.Add(ResultState.NoDeterminated, "Operation not verified.");
            ResultMessages.Add(ResultState.RuntimeError, "Operation throws a exception.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        internal protected ResultInfo(string message)
        {
            if (string.IsNullOrEmpty(message))
                throw new WrongParameterException("message", "The message to set at the current ResultInfo instance cannot be empty or null.");

            this.message = message;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Message
        {
            get { return this.message; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Exception RuntimeException
        {
            get { return this.runtimeException; }
            internal protected set { this.runtimeException = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ResultState State
        {
            get { return this.state; }
            internal protected set { this.state = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static string GetDefaultMessage(ResultState state)
        {
            return ResultMessages[state];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is ResultInfo)
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
            return this.Message.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Message: {0} - Result State: {1}", this.Message,  Enum.GetName(this.state.GetType(), this.state));
        }

    }

}
