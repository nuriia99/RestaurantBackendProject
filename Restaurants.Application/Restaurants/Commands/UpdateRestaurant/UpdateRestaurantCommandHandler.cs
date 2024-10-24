using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger,
        IRestaurantsRepository restaurantsRepository, IMapper mapper) : IRequestHandler<UpdateRestaurantCommand, bool>
    {

        public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating a new restaurant {@Restaurant}", request);
            var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);

            if (restaurant is null) return false;

            await restaurantsRepository.Update(mapper.Map<UpdateRestaurantCommand, Restaurant>(request));
            return true;
        }
    }
}
