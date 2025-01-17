using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.UploadRestaurantLogo
{
    public class UploadRestaurantLogoCommandHandler(ILogger<UploadRestaurantLogoCommandHandler> logger,
        IRestaurantsRepository restaurantsRepository,
        IRestaurantAuthorizationService restaurantAuthorizationService,
        IBlobStorageService blobStorageService
        ) : IRequestHandler<UploadRestaurantLogoCommand>
    {
        public async Task Handle(UploadRestaurantLogoCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Uploading restaurant logo for id: {RestaurantId}", request.RestaurantId);
            var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
                ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
                throw new ForbidException();

            var logoUrl = await blobStorageService.UploadToBlobAsync(request.File, request.FileName);
            restaurant.LogoUrl = logoUrl;

            await restaurantsRepository.Update(restaurant);
        }
    }
}
