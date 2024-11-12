using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dtos;
using Restaurants.Application.Restaurants.Commands;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;

namespace Restaurants.API.Controllers;

[ApiController]
[Authorize]
[Route("api/restaurants")]
public class RestaurantsController(IMediator mediatr) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll()
    {
        var restaurants = await mediatr.Send(new GetAllRestaurantsQuery());
        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RestaurantDto?>> GetById([FromRoute] int id)
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

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
    {
        await mediatr.Send(new DeleteRestaurantCommand(id));

        return NotFound();
    }
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRestaurant([FromBody] UpdateRestaurantCommand command)
    {
        await mediatr.Send(command);

        return NotFound();
    }

}