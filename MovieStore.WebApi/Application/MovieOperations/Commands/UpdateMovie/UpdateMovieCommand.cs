using System;
using System.Linq;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.MovieOperations.Commands.UpdateMovie
{
    public class UpdateMovieCommand
    {
        private readonly IMovieStoreDbContext dbContext;
        public UpdateMovieModel Model { get; set; }
        public int MovieId { get; set; }
        public UpdateMovieCommand(IMovieStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Handle()
        {
            var movie = dbContext.Movies.SingleOrDefault(x => x.Id == MovieId);
            if (movie is null)
                throw new InvalidOperationException("Film bulunamadı!");

            if (dbContext.Movies.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != MovieId))
                throw new InvalidOperationException("Aynı isimli film zaten mevcut!");

            movie.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? movie.Name : Model.Name;
            movie.GenreId = Model.GenreId == default ? movie.GenreId : Model.GenreId;
            movie.DirectorId = Model.DirectorId == default ? movie.DirectorId : Model.DirectorId;
            movie.Price = Model.Price == default ? movie.Price : Model.Price;

            dbContext.SaveChanges();
        }
    }

    public class UpdateMovieModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int GenreId { get; set; }
        public int DirectorId { get; set; }
    }
}