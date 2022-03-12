
using System;
using System.Linq;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.MovieOperations.Commands.CreateMovie
{
    public class CreateMovieCommand
    {
        private readonly IMovieStoreDbContext dbContext;
        public CreateMovieModel Model { get; set; }
        public CreateMovieCommand(IMovieStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Handle()
        {
            var movie = dbContext.Movies.SingleOrDefault(x => x.Name == Model.Name && x.GenreId == Model.GenreId && x.DirectorId == Model.DirectorId && x.Price == Model.Price && x.Year == Model.Year);

            if (movie is not null)
            {
                throw new InvalidOperationException("Film sistemde kayıtlı!");
            }

            movie = new Entities.Movie();
            movie.Name = Model.Name;
            movie.GenreId = Model.GenreId;
            movie.DirectorId = Model.DirectorId;
            movie.Price = Model.Price;
            movie.Year = Model.Year;
            dbContext.Movies.Add(movie);
            dbContext.SaveChanges();
        }


    }
    public class CreateMovieModel
    {
        public string Name { get; set; }
        public DateTime Year { get; set; }
        public decimal Price { get; set; }
        public int GenreId { get; set; }
        public int DirectorId { get; set; }

    }
}