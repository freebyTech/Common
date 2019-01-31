using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Text;
using freebyTech.Common.ExtensionMethods;
using freebyTech.Common.Logging.Core;
using freebyTech.Common.Logging.Interfaces;

namespace freebyTech.Common.Logging.Core
{
    /// <summary>
    /// This is the abstract base class for all Logging Class Types.
    /// </summary>
    public abstract class LoggerBase : IBasicLogger
    {
        #region Private Variables

        private List<PushLogItem> _pushLogItems { get; set; } = new List<PushLogItem>();
        
        private Stopwatch SW { get; set; }

        private ILogFrameworkAgent _frameworkLogger;

        #endregion

        #region Constructors

        protected LoggerBase(Assembly parentApplication, string messageType, string applicationLoggingId, ILogFrameworkAgent frameworkLogger)
        {
            BuildLoggerInfo(messageType, applicationLoggingId);
            BuildApplicationInfo(parentApplication);
            _frameworkLogger = frameworkLogger;
            ConstructCommon();
        }

        protected LoggerBase(string parentApplicationName, string parentApplicationVersion, string messageType, string applicationLoggingId, ILogFrameworkAgent frameworkLogger)
        {
            BuildLoggerInfo(messageType, applicationLoggingId);
            BuildApplicationInfo(parentApplicationName, parentApplicationVersion);
            _frameworkLogger = frameworkLogger;
            ConstructCommon();
        }

        private void ConstructCommon()
        {
            SW = Stopwatch.StartNew();
            SeperatorLineOnEmptyLog = true;
            SeperatorChar = SeperatorLineChars.Line;
            SeperatorLineLength = 80;
            LogDurationInPushes = true;
        }

        #endregion

        public void RestartDuration() {
            SW.Restart();
        }

        public LoggerDuration GetDuration() 
        {
            return new LoggerDuration { Ms = SW.ElapsedMilliseconds, Minutes = SW.Elapsed.TotalMinutes };
        }

        #region Properties of the LogEventInfo

        /// <summary>
        /// The Application identifier used to identify the application or application "sub-component"
        /// </summary>
        public string ApplicationLoggingId { get; private set; }

        /// <summary>
        /// The type of logging message that this logger logs, i.e. ApplicationLog / Instrumentation / RequestResponse
        /// </summary>
        public string MessageType { get; private set; }

        /// <summary>
        /// Derived property of Application Logging ID and Message Type, used when constructing the nlog logger as its Name constructor.
        /// 
        /// i.e. - fb-restapi-webengine-instrumentation
        ///        fb-restapi-webengine-requestresponse
        ///        fb-restapi-webengine-logmessage
        /// 
        /// This allows us to setup rules based upon different message types in the nlog.config.
        /// </summary>
        public string FullLoggingName { get; private set; }

        public string ApplicationName { get; private set; }

        public string ApplicationVersion { get; private set; }

        public string LoggingAssemblyVersion { get; private set; }

        // The type name 'WindowsIdentity' could not be found in the namespace 'System.Security.Principal'.
        // This type has been forwarded to assembly 'System.Security.Principal.Windows, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
        // TODO: This is not compatible with non windows projects?
        //private string _userName = string.Empty;
        public string UserName
        {
            get
            {
                //if (string.IsNullOrEmpty(_userName))
                //{
                //    var windowsIdentity = System.Security.Principal.WindowsIdentity.GetCurrent();
                //    _userName = windowsIdentity != null ? windowsIdentity.Name : "Unknown";
                //}
                //return (_userName);
                return "";
            }
        }

        private string _processName = string.Empty;
        public string ProcessName
        {
            get
            {
                if (string.IsNullOrEmpty(_processName))
                {
                    _processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                }
                return (_processName);
            }
        }

        private string _hostName = string.Empty;
        public string HostName
        {
            get
            {
                if (string.IsNullOrEmpty(_hostName))
                {
                    _hostName = Dns.GetHostName();
                }
                return (_hostName);
            }
        }

        #endregion

        #region Properties That Affect Logging Behavior

        /// <summary>
        /// If true, will log or push a line of seperator characters when a LogX or PushX method is run with an empty string.
        /// </summary>
        public bool LogDurationInPushes { get; set; }

        /// <summary>
        /// If true, will log or push a line of seperator characters when a LogX or PushX method is run with an empty string.
        /// </summary>
        public bool SeperatorLineOnEmptyLog { get; set; }

        /// <summary>
        /// The seperator character to use for empty writes and also headers.
        /// </summary>
        public char SeperatorChar { get; set; }

        /// <summary>
        /// The length of seperator lines to write when doing empty writes or generating headers.
        /// </summary>
        public int SeperatorLineLength { get; set; }

        #endregion

        #region Queue Count Properties

        public int PushLogCount
        {
            get
            {
                if (_pushLogItems == null) return 0;
                return _pushLogItems.Count;
            }
        }

        #endregion

        #region Must Override Methods

        /// <summary>
        /// This must override method is what sets custom properties in the log event based upon derived Logging type.
        /// </summary>
        /// <param name="logEventInfo"></param>
        protected abstract void SetCustomProperties(Dictionary<string, object> customProperties);

        #endregion

        #region Push Data Methods

        private void PushValue(GenericLogLevel level, string key, string value)
        {
            if(LogDurationInPushes) {
                _pushLogItems.Add(new PushLogItem(level, key, value, SW.ElapsedMilliseconds));
            }
            else {
                _pushLogItems.Add(new PushLogItem(level, key, value, -1));
            }
            
        }

        public void PushTrace(string key, string value)
        {
            PushValue(GenericLogLevel.Trace, key, value);
        }
        public void PushDebug(string key, string value)
        {
            PushValue(GenericLogLevel.Debug, key, value);
        }
        public void PushInfo(string key, string value)
        {
            PushValue(GenericLogLevel.Info, key, value);
        }
        public void PushError(string key, string value)
        {
            PushValue(GenericLogLevel.Error, key, value);
        }
        public void PushWarn(string key, string value)
        {
            PushValue(GenericLogLevel.Warn, key, value);
        }
        public void PushFatal(string key, string value)
        {
            PushValue(GenericLogLevel.Fatal, key, value);
        }

        #endregion

        #region Push Line Methods

        private void PushLine(GenericLogLevel level, string line)
        {
            if (string.IsNullOrEmpty(line) && SeperatorLineOnEmptyLog)
            {
                line = SeperatorChar.MakeLine(SeperatorLineLength);
            }

            if(LogDurationInPushes) {
                _pushLogItems.Add(new PushLogItem(level, line, SW.ElapsedMilliseconds));
            }
            else {
                _pushLogItems.Add(new PushLogItem(level, line, -1));
            }            
        }

        public void PushTrace(string line)
        {
            PushLine(GenericLogLevel.Trace, line);
        }
        public void PushTrace()
        {
            PushLine(GenericLogLevel.Trace, string.Empty);
        }
        public void PushDebug(string line)
        {
            PushLine(GenericLogLevel.Debug, line);
        }
        public void PushDebug()
        {
            PushLine(GenericLogLevel.Debug, string.Empty);
        }
        public void PushInfo(string line)
        {
            PushLine(GenericLogLevel.Info, line);
        }
        public void PushInfo()
        {
            PushLine(GenericLogLevel.Info, string.Empty);
        }
        public void PushError(string line)
        {
            PushLine(GenericLogLevel.Error, line);
        }
        public void PushError()
        {
            PushLine(GenericLogLevel.Error, string.Empty);
        }
        public void PushWarn(string line)
        {
            PushLine(GenericLogLevel.Warn, line);
        }
        public void PushWarn()
        {
            PushLine(GenericLogLevel.Warn, string.Empty);
        }
        public void PushFatal(string line)
        {
            PushLine(GenericLogLevel.Fatal, line);
        }
        public void PushFatal()
        {
            PushLine(GenericLogLevel.Fatal, string.Empty);
        }

        #endregion

        #region Push Header Methods

        private void PushHeader(GenericLogLevel level, string line)
        {
            PushLine(level, string.Empty);
            PushLine(level, EncloseMessageInHeader(line));
            PushLine(level, string.Empty);
        }

        public void PushHeaderTrace(string line)
        {
            PushHeader(GenericLogLevel.Trace, line);
        }
        public void PushHeaderDebug(string line)
        {
            PushHeader(GenericLogLevel.Debug, line);
        }
        public void PushHeaderInfo(string line)
        {
            PushHeader(GenericLogLevel.Info, line);
        }
        public void PushHeaderError(string line)
        {
            PushHeader(GenericLogLevel.Error, line);
        }
        public void PushHeaderWarn(string line)
        {
            PushHeader(GenericLogLevel.Warn, line);
        }
        public void PushHeaderFatal(string line)
        {
            PushLine(GenericLogLevel.Fatal, line);
        }

        #endregion

        #region General Queue Methods

        public void ClearQueueData()
        {
            _pushLogItems = new List<PushLogItem>();
        }

        /// <summary>
        /// Push all public properties to the Info stack.
        /// </summary>
        public void PushLoggingProperties()
        {
            PushLoggingProperties(this);
        }

        protected void PushLoggingProperties<T>(T target)
        {
            var targetType = target.GetType();
            var publicProperties = targetType.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            PushHeaderInfo("Application / Logging Properties");
            PushPropertiesToInfo(target, publicProperties);
        }

        private void PushPropertiesToInfo<T>(T target, PropertyInfo[] properties)
        {
            foreach (var prp in properties)
            {
                if (prp.PropertyType == typeof(string))
                {
                    PushInfo(prp.Name, prp.GetValue(target, null).ToString());
                }
            }
        }

        #endregion

        #region Log Header Methods

        private void LogHeader(GenericLogLevel level, string message)
        {
            LogThis(CreateLogEvent(level, string.Empty, null, null));
            LogThis(CreateLogEvent(level, EncloseMessageInHeader(message), null, null));
            LogThis(CreateLogEvent(level, string.Empty, null, null));
        }

        public void LogHeaderTrace(string line)
        {
            LogHeader(GenericLogLevel.Trace, line);
        }
        public void LogHeaderDebug(string line)
        {
            LogHeader(GenericLogLevel.Debug, line);
        }
        public void LogHeaderInfo(string line)
        {
            LogHeader(GenericLogLevel.Info, line);
        }
        public void LogHeaderError(string line)
        {
            LogHeader(GenericLogLevel.Error, line);
        }
        public void LogHeaderWarn(string line)
        {
            LogHeader(GenericLogLevel.Warn, line);
        }
        public void LogHeaderFatal(string line)
        {
            LogHeader(GenericLogLevel.Fatal, line);
        }

        private string EncloseMessageInHeader(string message)
        {
            // Just return unmodified message if current Special Character settings make no sense for a header of this type.
            if (SeperatorLineLength < 20 | !SeperatorLineOnEmptyLog) return message;

            var maxMessageLength = SeperatorLineLength - 8;

            if (message.Length > maxMessageLength)
            {
                message = message.Substring(0, maxMessageLength - 1);
            }

            // LogLineLength chars wide - message - 4 char space on each side of message
            var leftRightPad = (((double)SeperatorLineLength - message.Length - 8) / 2);
            var specialCharsLeft = new string(SeperatorChar, (int)Math.Floor(leftRightPad));
            var specialCharsRight = new string(SeperatorChar, (int)Math.Ceiling(leftRightPad));
            var space = new string(' ', 4);

            return string.Format("{0}{1}{2}{1}{3}", specialCharsLeft, space, message, specialCharsRight);
        }

        #endregion

        #region Log Trace
        /// <summary>
        /// Empty LogTrace will write out any Trace data that has been pushed via the 'PushTrace' Method
        /// </summary>
        public void LogTrace()
        {
            LogTrace(null, string.Empty);
        }
        public void LogTrace(string message)
        {
            LogTrace(message, string.Empty);
        }

        public void LogTrace(string message, string data)
        {
            LogThis(CreateLogEvent(GenericLogLevel.Trace, message, null, data));
        }

        #endregion

        #region Log Debug
        /// <summary>
        /// Empty LogDebug will write out any Debug data that has been pushed via the 'PushDebug' Method
        /// </summary>
        public void LogDebug()
        {
            LogDebug(null, string.Empty);
        }
        public void LogDebug(string message)
        {
            LogDebug(message, string.Empty);
        }
        public void LogDebug(string message, string data)
        {
            LogThis(CreateLogEvent(GenericLogLevel.Debug, message, null, data));
        }

        #endregion

        #region Log Info

        /// <summary>
        /// Empty LogInfo will write out any Info data that has been pushed via the 'PushInfo' Method
        /// </summary>
        public void LogInfo()
        {
            LogInfo(null, string.Empty);
        }
        public void LogInfo(string message)
        {
            LogInfo(message, string.Empty);
        }

        public void LogInfo(string message, string data)
        {
            LogThis(CreateLogEvent(GenericLogLevel.Info, message, null, data));
        }

        #endregion

        #region Log Warn
        /// <summary>
        /// Empty LogWarn will write out any Warn data that has been pushed via the 'PushWarn' Method
        /// </summary>
        public void LogWarn()
        {
            LogWarn(null, string.Empty);
        }
        public void LogWarn(string message)
        {
            LogWarn(message, string.Empty);
        }

        public void LogWarn(string message, string data)
        {
            LogThis(CreateLogEvent(GenericLogLevel.Warn, message, null, data));
        }

        public void LogWarn(string message, string data, Exception exceptionInfo)
        {
            LogThis(CreateLogEvent(GenericLogLevel.Warn, message, exceptionInfo, data));
        }

        #endregion

        #region Log Errors
        /// <summary>
        /// Empty LogError will write out any Error data that has been pushed via the 'PushError' Method
        /// </summary>
        public void LogError()
        {
            LogError(null, null);
        }

        public void LogError(string message)
        {
            LogError(message, null);
        }
        public void LogError(Exception ex)
        {
            LogError(string.Empty, ex);
        }

        public void LogError(string message, Exception ex)
        {
            LogThis(CreateLogEvent(GenericLogLevel.Error, message, ex, string.Empty));
        }

        public void LogError(string message, Exception ex, string data)
        {
            LogThis(CreateLogEvent(GenericLogLevel.Error, message, ex, data));
        }

        #endregion

        #region Log Fatal
        /// <summary>
        /// Empty LogFatal will write out any Fatal data that has been pushed via the 'PushFatal' Method
        /// </summary>
        public void LogFatal()
        {
            LogFatal(null, null);
        }

        public void LogFatal(string message)
        {
            LogFatal(message, null);
        }
        public void LogFatal(Exception ex)
        {
            LogFatal(string.Empty, ex);
        }

        public void LogFatal(string message, Exception ex)
        {
            LogThis(CreateLogEvent(GenericLogLevel.Fatal, message, ex, string.Empty));
        }

        public void LogFatal(string message, Exception ex, string data)
        {
            LogThis(CreateLogEvent(GenericLogLevel.Fatal, message, ex, data));
        }

        #endregion

        #region Private Methods

        private void BuildApplicationInfo(Assembly application)
        {
            var appInfo = application.GetName();
            ApplicationVersion = appInfo.Version.ToString();
            LoggingAssemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            ApplicationName = appInfo.Name;
        }
        private void BuildLoggerInfo(string messageType, string applicationLoggingId)
        {
            ApplicationLoggingId = applicationLoggingId;
            MessageType = messageType;
            FullLoggingName = $"{applicationLoggingId.ToLower()}-{messageType.ToLower()}";
        }

        private void BuildApplicationInfo(string overrideApplicationName, string overrideApplicationVersion)
        {
            ApplicationVersion = overrideApplicationVersion;
            LoggingAssemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            ApplicationName = overrideApplicationName;
        }

        private void LogThis(GenericLogEventInfo logEvent)
        {
            try
            {
                BumpLogCount(logEvent.LogLevel);
                _frameworkLogger.Log(FullLoggingName, logEvent);
            }
            catch (Exception) { }
        }

        private void BumpLogCount(GenericLogLevel logLevel)
        {
            switch (logLevel)
            {
                case GenericLogLevel.Error:
                    StaticApplicationLoggingMetrics.ErrorCount++; break;
                case GenericLogLevel.Info:
                    StaticApplicationLoggingMetrics.InfoCount++; break;
                case GenericLogLevel.Trace:
                    StaticApplicationLoggingMetrics.TraceCount++; break;
                case GenericLogLevel.Debug:
                    StaticApplicationLoggingMetrics.DebugCount++; break;
                case GenericLogLevel.Warn:
                    StaticApplicationLoggingMetrics.WarnCount++; break;
                case GenericLogLevel.Fatal:
                    StaticApplicationLoggingMetrics.FatalCount++; break;
                default: break;
            }
        }

        private List<PushLogItem> GetPushLog() {
            var pl = _pushLogItems;
            _pushLogItems = new List<PushLogItem>();
            return pl;
        }

        private GenericLogEventInfo CreateLogEvent(GenericLogLevel level, string message, Exception ex, string data)
        {
            message = GetMessage(message);

            var logEventInfo = new GenericLogEventInfo(level, MessageType, message, data, ex, GetPushLog());

            logEventInfo.ExtraProperties["severity"] = GetSeverityLevel(level);
            logEventInfo.ExtraProperties["applicationLoggingId"] = ApplicationLoggingId;
            logEventInfo.ExtraProperties["messageType"] = MessageType;
            logEventInfo.ExtraProperties["applicationName"] = ApplicationName;
            logEventInfo.ExtraProperties["applicationVersion"] = ApplicationVersion;
            logEventInfo.ExtraProperties["fullLoggingName"] = FullLoggingName;
            logEventInfo.ExtraProperties["loggingAssemblyVersion"] = LoggingAssemblyVersion;
            logEventInfo.ExtraProperties["userName"] = UserName;
            logEventInfo.ExtraProperties["processName"] = ProcessName;
            logEventInfo.ExtraProperties["hostName"] = HostName;
            logEventInfo.ExtraProperties["data"] = data;

            SetCustomProperties(logEventInfo.ExtraProperties);

            return logEventInfo;
        }

        private static int GetSeverityLevel(GenericLogLevel logLevel)
        {
            switch (logLevel)
            {
                case GenericLogLevel.Trace:
                    return 0;
                case GenericLogLevel.Debug:
                    return 0;
                case GenericLogLevel.Info:
                    return 10;
                case GenericLogLevel.Warn:
                    return 30;
                case GenericLogLevel.Error:
                    return 30;
                case GenericLogLevel.Fatal:
                    return 40;
            }
            return 0;
        }

        protected virtual string GetMessage(string message)
        {
            if (message != null) { return message; }

            message = string.Empty;
            if (!SeperatorLineOnEmptyLog) { return message; }

            message = SeperatorChar.MakeLine(SeperatorLineLength);
            return message;
        }       

        #endregion
    }

}
