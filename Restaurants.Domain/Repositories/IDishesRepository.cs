using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.Repositories
{
    public interface IDishesRepository
    {
        Task<int> Create(Dish dish);

        Task<Dish?> GetByIdAsync(int restaurantId, int dishId);

        public Task Delete(Dish dish);

        public Task DeleteAll(int restaurantId);
    }
}
