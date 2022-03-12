using Microsoft.EntityFrameworkCore;

namespace MovieStore.WebApi.Entities
{
    [Keyless]
    public class MovieActors
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int ActorId { get; set; }
        public Actor Actor { get; set; }
    }
}
