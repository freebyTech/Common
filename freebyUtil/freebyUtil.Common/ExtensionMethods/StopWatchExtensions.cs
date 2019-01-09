using System;
using System.Diagnostics;

namespace freebyUtil.Common.ExtensionMethods
{
  public static class StopwatchExtensions
  {
    public static string AddTimeToMessage(this Stopwatch sw, string message)
    {
      return ($"{message} [{sw.ElapsedMilliseconds} ms]");
    }
  }

}
