using System;
using freebyTech.Common.Data.Interfaces;

namespace freebyTech.Common.Resources;

/// <summary>
/// The Base Simple Resource View which doesn't contain DB write time and user information.
/// Simplifying the return of data for less size.
/// </summary>
public class BaseSimpleResourceViewNonActive<IDType> : IEditableResource
{
  /// <summary>
  /// Gets or sets the identifier.
  /// </summary>
  /// <value>
  /// The identifier.
  /// </value>
  public IDType Id { get; set; }

  /// <summary>
  /// The timestamp of the last record write.
  /// </summary>
  public byte[]? Ts { get; set; }

  #region State Properties Not Saved to the Database
  public bool IsNew { get; set; }
  public bool IsDirty { get; set; }
  public bool IsDeleted { get; set; }

  #endregion
}
