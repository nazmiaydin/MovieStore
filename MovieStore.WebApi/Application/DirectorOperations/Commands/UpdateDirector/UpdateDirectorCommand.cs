using System;
using System.Linq;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommand
    {
        private readonly IMovieStoreDbContext dbContext;
        public UpdateDirectorModel Model { get; set; }
        public int DirectorId { get; set; }
        public UpdateDirectorCommand(IMovieStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Handle()
        {
            var director = dbContext.Directors.SingleOrDefault(x => x.Id == DirectorId);
            if (director is null)
                throw new InvalidOperationException("Yönetmen bulunamadı!");

            if (dbContext.Directors.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Surname.ToLower() == Model.Surname.ToLower() && x.Id != DirectorId))
                throw new InvalidOperationException("Aynı isimli yönetmen zaten mevcut!");

            director.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? director.Name : Model.Name;
            director.Surname = string.IsNullOrEmpty(Model.Surname.Trim()) ? director.Surname : Model.Surname;

            dbContext.SaveChanges();
        }
    }

    public class UpdateDirectorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}