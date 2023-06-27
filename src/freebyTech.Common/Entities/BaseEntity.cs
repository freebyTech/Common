namespace freebyTech.Common.Entities;

/// <summary>
/// The Base Entity
/// </summary>
public class BaseEntity<IDType> : BaseEntityNonActive<IDType>
{
  /// <summary>
  /// Gets or sets a value indicating whether this instance is active.
  /// </summary>
  /// <value>
  ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
  /// </value>
  public bool IsActive { get; set; }
}
