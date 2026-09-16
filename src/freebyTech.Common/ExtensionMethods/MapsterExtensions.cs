using freebyTech.Common.Data.Interfaces;
using Mapster;

namespace freebyTech.Common.ExtensionMethods;

/// <summary>
/// Mapster equivalents of the resource/entity mapping helpers. These register
/// type adapters on a <see cref="TypeAdapterConfig"/> (the Mapster analogue of an
/// AutoMapper Profile). Method names/semantics are preserved from the previous
/// AutoMapper-based helpers so consuming profiles port mechanically:
///   - CreateMap&lt;A,B&gt;()                 -> config.NewConfig&lt;A,B&gt;()
///   - .ForMember(d, o =&gt; o.Ignore())    -> .Ignore(d =&gt; d.Member)
///   - .ForMember(d, o =&gt; o.MapFrom(..)) -> .Map(d =&gt; d.Member, s =&gt; ..)
/// The "Lut" (last-updated ticks) value that was produced by the old
/// CompactResourceViewResolver is now an inline mapping expression.
/// </summary>
public static class MapsterExtensions
{
  /// <summary>
  /// Creates a standard resource-to-entity mapping and vice versa.
  /// </summary>
  public static void CreateEAndRMaps<TEntity, TResource>(this TypeAdapterConfig config)
    where TEntity : IEditableModel, new()
    where TResource : IEditableResource, new()
  {
    config.NewConfig<TResource, TEntity>();
    config.NewConfig<TEntity, TResource>();
  }

  /// <summary>
  /// Creates a compact-resource-view-to-entity mapping and vice versa. The
  /// resource-&gt;entity direction ignores the audit fields; the entity-&gt;resource
  /// direction computes the compact Lut value.
  /// </summary>
  public static void CreateEAndCrvMaps<TEntity, TResource>(this TypeAdapterConfig config)
    where TEntity : IEditableModel, new()
    where TResource : ICompactEditableResource, new()
  {
    config
      .NewConfig<TResource, TEntity>()
      // String member names (not dest => dest.X): Mapster can't parse a member-access lambda
      // through a generic interface-typed parameter (throws "Allow only member access").
      .Ignore("CreatedOn", "CreatedBy", "ModifiedOn", "ModifiedBy");
    config
      .NewConfig<TEntity, TResource>()
      // Use the string destination-member overload rather than dest => dest.Lut: Mapster cannot
      // parse a member-access lambda through a generic interface-typed parameter (it throws
      // "Allow only member access"). The string form registers Lut as a mapped member so it
      // satisfies RequireDestinationMemberSource.
      .Map("Lut", src => src.ModifiedOn != null ? src.ModifiedOn.Value.Ticks : src.CreatedOn.Ticks);
  }

  /// <summary>
  /// Creates a standard resource-to-entity mapping, returning the setter for further configuration.
  /// </summary>
  public static TypeAdapterSetter<TResource, TEntity> CreateRToEMap<TResource, TEntity>(this TypeAdapterConfig config)
    where TEntity : new()
    where TResource : new()
  {
    return config.NewConfig<TResource, TEntity>();
  }

  /// <summary>
  /// Creates a standard entity-to-resource mapping, returning the setter for further configuration.
  /// </summary>
  public static TypeAdapterSetter<TEntity, TResource> CreateEToRMap<TEntity, TResource>(this TypeAdapterConfig config)
    where TEntity : new()
    where TResource : new()
  {
    return config.NewConfig<TEntity, TResource>();
  }

  /// <summary>
  /// Creates an entity-to-compact-resource-view mapping (computing Lut), returning the setter for further configuration.
  /// </summary>
  public static TypeAdapterSetter<TEntity, TResource> CreateEToCrvMap<TEntity, TResource>(this TypeAdapterConfig config)
    where TEntity : IEditableModel, new()
    where TResource : ICompactEditableResource, new()
  {
    return config
      .NewConfig<TEntity, TResource>()
      // Use the string destination-member overload rather than dest => dest.Lut: Mapster cannot
      // parse a member-access lambda through a generic interface-typed parameter (it throws
      // "Allow only member access"). The string form registers Lut as a mapped member so it
      // satisfies RequireDestinationMemberSource.
      .Map("Lut", src => src.ModifiedOn != null ? src.ModifiedOn.Value.Ticks : src.CreatedOn.Ticks);
  }

  /// <summary>
  /// Creates a simple-or-compact-resource-view-to-entity mapping (ignoring audit fields),
  /// returning the setter for further configuration.
  /// </summary>
  public static TypeAdapterSetter<TResource, TEntity> CreateSorCrvToEMap<TResource, TEntity>(this TypeAdapterConfig config)
    where TEntity : IEditableModel, new()
    where TResource : IEditableResource, new()
  {
    return config
      .NewConfig<TResource, TEntity>()
      // String member names (not dest => dest.X): Mapster can't parse a member-access lambda
      // through a generic interface-typed parameter (throws "Allow only member access").
      .Ignore("CreatedOn", "CreatedBy", "ModifiedOn", "ModifiedBy");
  }
}
