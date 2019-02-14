using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using freebyTech.Common.CommandLine;
using freebyTech.Common.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace freebyTech.Common.ExtensionMethods
{
    public static class ModelExtensions
    {
        /// <summary>
        /// Applies the proper created and modified settings based upon the state of the model and returns true if it updated any fields.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        public static bool UpdateIfEdited(this IEditableModel model, string userName)
        {
            if(model.IsNew) {
                model.CreatedBy = userName;
                model.CreatedOn = System.DateTime.Now;
                return true;
            }
            else if(model.IsDirty) {
                model.ModifiedBy = userName;
                model.ModifiedOn = System.DateTime.Now;
                return true;
            }
            return false;
        }

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

                builder.Property(p => p.CreatedOn)
                    .Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
                builder.Property(p => p.CreatedBy)
                    .Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            }
        }

        /// <summary>
        /// Applies the IEditableModel Entity Type Configuration to all models which fit that interface within the ModelBuilder.
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void ApplyEditableModelConfigurations(this ModelBuilder modelBuilder)
        {
            var entityTypes = modelBuilder.Model.GetEntityTypes()
                .Where(t => typeof(IEditableModel).IsAssignableFrom(t.ClrType));
            foreach (var entityType in entityTypes)
            {
                var configurationType = typeof(EditableModelConfiguration<>)
                    .MakeGenericType(entityType.ClrType);
                modelBuilder.ApplyConfiguration(
                    (dynamic)Activator.CreateInstance(configurationType));
            }
        }
    }
}