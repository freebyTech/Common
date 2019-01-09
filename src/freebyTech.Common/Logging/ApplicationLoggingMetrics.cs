using System;

namespace freebyUtil.Logging
{
  public class ApplicationLoggingMetrics
  {
    #region Static Properties

    public static int ErrorCount { get; set; }
    public static int WarnCount { get; set; }
    public static int InfoCount { get; set; }
    public static int FatalCount { get; set; }
    public static int DebugCount { get; set; }
    public static int TraceCount { get; set; }
    public static DateTime StartTime { get; set; } = DateTime.Now;
    public static DateTime? EndTime { get; set; }

    #endregion

    #region Static Methods

    public static TimeSpan TotalTime()
    {
      if (EndTime == null) { EndTime = DateTime.Now; }
      return EndTime.Value - StartTime;
    }
    public static void StopTiming()
    {
      EndTime = DateTime.Now;
    }

    public static void Reset()
    {
      StartTime = DateTime.Now;
      EndTime = null;
      ErrorCount = 0;
      WarnCount = 0;
      InfoCount = 0;
      FatalCount = 0;
      DebugCount = 0;
      TraceCount = 0;
    }

    #endregion

  }
}
