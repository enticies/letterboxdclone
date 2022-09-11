using lbdbackend.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Data.Configurations {
    public class CommentConfigurations : IEntityTypeConfiguration<Comment> {
        public void Configure(EntityTypeBuilder<Comment> builder) {
            builder.Property(b => b.Body).HasMaxLength(300).IsRequired(true);
            builder.Property(b => b.OwnerId).IsRequired();
            builder.Property(b => b.ReviewId).IsRequired(true);
        }
    }
}
