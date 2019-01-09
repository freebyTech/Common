using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using NLog;

namespace freebyUtil.Logging
{
  /// <summary>
  /// 
  /// This class provides basic validation and logging services for an application,
  /// it also sets the proper message type to "Validation" in log message payloads.
  /// 
  /// You can also subclass this class to define your own Validation logging, just override the
  /// SetDerivedCustomProperties() method to set any extra custom properties.
  /// 
  /// To Use this class place the following line at the top of any class you want to log validation messages from. 
  /// 
  /// private static readonly ValidationLoggingBase Log = new ValidationLoggingBase(Assembly.GetExecutingAssembly(), LoggingConstants.ApplicationLoggingId);
  /// 
  /// This assumes a constant called ApplicationLoggingId is defined in a class called LoggingConstants.
  /// 
  /// </summary>
  public class ValidationLoggingBase : LoggingBase
  {
    public ValidationLoggingBase(Assembly parentApplication, string applicationLoggingId) : base(parentApplication, LoggingMessageTypes.Validation.ToString(), applicationLoggingId) { }

    public ValidationLoggingBase(string parentApplicationName, string parentApplicationVersion, string applicationLoggingId) : base(parentApplicationName, parentApplicationVersion, LoggingMessageTypes.Validation.ToString(), applicationLoggingId) { }

    protected sealed override void SetCustomProperties(LogEventInfo logEvent)
    {
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

    #region Public Methods

    /// <summary>
    /// Will log many of the current application and configuration settings of the current application to a standard log. Can be used for after the fact validation of 
    /// application settings for failure application failure analysis.
    /// </summary>
    /// <param name="fileTypeLogging"></param>
    public void LogConfigSettings(bool fileTypeLogging = false)
    {
      // If using a file logger then push this info into data, if pushed to centralized logging this will be independent JSON properties.
      if (fileTypeLogging)
      {
        PushLoggingProperties();
      }

      PushAssemblyVersions();

      PushApplicationSettings();

      PushConnectionStrings();

      LogInfo("Application Configuration Settings");
    }

    /// <summary>
    /// Used as a test of NLog configuration writing of multiple local logging files.
    /// </summary>
    public void LogLogLevels()
    {
      LogHeaderTrace("Testing NLog Log Trace Level");
      PushTrace("Push Trace message.");
      LogTrace("Log Trace message.");
      LogTrace("");

      LogHeaderDebug("Testing NLog Log Debug Level");
      PushDebug("Push Debug message.");
      LogDebug("Log Debug message.");
      LogDebug("");

      LogHeaderInfo("Testing NLog Log Info Level");
      PushInfo("Push Info message.");
      LogInfo("Log Info message.");
      LogInfo("");

      LogHeaderWarn("Testing NLog Log Warn Level");
      PushWarn("Push Warn message.");
      LogWarn("Log Warn message.");
      LogWarn("");

      LogHeaderError("Testing NLog Log Error Level");
      PushError("Push Error message.");
      LogError("Log Error message.", new Exception("Test Exception!"));
      LogError("");

      LogHeaderFatal("Testing NLog Log Fatal Level");
      PushFatal("Push Fatal message.");
      LogFatal("Log Fatal message.", new Exception("Test Exception!"));
      LogFatal("");
    }


    /// <summary>
    /// Will check and log the status of a file write operation to the passed directory. It is up to the application to shut down
    /// if this operation returns false and should represent application configuration failure.
    /// </summary>
    /// <param name="path"></param>
    /// <returns>True if write operations succeed, otherwise false.</returns>
    public bool LogWriteAccessToDirectory(string path)
    {
      string logMessage;
      var status = TestWriteAccessToDirectory(path, out logMessage);
      LogInfo(logMessage);
      return status;
    }

    /// <summary>
    /// Will check and push to the info queue the status of a file write operation to the passed directory. It is up to the application to shut down
    /// if this operation returns false and should represent application configuration failure.
    /// </summary>
    /// <param name="path"></param>
    /// <returns>True if write operations succeed, otherwise false.</returns>
    public bool PushWriteAccessToDirectory(string path)
    {
      string logMessage;
      var status = TestWriteAccessToDirectory(path, out logMessage);
      PushInfo(logMessage);
      return status;
    }

    private bool TestWriteAccessToDirectory(string path, out string logMessage)
    {
      try
      {
        using (File.Create(Path.Combine(path, $"Access-{Guid.NewGuid()}.txt"), 1, FileOptions.DeleteOnClose))
        {
        }
        logMessage = $"File write operation to '{path}' successful.";
        return true;
      }
      catch (Exception ex)
      {
        logMessage = $"File write operation to '{path}' FAILED with exception message {ex.Message}.";
        return false;
      }
    }

    #endregion

    #region Private Info Methods 

    private void PushAssemblyVersions()
    {
      PushHeaderInfo("Referenced Assemblies");
      var asmNames = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
      var fullNames = asmNames.Select(nm => nm.FullName).ToList();
      foreach (var fullName in fullNames)
      {
        PushInfo(fullName);
      }
    }

    private void PushConnectionStrings()
    {
      PushHeaderInfo("Config Connection Strings");
      foreach (ConnectionStringSettings connectionString in ConfigurationManager.ConnectionStrings)
      {
        try
        {
          var sqlBuilder = new SqlConnectionStringBuilder(connectionString.ToString());
          if (!string.IsNullOrEmpty(sqlBuilder.Password))
          {
            sqlBuilder.Password = new string('*', sqlBuilder.Password.Length);
          }

          PushInfo(connectionString.Name, sqlBuilder.ConnectionString);
        }
        catch (Exception ex)
        {
          PushInfo($"Unable to translate connectionstring into valid connection string: {ex.Message}");
        }
      }
    }

    private void PushApplicationSettings()
    {
      PushHeaderInfo("Config Application Settings");
      foreach (var key in ConfigurationManager.AppSettings)
      {
        PushInfo(key.ToString(), ConfigurationManager.AppSettings[key.ToString()]);
      }
    }



    #endregion

  }
}
