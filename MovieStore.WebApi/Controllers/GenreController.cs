using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieStore.WebApi.Application.GenreOperations.Queries.GetGenres;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class GenreController : ControllerBase
    {
        private readonly IMovieStoreDbContext dbContext;
        private readonly IMapper mapper;

        public GenreController(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetGenres()
        {
            var query = new GetGenresQuery(dbContext, mapper);
            var result = query.Handle();
            return Ok(result);
        }
    }
}