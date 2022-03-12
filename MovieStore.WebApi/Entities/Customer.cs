using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStore.WebApi.Entities
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int PurchasedMoviesId { get; set; }
        public int FavoriteGenreId { get; set; }
    }
}