using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieStore.WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using MovieStore.WebApi.Application.CustomerOperations.Commands.DeleteCustomer;
using MovieStore.WebApi.Application.CustomerOperations.Queries.GetCustomers;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class CustomerController : ControllerBase
    {
        private readonly IMovieStoreDbContext dbContext;
        private readonly IMapper mapper;

        public CustomerController(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        [HttpPost]
        public IActionResult AddCustomer([FromBody] CreateCustomerModel newCustomer)
        {
            var command = new CreateCustomerCommand(dbContext);
            command.Model = newCustomer;

            var validator = new CreateCustomerCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult DeleteCustomer(int id)
        {
            var command = new DeleteCustomerCommand(dbContext);
            command.CustomerId = id;

            var validator = new DeleteCustomerCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpGet]
        public ActionResult GetCustomers()
        {
            var query = new GetCustomersQuery(dbContext, mapper);
            var result = query.Handle();
            return Ok(result);
        }
    }
}