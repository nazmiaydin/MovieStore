using System;
using System.Linq;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.ActorOperations.Commands.DeleteActor
{
    public class DeleteActorCommand
    {
        private readonly IMovieStoreDbContext dbContext;
        public int ActorId { get; set; }
        public DeleteActorCommand(IMovieStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Handle()
        {
            var actor = dbContext.Actors.SingleOrDefault(x => x.Id == ActorId);

            if (actor is null)
            {
                throw new InvalidOperationException("Oyuncu bulunamadı!");
            }

            dbContext.Actors.Remove(actor);
            dbContext.SaveChanges();
        }
    }
}
