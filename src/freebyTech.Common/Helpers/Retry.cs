using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace freebyTech.Common.Helpers
{
  /// <summary>
  /// Helper class for retry logic of actions that could fail because of transient conditions like network issues.
  /// </summary>
  public static class Retry
  {
    /// <summary>
    /// Do with execution of retry logic.
    /// </summary>
    /// <param name="action">The method to invoke</param>
    /// <param name="retryInterval">The time between retries</param>
    /// <param name="maxAttemptCount">The maximum number of times to invoke the method</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="AggregateException">The aggregate of exceptions that occured in all the retries.</exception>
    public static T Do<T>(Func<T> action, TimeSpan retryInterval, int maxAttemptCount = 1)
    {
      var exceptions = new List<Exception>();

      for (var attempted = 0; attempted < maxAttemptCount; attempted++)
      {
        try
        {
          if (attempted > 0)
          {
            Thread.Sleep(retryInterval);
          }

          var result = action();
          return result;
        }
        catch (Exception ex)
        {
          exceptions.Add(ex);
        }
      }

      throw new AggregateException(exceptions);
    }

    /// <summary>
    /// Do with execution of retry logic in an async action.
    /// </summary>
    /// <param name="action">The method to invoke</param>
    /// <param name="retryInterval">The time between retries</param>
    /// <param name="maxAttemptCount">The maximum number of times to invoke the method</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="AggregateException">The aggregate of exceptions that occured in all the retries.</exception>
    public static async Task<T> DoAsync<T>(Func<Task<T>> action, TimeSpan retryInterval, int maxAttemptCount = 1)
    {
      var exceptions = new List<Exception>();

      for (var attempted = 0; attempted < maxAttemptCount; attempted++)
      {
        try
        {
          if (attempted > 0)
          {
            Thread.Sleep(retryInterval);
          }

          var result = await action();
          return result;
        }
        catch (Exception ex)
        {
          exceptions.Add(ex);
        }
      }

      throw new AggregateException(exceptions);
    }
  }
}
