using System;
using freebyTech.Common.Data.Interfaces;

namespace freebyTech.Common.Resources;

/// <summary>
/// The Base Simple Resource View which doesn't contain DB write time and user information.
/// Simplifying the return of data for less size.
/// </summary>
public class BaseCompactResourceView<IDType> : BaseCompactResourceViewNonActive<IDType>
{
  /// <summary>
  /// Gets or sets a value indicating whether this instance is active.
  /// </summary>
  /// <value>
  ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
  /// </value>
  public bool IsActive { get; set; }
}
