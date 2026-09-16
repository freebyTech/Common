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
        return new Uri(paths.Aggregate(uri.AbsoluteUri, (current, path) => string.Format("{0}/{1}", current.TrimEnd('/'), EscapePathSegments(path.TrimStart('/').TrimEnd('/')))));
    }

    /// <summary>
    /// Escapes each path segment via <see cref="Uri.EscapeDataString"/> while preserving the
    /// '/' separators between segments. Replaces the obsolete <c>Uri.EscapeUriString</c> for path components.
    /// </summary>
    private static string EscapePathSegments(string path)
    {
        return string.Join('/', path.Split('/').Select(Uri.EscapeDataString));
    }
  }
}
