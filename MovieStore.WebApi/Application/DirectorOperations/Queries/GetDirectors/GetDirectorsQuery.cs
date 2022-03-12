using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.WebApi.DbOperations;

namespace MovieStore.WebApi.Application.DirectorOperations.Queries.GetDirector
{
    public class GetDirectorsQuery
    {
        private readonly IMovieStoreDbContext dbContext;
        private readonly IMapper mapper;

        public GetDirectorsQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public List<DirectorViewModel> Handle()
        {
            var directors = dbContext.Directors.Include(x => x.Movies).OrderBy(x => x.Id).ToList();
            if (directors is null)
                throw new InvalidOperationException("Yönetmen bulunamadı!");

            return mapper.Map<List<DirectorViewModel>>(directors);
        }
    }

    public class DirectorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<DirectorMovieViewModel> Movies { get; set; }

        public struct DirectorMovieViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}