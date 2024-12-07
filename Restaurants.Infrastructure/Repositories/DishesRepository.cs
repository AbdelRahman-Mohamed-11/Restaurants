using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    internal class DishesRepository(RestaurantsDbContext restaurantsDb) : IDishesRepository
    {
        public async Task<int> Create(Dish dish)
        {
            restaurantsDb.Dishes.Add(dish);
            await restaurantsDb.SaveChangesAsync();
            return dish.Id;
        }

        public async Task<Dish?> GetByIdAsync(int restaurantId , int dishId)
        {
            return await restaurantsDb.Dishes.FirstOrDefaultAsync(d => d.Id == dishId
            && d.RestaurantId == restaurantId);
        }

        public async Task DeleteAll(int restaurantId)
        {
            await restaurantsDb.Dishes
                .Where(d => d.RestaurantId == restaurantId)
                .ExecuteDeleteAsync();
        }

        public async Task Delete(Dish dish)
        {
            restaurantsDb.Dishes.Remove(dish);
            await restaurantsDb.SaveChangesAsync();
        }

    }
}
