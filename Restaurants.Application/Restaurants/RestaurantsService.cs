using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants
{
    internal class RestaurantsService(IRestaurantsRepository restaurantsRepository,
        ILogger<RestaurantsService> logger,
        RestaurantMapper mapper) : IRestaurantsService
    {
        public async Task<int> CreateRestaurantAsync(CreateRestaurantDto createRestaurantDto)
        {
            logger.LogInformation("Creating a new restaurant");

            var restaurant = mapper.MapCreateRestaurantToRestaurant(createRestaurantDto);

            var id = await restaurantsRepository.CreateAsync(restaurant);

            return id;
        }

        public async Task<IEnumerable<RestaurantDTO>> GetAllRestaurants()
        {
            logger.LogInformation("Getting all restaurants");

            var restaurants = await restaurantsRepository.GetAllAsync();

            var restaurantDtos = mapper.MapRestaurantListToDtoList(restaurants);

            return restaurantDtos;
        }

        public async Task<RestaurantDTO?> GetRestaurantByIdAsync(int id)
        {
            logger.LogInformation($"Getting a restaurant with ID {id}");

            var restaurant = await restaurantsRepository.GetByIdAsync(id);

            return restaurant != null ? mapper.MapRestaurantToDto(restaurant) : null;
        }
    }
}
