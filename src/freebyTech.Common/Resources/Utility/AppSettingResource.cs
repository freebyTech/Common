using Microsoft.EntityFrameworkCore;

namespace freebyTech.Common.Resources.Utility;

/// <summary>
/// The resource used to store runtime application settings in the database.
/// </summary>
public partial class AppSettingResource : BaseResource<int>
{
  /// <summary>
  /// Gets or sets the name for this application setting.
  /// </summary>
  /// <value>
  /// The name.
  /// </value>
  public string Name { get; set; }

  /// <summary>
  /// Gets or sets the description for this application setting.
  /// </summary>
  /// <value>
  /// The name.
  /// </value>
  public string Description { get; set; }

  /// <summary>
  /// Gets or sets the value for this application setting.
  /// </summary>
  /// <value>
  /// The name.
  /// </value>
  public string Value { get; set; }

  /// <summary>
  /// Gets or sets the level for which this value applies.
  /// </summary>
  /// <value>
  /// The name.
  /// </value>
  public string Level { get; set; }
}
