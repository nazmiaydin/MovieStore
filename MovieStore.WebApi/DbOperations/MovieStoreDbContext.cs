using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieStore.WebApi.Entities;

namespace MovieStore.WebApi.DbOperations
{
    public class MovieStoreDbContext : DbContext, IMovieStoreDbContext
    {
        public MovieStoreDbContext(DbContextOptions<MovieStoreDbContext> options) : base(options)
        {
        }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<MovieActors> MovieActors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MovieActors>(ConfigureMovieActors);

            base.OnModelCreating(builder);
        }

        private void ConfigureMovieActors(EntityTypeBuilder<MovieActors> obj)
        {
            obj.HasKey(x=> new{x.ActorId,x.MovieId});
            obj.HasOne(x=>x.Movie).WithMany(x=>x.MovieActors).HasForeignKey(x=>x.MovieId);
            obj.HasOne(x=>x.Actor).WithMany(x=>x.MovieActors).HasForeignKey(x=>x.ActorId);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}