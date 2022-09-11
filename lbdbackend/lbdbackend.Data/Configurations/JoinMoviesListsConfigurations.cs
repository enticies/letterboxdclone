using lbdbackend.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Data.Configurations {
    public class JoinMoviesListsConfigurations : IEntityTypeConfiguration<JoinMoviesLists> {
        public void Configure(EntityTypeBuilder<JoinMoviesLists> builder) {
            builder.Property(b => b.MovieListId).IsRequired();
            builder.Property(b => b.MovieId).IsRequired();
        }
    }
}
