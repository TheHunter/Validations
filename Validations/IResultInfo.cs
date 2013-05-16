﻿using System;
using Repower.Common.Validations;

namespace Validations
{
    /// <summary>
    /// 
    /// </summary>
    public interface IResultInfo
    {
        /// <summary>
        /// 
        /// </summary>
        string Message { get; }

        /// <summary>
        /// 
        /// </summary>
        Exception RuntimeException { get; }

        /// <summary>
        /// 
        /// </summary>
        ResultState State { get; }
    }
}