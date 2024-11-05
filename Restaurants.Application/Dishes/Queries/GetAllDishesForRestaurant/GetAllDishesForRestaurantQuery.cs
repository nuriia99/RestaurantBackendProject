using MediatR;
using Restaurants.Application.Dtos;

namespace Restaurants.Application.Dishes.Queries.GetAllDishesForRestaurant
{
    public class GetAllDishesForRestaurantQuery(int restaurantId) : IRequest<IEnumerable<DishDto>>
    {
        public int RestaurantId { get; } = restaurantId;
    }
}
