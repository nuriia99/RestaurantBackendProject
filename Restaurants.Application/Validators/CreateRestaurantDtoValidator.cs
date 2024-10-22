using FluentValidation;
using Restaurants.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Validators
{
    public class CreateRestaurantDtoValidator : AbstractValidator<CreateRestaurantDto>
    {
        public CreateRestaurantDtoValidator() { 
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
