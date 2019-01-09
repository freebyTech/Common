using NLog;
using System.Reflection;

namespace freebyTech.Common.Logging
{
  /// <summary>
  /// 
  /// This class provides basic logging services for an application,
  /// it also sets the proper message type to "LogMessage" in log message payloads.
  /// 
  /// To Use this class place the following line at the top of any class you want to log messages from. 
  /// 
  /// private static readonly SimpleLogger Log = new SimpleLogger(Assembly.GetExecutingAssembly(), LoggingConstants.ApplicationLoggingId);
  /// 
  /// This assumes a constant called ApplicationLoggingId is defined in a class called LoggingConstants.
  /// 
  /// </summary>
  public sealed class SimpleLogger : LoggingBase
  {
    public SimpleLogger(Assembly parentApplication, string applicationLoggingId) : base(parentApplication, LoggingMessageTypes.LogMessage.ToString(), applicationLoggingId) { }

    public SimpleLogger(string parentApplicationName, string parentApplicationVersion, string applicationLoggingId) : base(parentApplicationName, parentApplicationVersion, LoggingMessageTypes.LogMessage.ToString(), applicationLoggingId) { }

    #region Override Methods

    protected override void SetCustomProperties(LogEventInfo logEvent)
    {
    }

    #endregion
  }
}
