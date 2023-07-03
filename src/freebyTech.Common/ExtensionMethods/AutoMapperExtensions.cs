using AutoMapper;
using freebyTech.Common.Data.Interfaces;

namespace freebyTech.Common.ExtensionMethods;

public class CompactResourceViewResolver<TEntity, TResource> : IValueResolver<TEntity, TResource, long>
  where TEntity : IEditableModel
  where TResource : ICompactEditableResource
{
  public long Resolve(TEntity source, TResource destination, long member, ResolutionContext context)
  {
    if (source.ModifiedOn != null)
    {
      return source.ModifiedOn.Value.Ticks;
    }
    else
      return source.CreatedOn.Ticks;
  }
}

public static class AutoMapperExtensions
{
  /// <summary>
  /// Will create a standard resource to entity mapping and vice versa.
  /// </summary>
  /// <returns></returns>
  public static void CreateEAndRMaps<TEntity, TResource>(this Profile profile)
    where TEntity : IEditableModel, new()
    where TResource : IEditableResource, new()
  {
    profile.CreateMap<TResource, TEntity>();
    profile.CreateMap<TEntity, TResource>();
  }

  /// <summary>
  /// Will create a compact resource view to entity mapping and vice versa.
  /// </summary>
  /// <returns></returns>
  public static void CreateEAndCrvMaps<TEntity, TResource>(this Profile profile)
    where TEntity : IEditableModel, new()
    where TResource : ICompactEditableResource, new()
  {
    profile
      .CreateMap<TResource, TEntity>()
      .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
      .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
      .ForMember(dest => dest.ModifiedOn, opt => opt.Ignore())
      .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());
    profile.CreateMap<TEntity, TResource>().ForMember(dest => dest.Lut, opt => opt.MapFrom(new CompactResourceViewResolver<TEntity, TResource>()));
  }

  /// <summary>
  /// Will create a standard resource to entity mapping.
  /// </summary>
  /// <returns></returns>
  public static IMappingExpression<TResource, TEntity> CreateRToEMap<TResource, TEntity>(this Profile profile)
    where TEntity : new()
    where TResource : new()
  {
    return profile.CreateMap<TResource, TEntity>();
  }

  /// <summary>
  /// Will create a standard resource to entity mapping.
  /// </summary>
  /// <returns></returns>
  public static IMappingExpression<TEntity, TResource> CreateEToRMap<TEntity, TResource>(this Profile profile)
    where TEntity : new()
    where TResource : new()
  {
    return profile.CreateMap<TEntity, TResource>();
  }

  /// <summary>
  /// Will create a standard resource to entity mapping.
  /// </summary>
  /// <returns></returns>
  public static IMappingExpression<TEntity, TResource> CreateEToCrvMap<TEntity, TResource>(this Profile profile)
    where TEntity : IEditableModel, new()
    where TResource : ICompactEditableResource, new()
  {
    return profile.CreateMap<TEntity, TResource>().ForMember(dest => dest.Lut, opt => opt.MapFrom(new CompactResourceViewResolver<TEntity, TResource>()));
  }

  /// <summary>
  /// Will create a simple or compact resource view to entity mapping.
  /// </summary>
  /// <returns></returns>
  public static IMappingExpression<TResource, TEntity> CreateSorCrvToEMap<TResource, TEntity>(this Profile profile)
    where TEntity : IEditableModel, new()
    where TResource : IEditableResource, new()
  {
    return profile
      .CreateMap<TResource, TEntity>()
      .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
      .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
      .ForMember(dest => dest.ModifiedOn, opt => opt.Ignore())
      .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());
  }
}
