using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.CustomerOperations.Queries.GetCustomers
{
    public class GetCustomersQuery
    {
        private readonly IMovieStoreDbContext dbContext;
        private readonly IMapper mapper;

        public GetCustomersQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public List<CustomersViewModel> Handle(){
            var customers = dbContext.Customers.OrderBy(x=>x.Id).ToList();
            return mapper.Map<List<CustomersViewModel>>(customers);
        }
    }

    public class CustomersViewModel
    {
         public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int PurchasedMoviesId { get; set; }
        public int FavoriteGenreId { get; set; }
    }
}