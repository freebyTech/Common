using System;
using System.Diagnostics;

namespace freebyUtil.Common.ExtensionMethods
{
  public static class StopwatchExtensions
  {
    /// <summary>
    /// Added the number of milliseconds from a running stopwatch to the end of a string.
    /// Handy for logging.
    /// </summary>
    /// <returns></returns>
    [DebuggerStepThrough]
    public static string AddTimeToMessage(this Stopwatch sw, string message)
    {
      return ($"{message} [{sw.ElapsedMilliseconds} ms]");
    }
  }

}
