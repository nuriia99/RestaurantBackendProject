using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dtos;
using Restaurants.Application.Queries.GetAllRestaurants;
using Restaurants.Application.Queries.GetRestaurantById;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

namespace Restaurants.API.Controllers;

[ApiController]
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
        var isDeleted = await mediatr.Send(new DeleteRestaurantCommand(id));

        if (isDeleted)
            return NoContent();

        return NotFound();
    }
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRestaurant([FromBody] UpdateRestaurantCommand command)
    {
        var isUpdated = await mediatr.Send(command);

        if (isUpdated)
            return NoContent();

        return NotFound();
    }

}