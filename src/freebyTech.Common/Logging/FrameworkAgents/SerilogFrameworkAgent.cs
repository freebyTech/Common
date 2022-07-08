using System;
using System.Collections.Generic;
using System.Text;
using freebyTech.Common.ExtensionMethods;
using Serilog;
using Serilog.Events;
using System.Diagnostics;
using freebyTech.Common.Logging.Core;
using freebyTech.Common.Logging.Interfaces;

namespace freebyTech.Common.Logging.FrameworkAgents
{
  /// <summary>
  /// This interface represents an actual logging agent that takes the logging abstraction and translates it to each framework.
  /// </summary>
  public class SerilogFrameworkAgent : ILogFrameworkAgent
  {
    public void Log(string loggerName, GenericLogEventInfo logEvent)
    {
      var logger = Serilog.Log.Logger.ForContext("loggerName", loggerName)
      .ForContext("data", logEvent.Data)
      .ForContext("internalLog", AggregatePushLogItems(logEvent.PushLog))
      .ForContext("messageType", logEvent.MessageType);

      try
      {
        foreach (var extraProperty in logEvent.ExtraProperties)
        {
          logger = logger.ForContext(extraProperty.Key, extraProperty.Value);
        }
      }
      catch (Exception ex)
      {
        Debug.Assert(true, "Serilog Agent Context Generation Threw Exception", ex.Message);
      }

      logger.Write(GenericToSpecificLogLevel(logEvent.LogLevel), logEvent.Exception, logEvent.Message);
    }

    private static LogEventLevel GenericToSpecificLogLevel(GenericLogLevel logLevel)
    {
      switch (logLevel)
      {
        case GenericLogLevel.Trace:
          return LogEventLevel.Verbose;
        case GenericLogLevel.Debug:
          return LogEventLevel.Debug;
        case GenericLogLevel.Info:
          return LogEventLevel.Information;
        case GenericLogLevel.Warn:
          return LogEventLevel.Warning;
        case GenericLogLevel.Error:
          return LogEventLevel.Error;
        case GenericLogLevel.Fatal:
          return LogEventLevel.Fatal;
      }
      return LogEventLevel.Information;
    }

    private static string AggregatePushLogItems(List<PushLogItem> pushLogItems)
    {
      if (pushLogItems == null || pushLogItems.Count == 0) { return string.Empty; }
      var sb = new StringBuilder();

      foreach (var item in pushLogItems)
      {
        sb.AppendLine(item.ToString());
      }
      return sb.ToString();
    }
  }
}