using System;
using System.Linq;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.DirectorOperations.Commands.DeleteDirector
{
    public class DeleteDirectorCommand
    {
        private readonly IMovieStoreDbContext dbContext;
        public int DirectorId { get; set; }
        public DeleteDirectorCommand(IMovieStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Handle()
        {
            var director = dbContext.Directors.SingleOrDefault(x => x.Id == DirectorId);

            if (director is null)
            {
                throw new InvalidOperationException("Yönetmen bulunamadı!");
            }

            dbContext.Directors.Remove(director);
            dbContext.SaveChanges();
        }
    }
}
