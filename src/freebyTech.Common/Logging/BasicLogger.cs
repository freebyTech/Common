using System.Reflection;
using freebyTech.Common.Logging.Interfaces;
using System.Collections.Generic;

namespace freebyTech.Common.Logging
{
    /// <summary>
    /// 
    /// This class provides basic logging services for an application,
    /// it also sets the proper message type to "LogMessage" in log message payloads.
    /// 
    /// To Use this class register its type with the IServiceCollection during ConfigureServices like this:
    ///
    /// <code>
    ///   services.AddScoped<IBasicLogger, BasicLogger>((ctx) =>
    ///   {
    ///       return new BasicLogger(parentApplication, applicationLogginId);
    ///   });
    /// </code>
    /// 
    /// You can also register this class by running <code>services.AddBasicLoggingServices()</code>
    /// 
    /// </summary>
    public sealed class BasicLogger : LoggerBase
    {
        public BasicLogger(Assembly parentApplication, string applicationLoggingId, ILogFrameworkAgent frameworkLogger) : base(parentApplication, LoggingMessageTypes.LogMessage.ToString(), applicationLoggingId, frameworkLogger) { }

        public BasicLogger(string parentApplicationName, string parentApplicationVersion, string applicationLoggingId, ILogFrameworkAgent frameworkLogger) : base(parentApplicationName, parentApplicationVersion, LoggingMessageTypes.LogMessage.ToString(), applicationLoggingId, frameworkLogger) { }

        #region Override Methods

        protected  override void SetCustomProperties(Dictionary<string, object> customProperties)
        {
        }

        #endregion
    }
}
