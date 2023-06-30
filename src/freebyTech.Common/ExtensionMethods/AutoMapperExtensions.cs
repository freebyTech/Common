using AutoMapper;
using freebyTech.Common.Data.Interfaces;

namespace freebyTech.Common.ExtensionMethods;

public static class AutoMapperExtensions
{
  /// <summary>
  /// Will create a standard resource to entity mapping and vice versa.
  /// </summary>
  /// <returns></returns>
  public static void CreateEAndRMaps<TEntity, TResource>(this Profile profile)
    where TEntity : new()
    where TResource : new()
  {
    profile.CreateMap<TResource, TEntity>();
    profile.CreateMap<TEntity, TResource>();
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
  /// Will create a simple resource view to entity mapping.
  /// </summary>
  /// <returns></returns>
  public static IMappingExpression<TResource, TEntity> CreateSrvToEMap<TResource, TEntity>(this Profile profile)
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
