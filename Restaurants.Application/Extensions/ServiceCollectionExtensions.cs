﻿using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Users;

namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly));
        services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
        services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly).AddFluentValidationAutoValidation();

        services.AddScoped<IUserContext, UserContext>();
    }
}