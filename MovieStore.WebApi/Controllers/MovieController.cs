
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieStore.WebApi.Application.MovieOperations.Commands.CreateMovie;
using MovieStore.WebApi.Application.MovieOperations.Commands.DeleteMovie;
using MovieStore.WebApi.Application.MovieOperations.Commands.UpdateMovie;
using MovieStore.WebApi.Application.MovieOperations.Queries.GetMovieDetail;
using MovieStore.WebApi.Application.MovieOperations.Queries.GetMovies;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieStoreDbContext dbContext;
        private readonly IMapper mapper;

        public MovieController(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddMovie([FromBody] CreateMovieModel newMovie)
        {
            var command = new CreateMovieCommand(dbContext);
            command.Model = newMovie;
            var validator = new CreateMovieCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult DeleteMovie(int id)
        {
            var command = new DeleteMovieCommand(dbContext);
            command.MovieId = id;
            var validator = new DeleteMovieCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpGet]
        public ActionResult GetMovies()
        {
            var query = new GetMoviesQuery(dbContext, mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("id")]
        public ActionResult GetMovieDetail(int id)
        {
            var query = new GetMovieDetailQuery(dbContext, mapper);
            query.MovieId = id;
            var validator = new GetMovieDetailQueryValidator();
            validator.ValidateAndThrow(query);

            var result = query.Handle();
            return Ok(result);
        }

        [HttpPut("id")]
        public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieModel updateMovie)
        {
            var command = new UpdateMovieCommand(dbContext);
            command.MovieId = id;
            command.Model = updateMovie;

            var validator = new UpdateMovieCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
    }
}