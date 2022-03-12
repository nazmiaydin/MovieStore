
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieStore.WebApi.Application.DirectorOperations.Commands.CreateDirector;
using MovieStore.WebApi.Application.DirectorOperations.Commands.DeleteDirector;
using MovieStore.WebApi.Application.DirectorOperations.Commands.UpdateDirector;
using MovieStore.WebApi.Application.DirectorOperations.Queries.GetDirector;
using MovieStore.WebApi.Application.DirectorOperations.Queries.GetDirectorDetail;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class DirectorController : ControllerBase
    {
        private readonly IMovieStoreDbContext dbContext;
        private readonly IMapper mapper;

        public DirectorController(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddDirector([FromBody] CreateDirectorModel newDirector)
        {
            var command = new CreateDirectorCommand(dbContext);
            command.Model = newDirector;
            var validator = new CreateDirectorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult DeleteDirector(int id)
        {
            var command = new DeleteDirectorCommand(dbContext);
            command.DirectorId = id;
            var validator = new DeleteDirectorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpGet]
        public ActionResult GetDirectors()
        {
            var query = new GetDirectorsQuery(dbContext, mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("id")]
        public ActionResult GetDirectorDetail(int id)
        {
            var query = new GetDirectorDetailQuery(dbContext, mapper);
            query.DirectorId = id;
            var validator = new GetDirectorDetailQueryValidator();
            validator.ValidateAndThrow(query);

            var result = query.Handle();
            return Ok(result);
        }

        [HttpPut("id")]
        public IActionResult UpdateDirector(int id, [FromBody] UpdateDirectorModel updateDirector)
        {
            var command = new UpdateDirectorCommand(dbContext);
            command.DirectorId = id;
            command.Model = updateDirector;

            var validator = new UpdateDirectorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
    }
}