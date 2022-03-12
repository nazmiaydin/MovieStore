using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStore.WebApi.Entities
{
    public class Actor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<MovieActors> MovieActors { get; set; }
    }
}