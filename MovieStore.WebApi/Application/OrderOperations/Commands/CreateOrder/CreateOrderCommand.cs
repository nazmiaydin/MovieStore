
using System;
using System.Linq;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.OrderOperations.Commands.CreateOrder
{
    public class CreateOrderCommand
    {
        private readonly IMovieStoreDbContext dbContext;
        public CreateOrderModel Model { get; set; }
        public CreateOrderCommand(IMovieStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Handle()
        {
            var movie = dbContext.Movies.SingleOrDefault(x => x.Id == Model.MovieId && x.IsActive);

            if (movie is null)
                throw new InvalidOperationException("Satın alınmak istenen film mevcut değil!");

            var order = dbContext.Orders.SingleOrDefault(x => x.CustomerId == Model.CustomerId && x.MovieId == Model.MovieId);

            if (order is not null)
            {
                throw new InvalidOperationException("Siparişiniz zaten mevcut!");
            }

            order = new Entities.Order();
            order.CustomerId = Model.CustomerId;
            order.MovieId = Model.MovieId;
            dbContext.Orders.Add(order);
            dbContext.SaveChanges();
        }

    }
    public class CreateOrderModel
    {
        public int CustomerId { get; set; }
        public int MovieId { get; set; }
    }
}