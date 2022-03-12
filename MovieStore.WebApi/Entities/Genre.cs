using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStore.WebApi.Entities
{
    public class Genre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
        public Customer Customer { get; set; }
    }
}