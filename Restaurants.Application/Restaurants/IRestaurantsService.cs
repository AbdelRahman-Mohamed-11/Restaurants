using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants
{
    public interface IRestaurantsService
    {
        Task<IEnumerable<RestaurantDTO>> GetAllRestaurants();
        Task<RestaurantDTO?> GetRestaurantByIdAsync(int id);

        Task<int> CreateRestaurantAsync(CreateRestaurantDto createRestaurantDto);
    }
}