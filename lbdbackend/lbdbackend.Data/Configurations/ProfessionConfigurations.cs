using lbdbackend.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Data.Configurations {
    public class ProfessionConfigurations : IEntityTypeConfiguration<Profession> {
        public void Configure(EntityTypeBuilder<Profession> builder) {
            builder.Property(p => p.Name).HasMaxLength(25).IsRequired(true);
        }
    }
}