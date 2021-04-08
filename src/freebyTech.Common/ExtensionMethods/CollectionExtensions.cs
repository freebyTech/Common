using System;
using System.Collections.Generic;
using System.Linq;
using freebyTech.Common.Threading;

namespace freebyTech.Common.ExtensionMethods
{
  public static class CollectionExtensions
  {
    public static IEnumerable<T> TakeRandom<T>(this IEnumerable<T> source, int count)
    {
      var array = source.ToArray();
      return ShuffleInternal(array, Math.Min(count, array.Length)).Take(count);
    }

    private static IEnumerable<T> ShuffleInternal<T>(T[] array, int count)
    {
      for (var n = 0; n < count; n++)
      {
        var k = StaticThreadSafeRandom.Instance.Next(n, array.Length);
        var temp = array[n];
        array[n] = array[k];
        array[k] = temp;
      }

      return array;
    }
  }
}