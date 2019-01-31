using freebyTech.Common.Logging.Interfaces;
using System;
using System.Collections.Generic;
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
    public class BasicRunMetricsLogger : LoggerBase, IRunMetricsLogger
  {
    public BasicRunMetricsLogger(Assembly parentApplication, string applicationLoggingId, ILogFrameworkAgent frameworkLogger) : base(parentApplication, LoggingMessageTypes.Instrumentation.ToString(), applicationLoggingId, frameworkLogger){}

    public BasicRunMetricsLogger(string parentApplicationName, string parentApplicationVersion, string applicationLoggingId, ILogFrameworkAgent frameworkLogger) : base(parentApplicationName, parentApplicationVersion, LoggingMessageTypes.Instrumentation.ToString(), applicationLoggingId, frameworkLogger) { }

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

    protected sealed override void SetCustomProperties(Dictionary<string, object> customProperties)
    {
      // Set the properties for centralized logging.
      customProperties["startTime"] = StaticApplicationLoggingMetrics.StartTime.ToString();
      if (StaticApplicationLoggingMetrics.EndTime.HasValue)
      {
        customProperties["endTime"] = StaticApplicationLoggingMetrics.EndTime.Value.ToString();
      }
      customProperties["executionTimeMinutes"] = StaticApplicationLoggingMetrics.TotalTime().TotalMinutes;
      customProperties["executionTimeMS"] = StaticApplicationLoggingMetrics.TotalTime().Milliseconds;
      customProperties["fatalLogCount"] = StaticApplicationLoggingMetrics.FatalCount;
      customProperties["errorLogCount"] = StaticApplicationLoggingMetrics.ErrorCount;
      customProperties["warnLogCount"] = StaticApplicationLoggingMetrics.WarnCount;
      customProperties["infoLogCount"] = StaticApplicationLoggingMetrics.InfoCount;
      customProperties["debugLogCount"] = StaticApplicationLoggingMetrics.DebugCount;
      customProperties["traceLogCount"] = StaticApplicationLoggingMetrics.TraceCount;
      SetDerivedClassCustomProperties(customProperties);
    }

    /// <summary>
    /// If implementing a logger on top of this logger you should set your custom properties here rather 
    /// than in SetCustomProperties which is already being used by this class.
    /// </summary>
    /// <param name="logEvent"></param>
    protected virtual void SetDerivedClassCustomProperties(Dictionary<string, object> customProperties)
    {

    }

    #endregion

  }
}
