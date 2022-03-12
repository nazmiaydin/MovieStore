using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.WebApi.DbOperations;
using MovieStore.WebApi.Entities;

namespace MovieStore.WebApi.Application.MovieOperations.Queries.GetMovies
{
    public class GetMoviesQuery
    {
        private readonly IMovieStoreDbContext dbContext;
        private readonly IMapper mapper;

        public GetMoviesQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public List<MovieViewModel> Handle()
        {
            var movies = dbContext.Movies.Include(x => x.MovieActors).ThenInclude(x => x.Actor).Include(x => x.Director).Include(x => x.Genre).Where(x=>x.IsActive).OrderBy(x => x.Id).ToList();
            if (movies is null)
                throw new InvalidOperationException("Film bulunamadı!");

            return mapper.Map<List<MovieViewModel>>(movies);
        }
    }

    public class MovieViewModel
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