namespace freebyTech.Common.Logging.Core
{
    /// <summary>
    /// An enum defining LogLevels not tied to any specific implementation of logging.
    /// </summary>
    public enum GenericLogLevel : int
    {
        /// <summary>
        /// Trace log level. Most verbose level of logging.
        /// </summary>
        Trace = 0,
        /// <summary>
        /// Debug log level.
        /// </summary>
        Debug = 1,
        /// <summary>
        /// Infromationanl log level.
        /// </summary>
        Info = 2,
        /// <summary>
        /// Warning log level.
        /// </summary>
        Warn = 3,
        /// <summary>
        /// Error log level.
        /// </summary>
        Error = 4,
        /// <summary>
        /// Fatal log level.
        /// </summary>
        Fatal = 5
    }
}