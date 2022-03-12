using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.DirectorOperations.Queries.GetDirector
{
    public class GetDirectorDetailQuery
    {
     private readonly IMovieStoreDbContext dbContext;
        private readonly IMapper mapper;

        public int DirectorId { get; set; }

        public GetDirectorDetailQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public DirectorDetailViewModel Handle()
        {
            var director = dbContext.Directors.Include(x => x.Movies).SingleOrDefault(x=>x.Id == DirectorId);

            if (director is null)
                throw new InvalidOperationException("Yönetmen bulunamadı!");

            return mapper.Map<DirectorDetailViewModel>(director);
        }
    }

    public class DirectorDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<DirectorMovieViewModel> directorMovieViewModel { get; set; }

        public struct DirectorMovieViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}