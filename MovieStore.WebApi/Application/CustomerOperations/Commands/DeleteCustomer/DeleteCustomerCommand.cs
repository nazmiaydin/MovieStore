

using System;
using System.Linq;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.CustomerOperations.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand
    {
        private readonly IMovieStoreDbContext dbContext;

        public int CustomerId { get; set; }

        public DeleteCustomerCommand(IMovieStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Handle()
        {
            var customer = dbContext.Customers.SingleOrDefault(x => x.Id == CustomerId);
            if (customer is null)
            {
                throw new InvalidOperationException("Müşteri bulunamadı!");
            }

            dbContext.Customers.Remove(customer);
            dbContext.SaveChanges();
        }
    }
}