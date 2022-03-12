using Microsoft.EntityFrameworkCore;
using MovieStore.WebApi.Entities;

namespace MovieStore.WebApi.DbOperations
{
    public interface IMovieStoreDbContext
    {
        DbSet<Actor> Actors { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Director> Directors { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Movie> Movies { get; set; }
        DbSet<Order> Orders { get; set; }
        int SaveChanges();

    }
}