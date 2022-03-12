using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.WebApi.DbOperations;
using MovieStore.WebApi.Entities;

namespace MovieStore.WebApi.Application.OrderOperations.Queries.GetOrders
{
    public class GetOrdersQuery
    {
        private readonly IMovieStoreDbContext dbContext;
        private readonly IMapper mapper;

        public GetOrdersQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public List<OrderViewModel> Handle()
        {
            var orders = dbContext.Orders.Include(x => x.Customer).Include(x => x.Movie).OrderBy(x => x.Id).ToList();
            if (orders is null)
                throw new InvalidOperationException("Sipariş bulunamadı!");

            return mapper.Map<List<OrderViewModel>>(orders);
        }
    }

    public class OrderViewModel
    {
        public int CustomerId { get; set; }
        public int MovieId { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
    }


}