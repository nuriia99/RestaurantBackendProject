using MediatR;
using Restaurants.Application.Dtos;

namespace Restaurants.Application.Queries.GetRestaurantById
{
    public class GetRestaurantByIdQuery(int id) : IRequest<RestaurantDto?>
    {
        public int Id { get; } = id;
    }
}
