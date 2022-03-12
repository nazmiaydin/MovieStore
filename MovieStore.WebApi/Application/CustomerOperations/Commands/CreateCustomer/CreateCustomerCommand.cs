using System;
using System.Linq;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.CustomerOperations.Commands.CreateCustomer
{
    public class CreateCustomerCommand
    {
        private readonly IMovieStoreDbContext dbContext;

        public CreateCustomerModel Model { get; set; }

        public CreateCustomerCommand(IMovieStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Handle()
        {
            var customer = dbContext.Customers.SingleOrDefault(x => x.Name == Model.Name && x.Surname == Model.Surname);
            if (customer is not null)
            {
                throw new InvalidOperationException("Müşteri sistemde kayıtlı!");
            }

            customer = new Entities.Customer();
            customer.Name = Model.Name;
            customer.Surname = Model.Surname;
            dbContext.Customers.Add(customer);
            dbContext.SaveChanges();
        }
    }

    public class CreateCustomerModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}