using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dtos;
using Restaurants.Application.Queries.GetAllRestaurants;
using Restaurants.Application.Queries.GetRestaurantById;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/restaurants")]
public class RestaurantsController(IMediator mediatr) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var restaurants = await mediatr.Send(new GetAllRestaurantsQuery());
        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var restaurant = await mediatr.Send(new GetRestaurantByIdQuery(id));
        if (restaurant is null)
            return NotFound();

        return Ok(restaurant);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurant(CreateRestaurantCommand command)
    {
        int id = await mediatr.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }
}