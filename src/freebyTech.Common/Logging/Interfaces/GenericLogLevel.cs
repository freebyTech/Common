namespace freebyTech.Common.Logging.Interfaces
{
    /// <summary>
    /// A class defining LogLevels not tied to any specific implementation of logging.
    /// </summary>
    public sealed class GenericLogLevel
    {
        /// <summary>
        /// Trace log level.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Type is immutable")]
        public static readonly GenericLogLevel Trace = new GenericLogLevel("Trace", 0);

        /// <summary>
        /// Debug log level.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Type is immutable")]
        public static readonly GenericLogLevel Debug = new GenericLogLevel("Debug", 1);

        /// <summary>
        /// Info log level.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Type is immutable")]
        public static readonly GenericLogLevel Info = new GenericLogLevel("Info", 2);

        /// <summary>
        /// Warn log level.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Type is immutable")]
        public static readonly GenericLogLevel Warn = new GenericLogLevel("Warn", 3);

        /// <summary>
        /// Error log level.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Type is immutable")]
        public static readonly GenericLogLevel Error = new GenericLogLevel("Error", 4);

        /// <summary>
        /// Fatal log level.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Type is immutable")]
        public static readonly GenericLogLevel Fatal = new GenericLogLevel("Fatal", 5);

        public string Name { get; }
        public int Ordinal { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="LogLevel"/>.
        /// </summary>
        /// <param name="name">The log level name.</param>
        /// <param name="ordinal">The log level ordinal number.</param>
        private GenericLogLevel(string name, int ordinal)
        {
            Name = name;
            Ordinal = ordinal;
        }
    }
}