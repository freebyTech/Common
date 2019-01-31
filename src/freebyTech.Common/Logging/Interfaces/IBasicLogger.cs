using System;
using System.Collections.Generic;
using System.Text;

namespace freebyTech.Common.Logging.Interfaces
{
    /// <summary>
    /// General list of different logger types, not necessarily comprehensive as certain apps could have custom ones.
    /// </summary>
    public enum LoggingMessageTypes
    {
        LogMessage,
        RequestResponse,
        Instrumentation,
        Audit,
        Validation
    }

    public class SeperatorLineChars
    {
        public static readonly char Line = '-';
        public static readonly char Asterisk = '*';
        public static readonly char Equal = '=';
    }


    public interface IBasicLogger
    {
        /// <summary>
        /// The Application identifier used to identify the application or application "sub-component"
        /// </summary>
        string ApplicationLoggingId { get; }

        /// <summary>
        /// The type of logging message that this logger logs, i.e. ApplicationLog / Instrumentation / RequestResponse
        /// </summary>
        string MessageType { get; }

        /// <summary>
        /// Derived property of Application Logging ID and Message Type, used when constructing the nlog logger as its Name constructor.
        /// 
        /// i.e. - fb-restapi-webengine-instrumentation
        ///        fb-restapi-webengine-requestresponse
        ///        fb-restapi-webengine-logmessage
        /// 
        /// This allows us to setup rules based upon different message types in the nlog.config.
        /// </summary>
        string FullLoggingName { get; }

        string ApplicationName { get; }
        string ApplicationVersion { get; }
        string LoggingAssemblyVersion { get; }
        string UserName { get; }
        string ProcessName { get; }
        string HostName { get; }

        /// <summary>
        /// If true, will log or push a line of seperator characters when a LogX or PushX method is run with an empty string.
        /// </summary>
        bool SeperatorLineOnEmptyLog { get; set; }

        /// <summary>
        /// The seperator character to use for empty writes and also headers.
        /// </summary>
        char SeperatorChar { get; set; }

        /// <summary>
        /// The length of seperator lines to write when doing empty writes or generating headers.
        /// </summary>
        int SeperatorLineLength { get; set; }

        void PushTrace(string key, string value);
        void PushDebug(string key, string value);
        void PushInfo(string key, string value);
        void PushError(string key, string value);
        void PushWarn(string key, string value);
        void PushFatal(string key, string value);
        void PushTrace(string line);
        void PushTrace();
        void PushDebug(string line);
        void PushDebug();
        void PushInfo(string line);
        void PushInfo();
        void PushError(string line);
        void PushError();
        void PushWarn(string line);
        void PushWarn();
        void PushFatal(string line);
        void PushFatal();
        void PushHeaderTrace(string line);
        void PushHeaderDebug(string line);
        void PushHeaderInfo(string line);
        void PushHeaderError(string line);
        void PushHeaderWarn(string line);
        void PushHeaderFatal(string line);
        void ClearQueueData();

        /// <summary>
        /// Push all public properties to the Info stack.
        /// </summary>
        void PushLoggingProperties();

        void LogHeaderTrace(string line);
        void LogHeaderDebug(string line);
        void LogHeaderInfo(string line);
        void LogHeaderError(string line);
        void LogHeaderWarn(string line);
        void LogHeaderFatal(string line);

        /// <summary>
        /// Empty LogTrace will write out any Trace data that has been pushed via the 'PushTrace' Method
        /// </summary>
        void LogTrace();

        void LogTrace(string message);
        void LogTrace(string message, string data);

        /// <summary>
        /// Empty LogDebug will write out any Debug data that has been pushed via the 'PushDebug' Method
        /// </summary>
        void LogDebug();

        void LogDebug(string message);
        void LogDebug(string message, string data);

        /// <summary>
        /// Empty LogInfo will write out any Info data that has been pushed via the 'PushInfo' Method
        /// </summary>
        void LogInfo();

        void LogInfo(string message);
        void LogInfo(string message, string data);

        /// <summary>
        /// Empty LogWarn will write out any Warn data that has been pushed via the 'PushWarn' Method
        /// </summary>
        void LogWarn();

        void LogWarn(string message);
        void LogWarn(string message, string data);
        void LogWarn(string message, string data, Exception exceptionInfo);

        /// <summary>
        /// Empty LogError will write out any Error data that has been pushed via the 'PushError' Method
        /// </summary>
        void LogError();

        void LogError(string message);
        void LogError(Exception ex);
        void LogError(string message, Exception ex);
        void LogError(string message, Exception ex, string data);

        /// <summary>
        /// Empty LogFatal will write out any Fatal data that has been pushed via the 'PushFatal' Method
        /// </summary>
        void LogFatal();

        void LogFatal(string message);
        void LogFatal(Exception ex);
        void LogFatal(string message, Exception ex);
        void LogFatal(string message, Exception ex, string data);
    }
}
