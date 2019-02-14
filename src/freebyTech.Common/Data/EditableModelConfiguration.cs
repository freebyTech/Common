using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using freebyTech.Common.CommandLine;
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

            builder.Property(p => p.CreatedOn)
                .Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            builder.Property(p => p.CreatedBy)
                .Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
        }
    }
}