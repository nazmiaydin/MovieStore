
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieStore.WebApi.Application.ActorOperations.Commands.CreateActor;
using MovieStore.WebApi.Application.ActorOperations.Commands.DeleteActor;
using MovieStore.WebApi.Application.ActorOperations.Commands.UpdateActor;
using MovieStore.WebApi.Application.ActorOperations.Queries.GetActorDetail;
using MovieStore.WebApi.Application.ActorOperations.Queries.GetActors;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class ActorController : ControllerBase
    {
        private readonly IMovieStoreDbContext dbContext;
        private readonly IMapper mapper;

        public ActorController(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddActor([FromBody] CreateActorModel newActor)
        {
            var command = new CreateActorCommand(dbContext);
            command.Model = newActor;
            var validator = new CreateActorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult DeleteActor(int id)
        {
            var command = new DeleteActorCommand(dbContext);
            command.ActorId = id;
            var validator = new DeleteActorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpGet]
        public ActionResult GetActors()
        {
            var query = new GetActorsQuery(dbContext, mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("id")]
        public ActionResult GetActorDetail(int id)
        {
            var query = new GetActorDetailQuery(dbContext, mapper);
            query.ActorId = id;
            var validator = new GetActorDetailQueryValidator();
            validator.ValidateAndThrow(query);

            var result = query.Handle();
            return Ok(result);
        }

        [HttpPut("id")]
        public IActionResult UpdateActor(int id, [FromBody] UpdateActorModel updateActor)
        {
            var command = new UpdateActorCommand(dbContext);
            command.ActorId = id;
            command.Model = updateActor;

            var validator = new UpdateActorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
    }
}