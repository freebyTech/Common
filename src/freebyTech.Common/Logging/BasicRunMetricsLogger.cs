using freebyTech.Common.Logging.Interfaces;
using NLog;
using System;
using System.Reflection;

namespace freebyTech.Common.Logging
{
    /// <summary>
    /// 
    /// This class provides basic Run Metrics logging services for an application,
    /// it also sets the proper message type to "Instrumentation" in log message payloads.
    /// 
    /// You can also subclass this class to define your own Run Metrics logging, just override the
    /// SetDerivedCustomProperties() method to set any extra custom properties.
    /// 
    /// To Use this class register its type with the IServiceCollection during ConfigureServices like this:
    ///
    /// <code>
    ///   services.AddScoped<IRunMetricsLogger, BasicRunMetricsLogger>((ctx) =>
    ///   {
    ///       return new BasicRunMetricsLogger(parentApplication, applicationLogginId);
    ///   });
    /// </code>
    /// 
    /// You can also register this class by running <code>services.AddBasicLoggingServices()</code>
    /// 
    /// </summary>
    public class BasicRunMetricsLogger : LoggingBase, IRunMetricsLogger
  {
    public BasicRunMetricsLogger(Assembly parentApplication, string applicationLoggingId) : base(parentApplication, LoggingMessageTypes.Instrumentation.ToString(), applicationLoggingId){}

    public BasicRunMetricsLogger(string parentApplicationName, string parentApplicationVersion, string applicationLoggingId) : base(parentApplicationName, parentApplicationVersion, LoggingMessageTypes.Instrumentation.ToString(), applicationLoggingId) { }

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
      StaticApplicationLoggingMetrics.Reset();
    }

    #region Private Methods

    /// <summary>
    /// Pushes the logging stats to the Push Log Queue, used for file logging.
    /// </summary>
    private void PushLoggingStats()
    {
      // Create a nice Data message for File Based Logging.
      PushInfo("Start Time", StaticApplicationLoggingMetrics.StartTime.ToString());
      if (!StaticApplicationLoggingMetrics.EndTime.HasValue)
      {
        StaticApplicationLoggingMetrics.EndTime = DateTime.Now;
      }
      PushInfo("End Time", StaticApplicationLoggingMetrics.EndTime.Value.ToString());
      PushInfo("Total Time", StaticApplicationLoggingMetrics.TotalTime().ToString());
      PushInfo("Fatal Log Count", StaticApplicationLoggingMetrics.FatalCount.ToString());
      PushInfo("Error Log Count", StaticApplicationLoggingMetrics.ErrorCount.ToString());
      PushInfo("Warn Log Count", StaticApplicationLoggingMetrics.WarnCount.ToString());
      PushInfo("Info Log Count", StaticApplicationLoggingMetrics.InfoCount.ToString());
      PushInfo("Debug Log Count", StaticApplicationLoggingMetrics.DebugCount.ToString());
      PushInfo("Trace Log Count", StaticApplicationLoggingMetrics.TraceCount.ToString());
    }

    #endregion

    #region Override Methods

    protected sealed override void SetCustomProperties(LogEventInfo logEvent)
    {
      // Set the properties for centralized logging.
      logEvent.Properties["startTime"] = StaticApplicationLoggingMetrics.StartTime.ToString();
      if (StaticApplicationLoggingMetrics.EndTime.HasValue)
      {
        logEvent.Properties["endTime"] = StaticApplicationLoggingMetrics.EndTime.Value.ToString();
      }
      logEvent.Properties["executionTimeMinutes"] = StaticApplicationLoggingMetrics.TotalTime().TotalMinutes;
      logEvent.Properties["executionTimeMS"] = StaticApplicationLoggingMetrics.TotalTime().Milliseconds;
      logEvent.Properties["fatalLogCount"] = StaticApplicationLoggingMetrics.FatalCount;
      logEvent.Properties["errorLogCount"] = StaticApplicationLoggingMetrics.ErrorCount;
      logEvent.Properties["warnLogCount"] = StaticApplicationLoggingMetrics.WarnCount;
      logEvent.Properties["infoLogCount"] = StaticApplicationLoggingMetrics.InfoCount;
      logEvent.Properties["debugLogCount"] = StaticApplicationLoggingMetrics.DebugCount;
      logEvent.Properties["traceLogCount"] = StaticApplicationLoggingMetrics.TraceCount;
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
