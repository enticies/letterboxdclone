using lbdbackend.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Data.Configurations {
    public class GenreConfigurations : IEntityTypeConfiguration<Genre> {
        public void Configure(EntityTypeBuilder<Genre> builder) {
            builder.Property(b => b.Name).HasMaxLength(25).IsRequired(true);
        }
    }
}
