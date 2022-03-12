
using System;
using System.Linq;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.ActorOperations.Commands.CreateActor
{
    public class CreateActorCommand
    {
        private readonly IMovieStoreDbContext dbContext;
        public CreateActorModel Model { get; set; }
        public CreateActorCommand(IMovieStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Handle(){
            var actor = dbContext.Actors.SingleOrDefault(x=>x.Name == Model.Name && x.Surname == Model.Surname);

            if(actor is not null)
            {
                throw new InvalidOperationException("Oyuncu sistemde kayıtlı!");
            }

            actor = new Entities.Actor();
            actor.Name = Model.Name;
            actor.Surname = Model.Surname;
            dbContext.Actors.Add(actor);
            dbContext.SaveChanges();
        }


    }
    public class CreateActorModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
        }
}