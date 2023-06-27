using System;

namespace freebyTech.Common.Resources;

/// <summary>
/// The Base Resruoce
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
  public DateTime ModifiedOn { get; set; }
}
