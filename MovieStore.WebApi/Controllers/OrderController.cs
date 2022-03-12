
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieStore.WebApi.Application.OrderOperations.Commands.CreateOrder;
using MovieStore.WebApi.Application.OrderOperations.Queries.GetOrders;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class OrderController : ControllerBase
    {
        private readonly IMovieStoreDbContext dbContext;
        private readonly IMapper mapper;

        public OrderController(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddOrder([FromBody] CreateOrderModel newOrder)
        {
            var command = new CreateOrderCommand(dbContext);
            command.Model = newOrder;
            var validator = new CreateOrderCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        // [HttpDelete("id")]
        // public IActionResult DeleteMovie(int id)
        // {
        //     var command = new DeleteMovieCommand(dbContext);
        //     command.MovieId = id;
        //     var validator = new DeleteMovieCommandValidator();
        //     validator.ValidateAndThrow(command);

        //     command.Handle();
        //     return Ok();
        // }

        [HttpGet]
        public ActionResult GetOrders()
        {
            var query = new GetOrdersQuery(dbContext, mapper);
            var result = query.Handle();
            return Ok(result);
        }

        // [HttpGet("id")]
        // public ActionResult GetMovieDetail(int id)
        // {
        //     var query = new GetMovieDetailQuery(dbContext, mapper);
        //     query.MovieId = id;
        //     var validator = new GetMovieDetailQueryValidator();
        //     validator.ValidateAndThrow(query);

        //     var result = query.Handle();
        //     return Ok(result);
        // }

        // [HttpPut("id")]
        // public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieModel updateMovie)
        // {
        //     var command = new UpdateMovieCommand(dbContext);
        //     command.MovieId = id;
        //     command.Model = updateMovie;

        //     var validator = new UpdateMovieCommandValidator();
        //     validator.ValidateAndThrow(command);

        //     command.Handle();
        //     return Ok();
        // }
    }
}