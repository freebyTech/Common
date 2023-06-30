using AutoMapper;
using freebyTech.Common.Data.Interfaces;

namespace freebyTech.Common.ExtensionMethods;

public static class AutoMapperExtensions
{
  /// <summary>
  /// Will create a standard resource to entity mapping.
  /// </summary>
  /// <returns></returns>
  public static void CreateResourceToEntityMap<TEntity, TResource>(this Profile profile, TEntity entity, TResource resource)
    where TEntity : IEditableModel, new()
    where TResource : IEditableModel, new()
  {
    profile.CreateMap<TEntity, TResource>();
    profile.CreateMap<TResource, TEntity>();
  }

  /// <summary>
  /// Will create a standard resource to entity mapping.
  /// </summary>
  /// <returns></returns>
  public static void CreateSimpleResourceViewToEntityMap<TEntity, TResource>(this Profile profile, TEntity entity, TResource resource)
    where TEntity : IEditableModel, new()
    where TResource : IEditableResource, new()
  {
    profile
      .CreateMap<TEntity, TEntity>()
      .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
      .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
      .ForMember(dest => dest.ModifiedOn, opt => opt.Ignore())
      .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());
    profile
      .CreateMap<TEntity, TEntity>()
      .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
      .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
      .ForMember(dest => dest.ModifiedOn, opt => opt.Ignore())
      .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());
  }
}
