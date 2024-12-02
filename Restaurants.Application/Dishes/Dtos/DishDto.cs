using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Dtos
{
    public record DishDto(int Id , string Name, string Description,
       decimal Price, int? KiloCalories);

}
