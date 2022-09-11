using lbdbackend.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Data.Configurations {
    public class MovieListConfigurations : IEntityTypeConfiguration<MovieList> {
        public void Configure(EntityTypeBuilder<MovieList> builder) {
            builder.Property(b => b.Name).IsRequired();
            builder.Property(b => b.OwnerId).IsRequired();
        }
    }
}
