using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using freebyTech.Common.ExtensionMethods;
using freebyTech.Common.Logging.Interfaces;

namespace freebyTech.Common.Logging
{
    /// <summary>
    /// 
    /// This class provides the base for instrumentation logging services for an application,
    /// it also sets the proper message type to "Instrumentation" in log message payloads.
    /// 
    /// You use this class directly or subclass this class to define your own instrumentation logging, just override the
    /// <code>SetDerivedCustomProperties()</code> method to set any extra custom properties.
    /// 
    /// To Use this class register its type with the IServiceCollection during ConfigureServices like this:
    ///
    /// <code>
    ///   services.AddScoped<IInstrumentationLogger, BasicInstrumentationLogger>((ctx) =>
    ///   {
    ///       return new BasicInstrumentationLogger(parentApplication, applicationLogginId);
    ///   });
    /// </code>
    /// 
    /// You can also register this class by running <code>services.AddBasicLoggingServices()</code>
    /// 
    /// </summary>
    public class BasicInstrumentationLogger : LoggerBase, IInstrumentationLogger
  {
    public BasicInstrumentationLogger(Assembly parentApplication, string applicationLoggingId, ILogFrameworkAgent frameworkLogger) : base(parentApplication, LoggingMessageTypes.Instrumentation.ToString(), applicationLoggingId, frameworkLogger) { }

    public BasicInstrumentationLogger(string parentApplicationName, string parentApplicationVersion, string applicationLoggingId, ILogFrameworkAgent frameworkLogger) : base(parentApplicationName, parentApplicationVersion, LoggingMessageTypes.Instrumentation.ToString(), applicationLoggingId, frameworkLogger) { }

    #region Properties

    public string MethodSignature { get; private set; }
    private long ExecutionTime { get; set; }
    private double ExecutionTimeMinutes { get; set; }
    private long ItemCount { get; set; }
    private long FailedItemCount { get; set; }
    private long ByteCount { get; set; }
    private long FailedByteCount { get; set; }

    #endregion

    #region Instrumentation Logging Methods

    public Stopwatch SW { get; private set; }

    /// <summary>
    /// Initializes this class using the method's call signature as the signature for the log message.
    /// </summary>
    /// <param name="mb"></param>
    public void InitializeExecutionLogging(MethodBase mb)
    {
      SW = Stopwatch.StartNew();
      var methodWithReturnType = mb as MethodInfo;
      MethodSignature = methodWithReturnType != null ? methodWithReturnType.MethodSignature() : mb.MethodSignature();
    }

    /// <summary>
    /// Initializes this class using a custom "method description" as the signature for the log message.
    /// </summary>
    /// <param name="methodDescription"></param>
    public void InitializeExecutionLogging(string methodDescription)
    {
      SW = Stopwatch.StartNew();
      MethodSignature = methodDescription;
    }

    /// <summary>
    /// Resets the instrumentation properties of the logger and also Restarts the stopwatch.
    /// </summary>
    public void Restart()
    {
      if(SW == null) throw new InvalidOperationException($"{nameof(InitializeExecutionLogging)} has not been called.");

      ExecutionTime = 0;
      ExecutionTimeMinutes = 0.0;
      ItemCount = 0;
      ByteCount = 0;
      FailedItemCount = 0;
      FailedByteCount = 0;

      SW.Restart();
    }

    public void LogInfoWithTime(string message, string data = null, bool resetStopwatch = false, long itemCount = 0, long byteCount = 0, long failedItemCount = 0, long failedByteCount = 0)
    {
      if(SW == null) throw new InvalidOperationException($"{nameof(InitializeExecutionLogging)} has not been called.");

      ExecutionTime = SW.ElapsedMilliseconds;
      ExecutionTimeMinutes = SW.Elapsed.TotalMinutes;
      ItemCount = itemCount;
      ByteCount = byteCount;
      FailedItemCount = failedItemCount;
      FailedByteCount = failedByteCount;

      LogInfo(message, data);

      if (resetStopwatch)
      {
        SW.Restart();
      }
    }

    public void PushInfoWithTime(string message)
    {
      if(SW == null) throw new InvalidOperationException($"{nameof(InitializeExecutionLogging)} has not been called.");
      
      PushInfo(SW.AddTimeToMessage(message));
    }

    public void PushInfoWithTime(string key, string value)
    {
      if(SW == null) throw new InvalidOperationException($"{nameof(InitializeExecutionLogging)} has not been called.");
      
      PushInfo(key, SW.AddTimeToMessage(value));
    }

    public void LogWarnWithTime(string message, string data = null, bool resetStopwatch = false, long itemCount = 0, long byteCount = 0, long failedItemCount = 0, long failedByteCount = 0)
    {
      if(SW == null) throw new InvalidOperationException($"{nameof(InitializeExecutionLogging)} has not been called.");
      
      ExecutionTime = SW.ElapsedMilliseconds;
      ExecutionTimeMinutes = SW.Elapsed.TotalMinutes;
      ItemCount = itemCount;
      ByteCount = byteCount;
      FailedItemCount = failedItemCount;
      FailedByteCount = failedByteCount;

      LogWarn(message, data);
      if (resetStopwatch)
      {
        SW.Restart();
      }
    }

    public void PushWarnWithTime(string message)
    {
      if(SW == null) throw new InvalidOperationException($"{nameof(InitializeExecutionLogging)} has not been called.");
      
      PushWarn(SW.AddTimeToMessage(message));
    }

    public void PushWarnWithTime(string key, string value)
    {
      if(SW == null) throw new InvalidOperationException($"{nameof(InitializeExecutionLogging)} has not been called.");
      
      PushWarn(key, SW.AddTimeToMessage(value));
    }

    public void LogErrorWithTime(string message, string data = null, Exception exceptionInfo = null, bool resetStopwatch = false, long itemCount = 0, long byteCount = 0, long failedItemCount = 0, long failedByteCount = 0)
    {
      if(SW == null) throw new InvalidOperationException($"{nameof(InitializeExecutionLogging)} has not been called.");
      
      ExecutionTime = SW.ElapsedMilliseconds;
      ExecutionTimeMinutes = SW.Elapsed.TotalMinutes;
      ItemCount = itemCount;
      ByteCount = byteCount;
      FailedItemCount = failedItemCount;
      FailedByteCount = failedByteCount;

      LogError(message, exceptionInfo, data);
      if (resetStopwatch)
      {
        SW.Restart();
      }
    }

    public void PushErrorWithTime(string message)
    {
      if(SW == null) throw new InvalidOperationException($"{nameof(InitializeExecutionLogging)} has not been called.");
      
      PushError(SW.AddTimeToMessage(message));
    }

    public void PushErrorWithTime(string key, string value)
    {
      if(SW == null) throw new InvalidOperationException($"{nameof(InitializeExecutionLogging)} has not been called.");
      
      PushError(key, SW.AddTimeToMessage(value));
    }

    public void LogExecutionComplete(long itemCount = 1, long byteCount = 0, string data = null, long failedItemCount = 0, long failedByteCount = 0)
    {
      if (SW == null) throw new InvalidOperationException($"{nameof(InitializeExecutionLogging)} has not been called.");
      
      SW.Stop();

      ExecutionTime = SW.ElapsedMilliseconds;
      ExecutionTimeMinutes = SW.Elapsed.TotalMinutes;
      ItemCount = itemCount;
      ByteCount = byteCount;
      FailedItemCount = failedItemCount;
      FailedByteCount = failedByteCount;

      LogInfo("Execution Complete", data);
    }

    public void LogExecutionCompleteAsWarn(long itemCount = 1, long byteCount = 0, string data = null, long failedItemCount = 0, long failedByteCount = 0, Exception exceptionInfo = null)
    {
      if (SW == null) throw new InvalidOperationException($"{nameof(InitializeExecutionLogging)} has not been called.");

      SW.Stop();

      ExecutionTime = SW.ElapsedMilliseconds;
      ExecutionTimeMinutes = SW.Elapsed.TotalMinutes;
      ItemCount = itemCount;
      ByteCount = byteCount;
      FailedItemCount = failedItemCount;
      FailedByteCount = failedByteCount;

      LogWarn("Execution Complete", data, exceptionInfo);
    }

    public void LogExecutionCompleteAsError(long itemCount = 0, long byteCount = 0, string data = null, long failedItemCount = 1, long failedByteCount = 0, Exception exceptionInfo = null)
    {
      if (SW == null) throw new InvalidOperationException($"{nameof(InitializeExecutionLogging)} has not been called.");
      
      SW.Stop();

      ExecutionTime = SW.ElapsedMilliseconds;
      ExecutionTimeMinutes = SW.Elapsed.TotalMinutes;
      ItemCount = itemCount;
      ByteCount = byteCount;
      FailedItemCount = failedItemCount;
      FailedByteCount = failedByteCount;

      LogError("Execution Complete", exceptionInfo, data);
    }

    public void LogExecutionCompleteAsFatal(long itemCount = 0, long byteCount = 0, string data = null, long failedItemCount = 1, long failedByteCount = 0, Exception exceptionInfo = null)
    {
      if (SW == null) throw new InvalidOperationException($"{nameof(InitializeExecutionLogging)} has not been called.");
      
      SW.Stop();

      ExecutionTime = SW.ElapsedMilliseconds;
      ExecutionTimeMinutes = SW.Elapsed.TotalMinutes;
      ItemCount = itemCount;
      ByteCount = byteCount;
      FailedItemCount = failedItemCount;
      FailedByteCount = failedByteCount;

      LogFatal("Execution Complete", exceptionInfo, data);
    }


    #endregion

    #region Override Methods

    protected sealed override void SetCustomProperties(Dictionary<string, object> customProperties)
    {
      customProperties["methodSignature"] = MethodSignature;
      customProperties["executionTimeMS"] = ExecutionTime;
      customProperties["executionTimeMinutes"] = ExecutionTimeMinutes;
      customProperties["itemCount"] = ItemCount;
      customProperties["byteCount"] = ByteCount;
      customProperties["failedItemCount"] = FailedItemCount;
      customProperties["failedByteCount"] = FailedByteCount;
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
