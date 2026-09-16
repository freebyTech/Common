using System;
using Xunit;
using Mapster;
using freebyTech.Common.Data.Interfaces;
using freebyTech.Common.ExtensionMethods;
using freebyTech.Common.Resources;

namespace freebyTech.Common.Tests.ExtensionMethods;

/// <summary>
/// Exercises the Mapster mapping helpers end-to-end (build config + Compile + adapt).
/// The helpers were previously never covered by a test, which let a generic
/// member-expression bug in the Lut mapping ship. These guard against recurrence.
/// </summary>
public class MapsterExtensionsTests
{
  private class SampleEntity : IEditableModel
  {
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime? ModifiedOn { get; set; }
    public string? ModifiedBy { get; set; }
    public byte[]? Ts { get; set; }
    public bool IsNew { get; set; }
    public bool IsDirty { get; set; }
    public bool IsDeleted { get; set; }
  }

  private class SampleCompactResource : BaseCompactResourceViewNonActive<Guid>
  {
    public string Name { get; set; } = string.Empty;
  }

  [Fact]
  public void CreateEToCrvMap_Compiles_AndComputesLutFromModifiedOn()
  {
    var config = new TypeAdapterConfig { RequireDestinationMemberSource = true };
    config.CreateEToCrvMap<SampleEntity, SampleCompactResource>();
    config.Compile();

    var modified = new DateTime(2026, 9, 16, 12, 0, 0, DateTimeKind.Utc);
    var entity = new SampleEntity { Id = Guid.NewGuid(), Name = "abc", CreatedOn = modified.AddDays(-2), ModifiedOn = modified };

    var resource = entity.Adapt<SampleCompactResource>(config);

    Assert.Equal("abc", resource.Name);
    Assert.Equal(modified.Ticks, resource.Lut);
  }

  [Fact]
  public void CreateEToCrvMap_UsesCreatedOn_WhenModifiedOnNull()
  {
    var config = new TypeAdapterConfig();
    config.CreateEToCrvMap<SampleEntity, SampleCompactResource>();
    config.Compile();

    var created = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    var entity = new SampleEntity { Id = Guid.NewGuid(), CreatedOn = created, ModifiedOn = null };

    var resource = entity.Adapt<SampleCompactResource>(config);

    Assert.Equal(created.Ticks, resource.Lut);
  }

  [Fact]
  public void CreateEAndCrvMaps_Compiles_BothDirections()
  {
    var config = new TypeAdapterConfig { RequireDestinationMemberSource = true };
    config.CreateEAndCrvMaps<SampleEntity, SampleCompactResource>();
    config.Compile();
  }
}
