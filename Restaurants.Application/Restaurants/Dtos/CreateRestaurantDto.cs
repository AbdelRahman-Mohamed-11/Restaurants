using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Dtos
{
    public class CreateRestaurantDto
    {
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public string Category { get; init; } = default!;
        public bool HasDelivery { get; init; }
        public string? ContactEmail { get; set; }

        public string? ContactNumber { get; set; }

        public string? City { get; init; }
        public string? Street { get; init; }
        public string? PostalCode { get; init; }
    }
}
