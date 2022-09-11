using lbdbackend.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Data.Configurations {
    public class MovieConfigurations : IEntityTypeConfiguration<Movie> {
        public void Configure(EntityTypeBuilder<Movie> builder) {
            builder.Property(b => b.Name).HasMaxLength(40).IsRequired(true);
            builder.Property(b => b.Synopsis).HasMaxLength(300).IsRequired(true);
            builder.Property(b => b.BackgroundImage).HasMaxLength(300).IsRequired(true);
            builder.Property(b => b.PosterImage).HasMaxLength(300).IsRequired(true);
        }
    }
}
