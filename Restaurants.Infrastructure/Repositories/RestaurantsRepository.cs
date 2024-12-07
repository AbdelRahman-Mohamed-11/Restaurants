using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class RestaurantsRepository(RestaurantsDbContext restaurantsDb) : IRestaurantsRepository
{
    public async Task<int> CreateAsync(Restaurant restaurant)
    {
       restaurantsDb.Restaurants.Add(restaurant);

       await restaurantsDb.SaveChangesAsync();

       return restaurant.Id;
    }


    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await restaurantsDb
            .Restaurants
            .AsNoTracking().Include(r => r.Dishes).ToListAsync();

        return restaurants;
    
    }

    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        var restaurant = await restaurantsDb.Restaurants
            .Include(r => r.Dishes)
            .FirstOrDefaultAsync(r => r.Id == id);

        return restaurant;
    }
    public async Task DeleteAsync(Restaurant restaurant)
    {
        restaurantsDb.Restaurants.Remove(restaurant);

        await restaurantsDb.SaveChangesAsync();
    }

    public Task UpdateAsync(Restaurant restaurant)
    {
        // you can implement it if you want , no need of this untill now.
        throw new NotImplementedException();
    }



    public async Task SaveAsync() => await restaurantsDb.SaveChangesAsync();
}
