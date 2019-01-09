using System;
using System.Reflection;
using NLog;

namespace freebyUtil.Logging
{
  /// <summary>
  /// 
  /// This class provides basic Run Metrics logging services for an application,
  /// it also sets the proper message type to "Instrumentation" in log message payloads.
  /// 
  /// You can also subclass this class to define your own Run Metrics logging, just override the
  /// SetDerivedCustomProperties() method to set any extra custom properties.
  /// 
  /// To Use this class place the following line at the top of any class you want to log messages from. 
  /// 
  /// private static readonly RunMetricsLogger Log = new RunMetricsLogger(Assembly.GetExecutingAssembly(), LoggingConstants.ApplicationLoggingId);
  /// 
  /// This assumes a constant called ApplicationLoggingId is defined in a class called LoggingConstants.
  /// 
  /// </summary>
  public class RunMetricsLoggerBase : LoggingBase
  {
    public RunMetricsLoggerBase(Assembly parentApplication, string applicationLoggingId) : base(parentApplication, LoggingMessageTypes.Instrumentation.ToString(), applicationLoggingId){}

    public RunMetricsLoggerBase(string parentApplicationName, string parentApplicationVersion, string applicationLoggingId) : base(parentApplicationName, parentApplicationVersion, LoggingMessageTypes.Instrumentation.ToString(), applicationLoggingId) { }

    /// <summary>
    /// Will Log all Application run statistics into an instrumentation message.
    /// </summary>
    public void LogRunMetrics(bool fileTypeLogging = false)
    {
      if (fileTypeLogging)
      {
        PushLoggingStats();
      }
      
      LogInfo("Current Application Logging Metrics");
    }

    public void ResetSummaryData()
    {
      ApplicationLoggingMetrics.Reset();
    }

    #region Private Methods

    /// <summary>
    /// Pushes the logging stats to the Push Log Queue, used for file logging.
    /// </summary>
    private void PushLoggingStats()
    {
      // Create a nice Data message for File Based Logging.
      PushInfo("Start Time", ApplicationLoggingMetrics.StartTime.ToString());
      if (!ApplicationLoggingMetrics.EndTime.HasValue)
      {
        ApplicationLoggingMetrics.EndTime = DateTime.Now;
      }
      PushInfo("End Time", ApplicationLoggingMetrics.EndTime.Value.ToString());
      PushInfo("Total Time", ApplicationLoggingMetrics.TotalTime().ToString());
      PushInfo("Fatal Log Count", ApplicationLoggingMetrics.FatalCount.ToString());
      PushInfo("Error Log Count", ApplicationLoggingMetrics.ErrorCount.ToString());
      PushInfo("Warn Log Count", ApplicationLoggingMetrics.WarnCount.ToString());
      PushInfo("Info Log Count", ApplicationLoggingMetrics.InfoCount.ToString());
      PushInfo("Debug Log Count", ApplicationLoggingMetrics.DebugCount.ToString());
      PushInfo("Trace Log Count", ApplicationLoggingMetrics.TraceCount.ToString());
    }

    #endregion

    #region Override Methods

    protected sealed override void SetCustomProperties(LogEventInfo logEvent)
    {
      // Set the properties for centralized logging.
      logEvent.Properties["startTime"] = ApplicationLoggingMetrics.StartTime.ToString();
      if (ApplicationLoggingMetrics.EndTime.HasValue)
      {
        logEvent.Properties["endTime"] = ApplicationLoggingMetrics.EndTime.Value.ToString();
      }
      logEvent.Properties["executionTimeMinutes"] = ApplicationLoggingMetrics.TotalTime().TotalMinutes;
      logEvent.Properties["executionTimeMS"] = ApplicationLoggingMetrics.TotalTime().Milliseconds;
      logEvent.Properties["fatalLogCount"] = ApplicationLoggingMetrics.FatalCount;
      logEvent.Properties["errorLogCount"] = ApplicationLoggingMetrics.ErrorCount;
      logEvent.Properties["warnLogCount"] = ApplicationLoggingMetrics.WarnCount;
      logEvent.Properties["infoLogCount"] = ApplicationLoggingMetrics.InfoCount;
      logEvent.Properties["debugLogCount"] = ApplicationLoggingMetrics.DebugCount;
      logEvent.Properties["traceLogCount"] = ApplicationLoggingMetrics.TraceCount;
      SetDerivedClassCustomProperties(logEvent);
    }

    /// <summary>
    /// If implementing a logger on top of this logger you should set your custom properties here rather 
    /// than in SetCustomProperties which is already being used by this class.
    /// </summary>
    /// <param name="logEvent"></param>
    protected virtual void SetDerivedClassCustomProperties(LogEventInfo logEvent)
    {

    }

    #endregion

  }
}
