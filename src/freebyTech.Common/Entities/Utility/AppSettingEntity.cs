using System;
using freebyTech.Common.Constants;
using freebyTech.Common.Data;
using freebyTech.Common.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace freebyTech.Common.Entities.Utility;

/// <summary>
/// The entity used to store runtime application settings in the database.
/// </summary>
public partial class AppSettingEntity : BaseEntity<int>, IFindableById<int>
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
  /// The description.
  /// </value>
  public string Description { get; set; }

  /// <summary>
  /// Gets or sets the value for this application setting.
  /// </summary>
  /// <value>
  /// The value.
  /// </value>
  public string Value { get; set; }

  /// <summary>
  /// Gets or sets the level for which this value applies.
  /// </summary>
  /// <value>
  /// The level.
  /// </value>
  public int Level { get; set; }

  /// <summary>
  /// Gets or sets the type for this application setting.
  /// </summary>
  /// <value>
  /// The type.
  /// </value>
  public string Type { get; set; }
}

/// <summary>
/// The AppSettingEntity Configuration
/// </summary>
public static class AppSettingEntityConfiguration
{
  /// <summary>
  /// Configures the specified model builder.
  /// </summary>
  /// <param name="modelBuilder">The model builder.</param>
  public static void Configure(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<AppSettingEntity>(entity =>
    {
      var editableModelConfig = new EditableModelConfiguration<AppSettingEntity>();

      editableModelConfig.Configure(entity);

      entity.HasKey(o => o.Id);

      entity.Property(o => o.Name).HasMaxLength(100).IsRequired();
      entity.Property(o => o.Description).HasMaxLength(500).IsRequired();
      entity.Property(o => o.Value).IsRequired();
      entity.Property(o => o.Level).IsRequired();
      entity.Property(o => o.Type).IsRequired();
    });

    modelBuilder
      .Entity<AppSettingEntity>()
      .HasData(
        new AppSettingEntity
        {
          Id = -1,
          Name = "SeedTest",
          Description = "Initial Seed Test",
          Value = "Successful",
          Level = 0,
          Type = AppSettingTypesConstants.StringType,
          CreatedBy = "James",
          CreatedOn = new DateTime(2023, 1, 21),
          ModifiedBy = "James",
          ModifiedOn = new DateTime(2023, 1, 21)
        },
        new AppSettingEntity
        {
          Id = -2,
          Name = "SeedTest",
          Description = "Initial Seed Test",
          Value = "Successful",
          Level = 1,
          Type = AppSettingTypesConstants.StringType,
          CreatedBy = "James",
          CreatedOn = new DateTime(2023, 1, 21),
          ModifiedBy = "James",
          ModifiedOn = new DateTime(2023, 1, 21)
        }
      );
  }
}
