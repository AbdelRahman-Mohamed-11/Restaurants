using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.API.Controllers
{
    public class RestaurantsController(IRestaurantsService restaurantsService) : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var restaurants = await restaurantsService.GetAllRestaurants();

            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var restaurant = await restaurantsService.GetRestaurantByIdAsync(id);

            if (restaurant is null)
                return NotFound();

            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant(CreateRestaurantDto createRestaurantDto)
        {
            int id = await restaurantsService.CreateRestaurantAsync(createRestaurantDto);

            return CreatedAtAction(nameof(GetById), new { id }, null);
        }
    }
}
