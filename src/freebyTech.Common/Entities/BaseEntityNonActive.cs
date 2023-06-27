using System;
using freebyTech.Common.Data.Interfaces;

namespace freebyTech.Common.Entities;

/// <summary>
/// The Base Entity
/// </summary>
public class BaseEntityNonActive<IDType> : IEditableModel
{
  /// <summary>
  /// Gets or sets the identifier.
  /// </summary>
  /// <value>
  /// The identifier.
  /// </value>
  public IDType Id { get; set; }

  /// <summary>
  /// Gets or sets the created by.
  /// </summary>
  /// <value>
  /// The created by.
  /// </value>
  public string CreatedBy { get; set; }

  /// <summary>
  /// Gets or sets the modified by.
  /// </summary>
  /// <value>
  /// The modified by.
  /// </value>
  public string? ModifiedBy { get; set; }

  /// <summary>
  /// Gets or sets the created on.
  /// </summary>
  /// <value>
  /// The created on.
  /// </value>
  public DateTime CreatedOn { get; set; }

  /// <summary>
  /// Gets or sets the modified on.
  /// </summary>
  /// <value>
  /// The modified on.
  /// </value>
  public DateTime? ModifiedOn { get; set; }

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
