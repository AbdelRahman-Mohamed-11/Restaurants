using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Dtos
{
    public record RestaurantDTO
    {
        public int Id { get; set; }
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public string Category { get; init; } = default!;
        public bool HasDelivery { get; init; }
        public string? City { get; init; }
        public string? Street { get; init; }
        public string? PostalCode { get; init; }
        public List<DishDto> Dishes { get; init; } = [];
    }


}
