﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dtos
{
    public class CreateRestaurantDto
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public bool HasDelivery { get; set; } = default!;

        public string? ContactEmail { get; set; }
        public string? ContactNumber { get; set; }

        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
    }
}