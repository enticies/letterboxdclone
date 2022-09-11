using lbdbackend.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Data.Configurations {
    public class PersonConfigurations : IEntityTypeConfiguration<Person> {
        public void Configure(EntityTypeBuilder<Person> builder) {
            builder.Property(b => b.Name).HasMaxLength(25).IsRequired(true);
            builder.Property(b => b.Description).HasMaxLength(300).IsRequired(true);
            builder.Property(b => b.Description).IsRequired(true);
        }
    }
}