using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.MovieOperations.Queries.GetMovieDetail
{
    public class GetMovieDetailQuery
    {
        private readonly IMovieStoreDbContext dbContext;
        private readonly IMapper mapper;

        public int MovieId { get; set; }

        public GetMovieDetailQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public MovieDetailViewModel Handle()
        {
            var movie = dbContext.Movies.Include(x => x.MovieActors).ThenInclude(x => x.Actor).Include(x => x.Director).Include(x => x.Genre).SingleOrDefault(x=>x.Id == MovieId && x.IsActive);

            if (movie is null)
                throw new InvalidOperationException("Film bulunamadı!");

            return mapper.Map<MovieDetailViewModel>(movie);
        }
    }

    public class MovieDetailViewModel
    {
        public string Name { get; set; }
        public DateTime Year { get; set; }
        public decimal Price { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }

        public List<MovieActorsViewModel> movieActorsViewModel { get; set; }

        public struct MovieActorsViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
        }
    }
}
