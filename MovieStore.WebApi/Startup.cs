using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MovieStore.WebApi.DbOperations;
using AutoMapper;
using MovieStore.WebApi.Common;
using MovieStore.WebApi.Services;
using MovieStore.WebApi.Middlewares;

namespace MovieStore.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieStore.WebApi", Version = "v1" });
            });

            services.AddDbContext<MovieStoreDbContext>(options => options.UseInMemoryDatabase(databaseName: "MovieStoreDB"));
            services.AddScoped<IMovieStoreDbContext>(provider => provider.GetService<MovieStoreDbContext>());
            services.AddSingleton<ILoggerService, ConsoleLogger>();

            var mapperConfig = new MapperConfiguration(mc =>
                                {
                                    mc.AddProfile(new MappingProfile());
                                });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            //services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieStore.WebApi v1"));
            }

            app.UseCustomExceptionMiddleware();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
