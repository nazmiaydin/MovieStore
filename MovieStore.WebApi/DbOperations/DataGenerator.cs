using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieStore.WebApi.Entities;

namespace MovieStore.WebApi.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MovieStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<MovieStoreDbContext>>()))
            {
                var MovieActors = new List<MovieActors>()
                {
                    new MovieActors(){ActorId = 1, MovieId = 1},
                    new MovieActors(){ActorId = 1, MovieId = 2},
                    new MovieActors(){ActorId = 2, MovieId = 1},
                    new MovieActors(){ActorId = 3, MovieId = 1},
                    new MovieActors(){ActorId = 4, MovieId = 2},
                    new MovieActors(){ActorId = 5, MovieId = 2},
                    new MovieActors(){ActorId = 6, MovieId = 3},
                    new MovieActors(){ActorId = 7, MovieId = 3},
                };

                if (context.Genres.Any())
                {
                    return;
                }

                context.Genres.AddRange(
                    new Genre { Name = "Komedi" },
                    new Genre { Name = "Dram" },
                    new Genre { Name = "Aksiyon" },
                    new Genre { Name = "Bilim-Kurgu" }
                );

                if (context.Customers.Any())
                {
                    return;
                }

                context.Customers.AddRange(
                                    new Customer { Name = "Erdal", Surname = "Bakkal", FavoriteGenreId = 1, PurchasedMoviesId = 1 },
                                    new Customer { Name = "Ali", Surname = "Gocuk", FavoriteGenreId = 2, PurchasedMoviesId = 2 },
                                    new Customer { Name = "Veli", Surname = "Ilgaz", FavoriteGenreId = 4, PurchasedMoviesId = 3 }
                                );

                if (context.Actors.Any())
                {
                    return;
                }

                context.Actors.AddRange(
                                    new Actor { Name = "Timothée", Surname = "Chalamet" },
                                    new Actor { Name = "Rebecca", Surname = "Ferguson" },
                                    new Actor { Name = "Oscar", Surname = "Isaac", },
                                    new Actor { Name = "Robert", Surname = "Pattinson" },
                                    new Actor { Name = "Zoë", Surname = "Kravitz" },
                                    new Actor { Name = "Tom", Surname = "Holland" },
                                    new Actor { Name = "Sophia ", Surname = "Ali" }
                                );

                if (context.Directors.Any())
                {
                    return;
                }

                context.Directors.AddRange(
                        new Director { Name = "Ruben", Surname = "Fleischer" },
                        new Director { Name = "Matt", Surname = "Reeves" },
                        new Director { Name = "Denis", Surname = "Villeneuve" }
                    );

                if (context.Movies.Any())
                {
                    return;
                }

                context.Movies.AddRange(
                    new Movie { Name = "Dune", GenreId = 4, DirectorId = 3, Year = new DateTime(2021, 01, 01), Price = 10 },
                    new Movie { Name = "Batman", GenreId = 4, DirectorId = 2, Year = new DateTime(2021, 03, 01), Price = 10 },
                    new Movie { Name = "Uncharted", GenreId = 4, DirectorId = 1, Year = new DateTime(2021, 02, 01), Price = 10 }
                );

                if (context.MovieActors.Any())
                {
                    return;
                }

                context.MovieActors.AddRange(MovieActors);

                // var DuneMoive = new List<Movie>();
                // DuneMoive.Add(new Movie { Name = "Dune", GenreId = 4, ActorId = 1, DirectorId = 3, Year = new DateTime(2021, 01, 01), Price = 10 });

                // var BatmanMoive = new List<Movie>();
                // BatmanMoive.Add(new Movie { Name = "Batman", GenreId = 4, ActorId = 4, DirectorId = 2, Year = new DateTime(2021, 03, 01), Price = 10 });

                // var UnchartedMoive = new List<Movie>();
                // UnchartedMoive.Add(new Movie { Name = "Uncharted", GenreId = 4, ActorId = 6, DirectorId = 1, Year = new DateTime(2021, 02, 01), Price = 10 });

                context.SaveChanges();
            }
        }
    }
}