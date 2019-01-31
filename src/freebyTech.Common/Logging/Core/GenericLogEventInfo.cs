using System;
using System.Collections.Generic;

namespace freebyTech.Common.Logging.Core
{
    /// <summary>
    /// A class defining LogLevels not tied to any specific implementation of logging.
    /// </summary>
    public class GenericLogEventInfo
    {
        public GenericLogEventInfo(GenericLogLevel logLevel, string messageType, string message, string data, Exception exception, List<PushLogItem> PushLog) {
            LogLevel = logLevel;
            Message = message;
            MessageType = messageType;
            Data = data;
        }
        public GenericLogLevel LogLevel { get; set; }

        public string Message { get; set; }

        public string MessageType { get; set;}

        public string Data { get; set; }

        public Exception Exception { get; set; }

        public List<PushLogItem> PushLog { get; }

        public Dictionary<string, object> ExtraProperties { get; } = new Dictionary<string, object>();
    }
}