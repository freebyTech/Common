using System;
using System.Diagnostics;
using System.Reflection;

namespace freebyTech.Common.Logging.Interfaces
{
    public interface IInstrumentationLogger : IBasicLogger
    {
        string MethodSignature { get; }

        /// <summary>
        /// Initializes this class using the method's call signature as the signature for the log message.
        /// </summary>
        /// <param name="mb"></param>
        void SetMethodName(MethodBase mb);

        /// <summary>
        /// Initializes this class using a custom "method signature" as the signature for the log message.
        /// </summary>
        /// <param name="methodSignature"></param>
        void SetMethodSignature(string methodSignature);

        void LogInfoWithTime(string message, string data = null, bool resetDuration = false, long itemCount = 0, long byteCount = 0, long failedItemCount = 0, long failedByteCount = 0);
        
        void LogWarnWithTime(string message, string data = null, bool resetDuration = false, long itemCount = 0, long byteCount = 0, long failedItemCount = 0, long failedByteCount = 0);
        
        void LogErrorWithTime(string message, string data = null, Exception exceptionInfo = null, bool resetDuration = false, long itemCount = 0, long byteCount = 0, long failedItemCount = 0, long failedByteCount = 0);
        
        void LogExecutionComplete(long itemCount = 1, long byteCount = 0, string data = null, long failedItemCount = 0, long failedByteCount = 0);
        void LogExecutionCompleteAsWarn(long itemCount = 1, long byteCount = 0, string data = null, long failedItemCount = 0, long failedByteCount = 0, Exception exceptionInfo = null);
        void LogExecutionCompleteAsError(long itemCount = 0, long byteCount = 0, string data = null, long failedItemCount = 1, long failedByteCount = 0, Exception exceptionInfo = null);
        void LogExecutionCompleteAsFatal(long itemCount = 0, long byteCount = 0, string data = null, long failedItemCount = 1, long failedByteCount = 0, Exception exceptionInfo = null);
    }
}