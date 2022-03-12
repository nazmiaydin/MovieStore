using System;
using System.Linq;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.ActorOperations.Commands.UpdateActor
{
    public class UpdateActorCommand
    {
        private readonly IMovieStoreDbContext dbContext;
        public UpdateActorModel Model { get; set; }
        public int ActorId { get; set; }
        public UpdateActorCommand(IMovieStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Handle()
        {
            var actor = dbContext.Actors.SingleOrDefault(x => x.Id == ActorId);
            if (actor is null)
                throw new InvalidOperationException("Oyuncu bulunamadı!");

            if (dbContext.Actors.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Surname.ToLower() == Model.Surname.ToLower() && x.Id != ActorId))
                throw new InvalidOperationException("Aynı isimli oyuncu zaten mevcut!");

            actor.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? actor.Name : Model.Name;
            actor.Surname = string.IsNullOrEmpty(Model.Surname.Trim()) ? actor.Surname : Model.Surname;

            dbContext.SaveChanges();
        }
    }

    public class UpdateActorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}