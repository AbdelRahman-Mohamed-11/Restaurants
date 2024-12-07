using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.RemoveAllDishesForRestaurant;
using Restaurants.Application.Dishes.Commands.RemoveDishByIdForRestaurant;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

namespace Restaurants.API.Controllers
{
    /// <summary>
    /// Controller to manage dish-related operations within a restaurant.
    /// </summary>
    [Route("api/restaurants/{restaurantId}/[controller]")]
    [ApiController]
    public class DishesController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Get all dishes for a restaurant.
        /// </summary>
        /// <param name="restaurantId">The ID of the restaurant.</param>
        /// <returns>List of dishes for the specified restaurant.</returns>
        /// <response code="200">Returns the list of dishes.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DishDto>>> GetDishesForRestaurant(int restaurantId)
        {
            var dishes = await mediator.Send(new GetDishesForRestaurantQuery(restaurantId));
            return Ok(dishes);
        }

        /// <summary>
        /// Get a specific dish for a restaurant by its ID.
        /// </summary>
        /// <param name="restaurantId">The ID of the restaurant.</param>
        /// <param name="dishId">The ID of the dish.</param>
        /// <returns>The details of the specified dish.</returns>
        /// <response code="200">Returns the dish details.</response>
        /// <response code="404">If the dish is not found.</response>
        [HttpGet("{dishId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DishDto>> GetDishByIdForRestaurant(int restaurantId, int dishId)
        {
            var dish = await mediator.Send(new GetDishByIdForRestaurantQuery(restaurantId, dishId));
            return Ok(dish);
        }

        /// <summary>
        /// Create a new dish for a restaurant.
        /// </summary>
        /// <param name="restaurantId">The ID of the restaurant.</param>
        /// <param name="createDishCommand">Details of the dish to create.</param>
        /// <returns>The ID of the newly created dish.</returns>
        /// <response code="201">If the dish is successfully created.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, [FromBody] CreateDishCommand createDishCommand)
        {
            createDishCommand.RestaurantId = restaurantId;
            
            var dishId = await mediator.Send(createDishCommand);
            
            return CreatedAtAction(nameof(GetDishByIdForRestaurant) , 
                new {restaurantId , dishId} , null);
        }

        /// <summary>
        /// Remove all dishes for a restaurant.
        /// </summary>
        /// <param name="restaurantId">The ID of the restaurant.</param>
        /// <response code="204">If all dishes are successfully removed.</response>
        /// <response code="404">If the restaurant is not found.</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveAllDishesForRestaurant(int restaurantId)
        {
            await mediator.Send(new DeleteAllDishesForRestaurantCommand(restaurantId));
            return NoContent();
        }

        /// <summary>
        /// Remove a specific dish for a restaurant by its ID.
        /// </summary>
        /// <param name="restaurantId">The ID of the restaurant.</param>
        /// <param name="dishId">The ID of the dish.</param>
        /// <response code="204">If the dish is successfully removed.</response>
        /// <response code="404">If the dish or restaurant is not found.</response>
        [HttpDelete("{dishId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveDishByIdForRestaurant(int restaurantId, int dishId)
        {
            await mediator.Send(new DeleteDishByIdForRestaurantCommand(restaurantId, dishId));
            return NoContent();
        }
    }
}
