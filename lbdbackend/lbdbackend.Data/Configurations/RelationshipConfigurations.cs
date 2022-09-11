using lbdbackend.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Data.Configurations {
    public class RelationshipConfigurations : IEntityTypeConfiguration<Relationship> {
        public void Configure(EntityTypeBuilder<Relationship> builder) {
            builder.Property(b => b.FolloweeId).IsRequired();
            builder.Property(b => b.FolloweeId).IsRequired(true);
        }
    }
}
