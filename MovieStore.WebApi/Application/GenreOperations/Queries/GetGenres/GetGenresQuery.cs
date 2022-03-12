using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        private readonly IMovieStoreDbContext dbContext;
        private readonly IMapper mapper;

        public GetGenresQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public List<GenresViewModel> Handle(){
            var genres = dbContext.Genres.Where(x=>x.IsActive).OrderBy(x=>x.Id);
            return mapper.Map<List<GenresViewModel>>(genres);
        }
    }

    public class GenresViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}