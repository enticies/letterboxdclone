using Microsoft.EntityFrameworkCore;
using lbdbackend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

// dotnet ef --startup-project ..\lbdbackend.Api migrations add InitialMigration
// dotnet ef --startup-project ..\lbdbackend.Api database update
// dotnet ef --startup-project ..\lbdbackend.Api migrations remove 
namespace lbdbackend.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Year> Years { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Profession> Professions { get; set; }

        public DbSet<Person> People { get; set; }
        public DbSet<JoinMoviesPeople> JoinMoviesPeople { get; set; }
        public DbSet<JoinMoviesGenres> JoinMoviesGenres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<MovieList> MovieLists { get; set; }
        public DbSet<JoinMoviesLists> JoinMoviesLists { get; set; }
        //public DbSet<UserFollower> Followers { get; set; }
        //public DbSet<UserFollowing> Followings { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<UserFollower>()
            //    .HasOne(p => p.User)
            //    .WithMany(t => t.Followers)
            //    .HasForeignKey(m => m.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Relationship>()
                .HasOne(p => p.Follower)
                .WithMany(t => t.Followings)
                .HasForeignKey(m => m.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Relationship>()
                .HasOne(p => p.Followee)
                .WithMany(p => p.Followers)
                .HasForeignKey(p => p.FolloweeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

    }
}
