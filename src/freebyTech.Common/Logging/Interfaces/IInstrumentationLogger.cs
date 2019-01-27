using System;
using System.Diagnostics;
using System.Reflection;

namespace freebyTech.Common.Logging.Interfaces
{
    public interface IInstrumentationLogger : IBasicLogger
    {
        string MethodSignature { get; }
        Stopwatch SW { get; }

        /// <summary>
        /// Initializes this class using the method's call signature as the signature for the log message.
        /// </summary>
        /// <param name="mb"></param>
        void InitializeExecutionLogging(MethodBase mb);

        /// <summary>
        /// Initializes this class using a custom "method description" as the signature for the log message.
        /// </summary>
        /// <param name="methodDescription"></param>
        void InitializeExecutionLogging(string methodDescription);

        /// <summary>
        /// Resets the instrumentation properties of the logger and also Restarts the stopwatch.
        /// </summary>
        void Restart();

        void LogInfoWithTime(string message, string data = null, bool resetStopwatch = false, long itemCount = 0, long byteCount = 0, long failedItemCount = 0, long failedByteCount = 0);
        void PushInfoWithTime(string message);
        void PushInfoWithTime(string key, string value);
        void LogWarnWithTime(string message, string data = null, bool resetStopwatch = false, long itemCount = 0, long byteCount = 0, long failedItemCount = 0, long failedByteCount = 0);
        void PushWarnWithTime(string message);
        void PushWarnWithTime(string key, string value);
        void LogErrorWithTime(string message, string data = null, Exception exceptionInfo = null, bool resetStopwatch = false, long itemCount = 0, long byteCount = 0, long failedItemCount = 0, long failedByteCount = 0);
        void PushErrorWithTime(string message);
        void PushErrorWithTime(string key, string value);
        void LogExecutionComplete(long itemCount = 1, long byteCount = 0, string data = null, long failedItemCount = 0, long failedByteCount = 0);
        void LogExecutionCompleteAsWarn(long itemCount = 1, long byteCount = 0, string data = null, long failedItemCount = 0, long failedByteCount = 0, Exception exceptionInfo = null);
        void LogExecutionCompleteAsError(long itemCount = 0, long byteCount = 0, string data = null, long failedItemCount = 1, long failedByteCount = 0, Exception exceptionInfo = null);
        void LogExecutionCompleteAsFatal(long itemCount = 0, long byteCount = 0, string data = null, long failedItemCount = 1, long failedByteCount = 0, Exception exceptionInfo = null);
    }
}