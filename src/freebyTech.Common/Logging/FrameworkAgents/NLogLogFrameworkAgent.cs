using System;
using System.Collections.Generic;
using System.Text;
using freebyTech.Common.Logging.Interfaces;
using freebyTech.Common.ExtensionMethods;
using NLog;

namespace freebyTech.Common.Logging.FrameworkAgents
{
    /// <summary>
    /// This interface represents an actual logging agent that takes the logging abstraction and translates it to each framework.
    /// </summary>
    public class NLogFrameworkAgent : ILogFrameworkAgent
    {
        public void Log(string loggerName, GenericLogEventInfo logEvent) {
            LogManager.GetLogger(loggerName).Log(GenericToSpecific(logEvent));
        }

        private static LogEventInfo GenericToSpecific(GenericLogEventInfo logEvent) {
            var logEventInfo = new LogEventInfo(LogLevel.FromOrdinal((int)logEvent.LogLevel), logEvent.MessageType, logEvent.Message);

            if (logEvent.Exception != null)
            {
                logEventInfo.Exception = logEvent.Exception;
            }

            logEventInfo.Properties["internalLog"] = AggregatePushLogItems(logEvent.PushLog);
            logEventInfo.Properties["data"] = logEvent.Data;
            foreach(var property in logEvent.ExtraProperties) {
                logEventInfo.Properties[property.Key] = property.Value;
            }

            return logEventInfo;
        }

        private static string AggregatePushLogItems(List<PushLogItem> pushLogItems)
        {
            if (pushLogItems == null || pushLogItems.Count == 0) { return string.Empty; }
            var sb = new StringBuilder();

            foreach (var item in pushLogItems)
            {
                if(!item.Key.IsNullOrEmpty()) {
                    sb.AppendLine($"{item.LogLevel.ToString()} -- {item.Key} -- {item.Value} [{item.DurationMs} ms]");
                }
                else {
                    sb.AppendLine($"{item.LogLevel.ToString()} -- {item.Line} [{item.DurationMs} ms]");
                }             
            }
            return sb.ToString();
        }
    }
}