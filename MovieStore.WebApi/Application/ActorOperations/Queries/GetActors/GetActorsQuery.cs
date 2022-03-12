using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.WebApi.DbOperations;
using MovieStore.WebApi.Entities;

namespace MovieStore.WebApi.Application.ActorOperations.Queries.GetActors
{
    public class GetActorsQuery
    {

        private readonly IMovieStoreDbContext dbContext;
        private readonly IMapper mapper;

        public GetActorsQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public List<ActorViewModel> Handle()
        {
            var actors = dbContext.Actors.Include(x => x.MovieActors).ThenInclude(x => x.Movie).OrderBy(x => x.Id).ToList();
            if (actors is null)
                throw new InvalidOperationException("Oyuncu bulunamadı!");

            return mapper.Map<List<ActorViewModel>>(actors);
        }
    }

    public class ActorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<MovieActorsViewModel> movieActorsViewModel { get; set; }
        public struct MovieActorsViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }


}