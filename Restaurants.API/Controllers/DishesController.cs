using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteDishByIdForRestaurant;
using Restaurants.Application.Dishes.Commands.DeleteDishes;
using Restaurants.Application.Dishes.Queries.GetAllDishesForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;
using Restaurants.Application.Dtos;
using Restaurants.Domain.Entities;

namespace Restaurants.API.Controllers
{
    [Route("api/restaurants/{restaurantId}/dishes")]
    [Authorize]
    [ApiController]
    public class DishesController(IMediator mediatr) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, CreateDishCommand command)
        {
            command.RestaurantId = restaurantId;

            var dishId = await mediatr.Send(command);
            return CreatedAtAction(nameof(GetDishByIdForRestaurant), new { restaurantId, dishId }, null);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishDto>>> GetAllDishesForRestaurant([FromRoute] int restaurantId)
        {
            var dishes = await mediatr.Send(new GetAllDishesForRestaurantQuery(restaurantId));
            return Ok(dishes);
        }

        [HttpGet("{dishId}")]
        public async Task<ActionResult<DishDto>> GetDishByIdForRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            var dish = await mediatr.Send(new GetDishByIdForRestaurantQuery(restaurantId, dishId));
            return Ok(dish);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllDishes([FromRoute] int restaurantId)
        {
            await mediatr.Send(new DeleteDishesForRestaurantCommand(restaurantId));
            return NoContent();
        }

        [HttpDelete("{dishId}")]
        public async Task<IActionResult> DeleteDishById([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            await mediatr.Send(new DeleteDishByIdForRestaurantCommand(restaurantId, dishId));
            return NoContent();
        }
    }
}
