using System;

namespace freebyTech.Common.Threading
{
  /// <summary>
  /// Implements a threadsafe random number generator, as derived from here:
  /// 
  /// http://blogs.msdn.com/b/pfxteam/archive/2009/02/19/9434171.aspx
  /// 
  /// </summary>
  public sealed class ThreadSafeRandom : Random
  {
    private object _lock = new object();

    public override int Next()
    {
      lock (_lock) return base.Next();
    }

    public override int Next(int maxValue)
    {
      lock (_lock) return base.Next(maxValue);
    }

    public override int Next(int minValue, int maxValue)
    {
      lock (_lock) return base.Next(minValue, maxValue);
    }

    public override void NextBytes(byte[] buffer)
    {
      lock (_lock) base.NextBytes(buffer);
    }

    public override double NextDouble()
    {
      lock (_lock) return base.NextDouble();
    }
  }

  public static class StaticThreadSafeRandom
  {
    static StaticThreadSafeRandom()
    {
      Instance = new ThreadSafeRandom();
    }

    public static ThreadSafeRandom Instance { get; private set; }
  }
}