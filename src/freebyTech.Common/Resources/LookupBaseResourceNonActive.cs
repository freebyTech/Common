using System;

namespace freebyTech.Common.Resources;

/// <summary>
/// The Base Lookup Resource which doesn't contain DB write time and user information.
/// Simplifying the return of data for less size.
/// </summary>
public class LookupBaseResourceNonActive
{
  /// <summary>
  /// Gets or sets the identifier.
  /// </summary>
  /// <value>
  /// The identifier.
  /// </value>
  public int Id { get; set; }
}
