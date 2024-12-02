using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class RestaurantsRepository(RestaurantsDbContext restaurantsDb) : IRestaurantsRepository
{
    public async Task<int> CreateAsync(Restaurant restaurant)
    {
       restaurantsDb.Restaurnts.Add(restaurant);

       await restaurantsDb.SaveChangesAsync();

       return restaurant.Id;
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await restaurantsDb
            .Restaurnts.Include(r => r.Dishes).ToListAsync();

        return restaurants;
    
    }

    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        var restaurant = await restaurantsDb.Restaurnts
            .Include(r => r.Dishes)
            .FirstOrDefaultAsync(r => r.Id == id);

        return restaurant;
    }
}
