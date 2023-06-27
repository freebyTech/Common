namespace freebyTech.Common.Resources;

/// <summary>
/// The Base Resruoce
/// </summary>
public class BaseResource<IDType> : BaseResourceNonActive<IDType>
{
  /// <summary>
  /// Gets or sets a value indicating whether this instance is active.
  /// </summary>
  /// <value>
  ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
  /// </value>
  public bool IsActive { get; set; }
}
