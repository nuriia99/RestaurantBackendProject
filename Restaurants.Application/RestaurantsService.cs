using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using System.Collections.Generic;

namespace Restaurants.Application.Restaurants;

internal class RestaurantsService(IRestaurantsRepository restaurantsRepository,
    ILogger<RestaurantsService> logger, IMapper mapper) : IRestaurantsService
{
    public async Task<int> CreateRestaurant(CreateRestaurantDto dto)
    {
        logger.LogInformation("Creating a new restaurants");
        return await restaurantsRepository.Create(mapper.Map<Restaurant>(dto));
    }

    public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
    {
        logger.LogInformation("Getting all restaurants");
        var restaurants = await restaurantsRepository.GetAllAsync();
        return mapper.Map<IEnumerable<Restaurant>?, IEnumerable<RestaurantDto>>(restaurants);
    }

    public async Task<RestaurantDto?> GetById(int id)
    {
        logger.LogInformation($"Getting restaurant {id}");
        var restaurant = await restaurantsRepository.GetByIdAsync(id);

        return mapper.Map<Restaurant?, RestaurantDto?>(restaurant);

    }
}