namespace freebyTech.Common.Logging.Interfaces
{
    public interface IValidationLogger : IBasicLogger
    {
        /// <summary>
        /// Will log many of the current application and configuration settings of the current application to a standard log. Can be used for after the fact validation of 
        /// application settings for failure application failure analysis.
        /// </summary>
        /// <param name="fileTypeLogging"></param>
        void LogConfigSettings(bool fileTypeLogging = false);

        /// <summary>
        /// Used as a test of NLog configuration writing of multiple local logging files.
        /// </summary>
        void LogLogLevels();

        /// <summary>
        /// Will check and log the status of a file write operation to the passed directory. It is up to the application to shut down
        /// if this operation returns false and should represent application configuration failure.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>True if write operations succeed, otherwise false.</returns>
        bool LogWriteAccessToDirectory(string path);

        /// <summary>
        /// Will check and push to the info queue the status of a file write operation to the passed directory. It is up to the application to shut down
        /// if this operation returns false and should represent application configuration failure.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>True if write operations succeed, otherwise false.</returns>
        bool PushWriteAccessToDirectory(string path);
    }
}