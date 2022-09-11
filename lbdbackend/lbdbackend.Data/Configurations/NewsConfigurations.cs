using lbdbackend.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Data.Configurations {
    public class NewsConfigurations : IEntityTypeConfiguration<News> {
        public void Configure(EntityTypeBuilder<News> builder) {
            builder.Property(b => b.Body).HasMaxLength(3000).IsRequired();
            builder.Property(b => b.Title).HasMaxLength(100).IsRequired();
        }
    }
}
