using System;
using System.Diagnostics;
using System.Linq;

namespace freebyTech.Common.ExtensionMethods
{
  public static class UriExtensions
  {
    /// <summary>
    /// Handy extension method for dealing with proper Uri combining.
    /// </summary>
    /// <returns>A new Uri object wiht the full path defined with proper formatting.</returns>
    [DebuggerStepThrough]
    public static Uri Append(this Uri uri, params string[] paths)
    {
        return new Uri(paths.Aggregate(uri.AbsoluteUri, (current, path) => string.Format("{0}/{1}", current.TrimEnd('/'), Uri.EscapeUriString(path.TrimStart('/').TrimEnd('/')))));
    }
  }
}
