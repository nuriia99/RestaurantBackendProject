using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateRestaurantCommand>
    {
        private readonly List<string> validCategories = ["Spanish", "Italian", "Japanese", "American", "Italian", "Indian"];
        public UpdateRestaurantCommandValidator()
        {
            RuleFor(x => x.Name).Length(3, 100);
            RuleFor(x => x.Category).NotEmpty().WithMessage("Insert a valid category.");

            RuleFor(x => x.PostalCode).Matches(@"^\d{2}-\d{3}$").WithMessage("Please provide a valid postal code (XX-XXX).");

            RuleFor(x => x.Category).Custom((value, context) =>
            {
                if (!validCategories.Contains(value))
                {
                    context.AddFailure("Category", "Invalid category. Please choose one from the valid ones.");
                }
            });
        }
    }
}
