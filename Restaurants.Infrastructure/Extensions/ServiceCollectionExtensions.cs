﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;

namespace Restaurants.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RestaurantsDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("RestaurantsDb")));
            services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
        }
    }
}
