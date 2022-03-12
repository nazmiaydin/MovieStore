using System;
using System.Linq;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.MovieOperations.Commands.DeleteMovie
{
    public class DeleteMovieCommand
    {
        private readonly IMovieStoreDbContext dbContext;
        public int MovieId { get; set; }
        public DeleteMovieCommand(IMovieStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Handle()
        {
            var movie = dbContext.Movies.SingleOrDefault(x => x.Id == MovieId);

            if (movie is null)
            {
                throw new InvalidOperationException("Film bulunamadı!");
            }

            movie.IsActive = default;
            dbContext.Movies.Update(movie);
            dbContext.SaveChanges();
        }
    }
}
