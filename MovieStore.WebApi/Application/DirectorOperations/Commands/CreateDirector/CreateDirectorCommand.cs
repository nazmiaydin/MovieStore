
using System;
using System.Linq;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.DirectorOperations.Commands.CreateDirector
{
    public class CreateDirectorCommand
    {
        private readonly IMovieStoreDbContext dbContext;
        public CreateDirectorModel Model { get; set; }
        public CreateDirectorCommand(IMovieStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Handle()
        {
            var director = dbContext.Directors.SingleOrDefault(x => x.Name == Model.Name && x.Surname == Model.Surname);

            if (director is not null)
            {
                throw new InvalidOperationException("Yönetmen sistemde kayıtlı!");
            }

            director = new Entities.Director();
            director.Name = Model.Name;
            director.Surname = Model.Surname;
            dbContext.Directors.Add(director);
            dbContext.SaveChanges();
        }


    }
    public class CreateDirectorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}