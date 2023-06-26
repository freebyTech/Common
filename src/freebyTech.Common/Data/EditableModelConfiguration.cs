using freebyTech.Common.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace freebyTech.Common.Data
{
  /// <summary>
  /// Applies general configruation to model classes that derive from IEditableModel.
  /// </summary>
  /// <typeparam name="TEntity"></typeparam>
  public class EditableModelConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IEditableModel
  {
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
      builder.Ignore(p => p.IsNew);
      builder.Ignore(p => p.IsDirty);
      builder.Ignore(p => p.IsDeleted);

      builder.Property(o => o.Ts).IsRowVersion();
      builder.Property(o => o.ModifiedBy).HasMaxLength(100).IsRequired();
      builder.Property(o => o.ModifiedOn).IsRequired();
      builder.Property(o => o.CreatedBy).HasMaxLength(100).IsRequired().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
      builder.Property(o => o.CreatedOn).IsRequired().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
    }
  }
}
