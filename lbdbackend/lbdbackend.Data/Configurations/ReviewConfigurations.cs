using lbdbackend.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Data.Configurations {
    public class ReviewConfigurations : IEntityTypeConfiguration<Review> {
        public void Configure(EntityTypeBuilder<Review> builder) {
            builder.Property(b => b.Body).HasMaxLength(300);
            //builder.Property(b => b.AppUserId).IsRequired(true);
            builder.Property(b => b.MovieId).IsRequired(true);
        }
    }
}
