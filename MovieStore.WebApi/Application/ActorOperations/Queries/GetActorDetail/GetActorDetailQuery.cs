using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.ActorOperations.Queries.GetActorDetail
{
    public class GetActorDetailQuery
    {
        private readonly IMovieStoreDbContext dbContext;
        private readonly IMapper mapper;

        public int ActorId { get; set; }

        public GetActorDetailQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public ActorDetailViewModel Handle()
        {
            var actor = dbContext.Actors.Include(x => x.MovieActors).ThenInclude(x => x.Movie).SingleOrDefault(x=>x.Id == ActorId);

            if (actor is null)
                throw new InvalidOperationException("Oyuncu bulunamadı!");

            return mapper.Map<ActorDetailViewModel>(actor);
        }
    }

    public class ActorDetailViewModel
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
