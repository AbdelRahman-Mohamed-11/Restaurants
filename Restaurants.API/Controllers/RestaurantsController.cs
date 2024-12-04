using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.API.Extensions;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;

namespace Restaurants.API.Controllers
{
    /// <summary>
    /// Controller to manage restaurant-related operations.
    /// </summary>

    public class RestaurantsController(IMediator mediator) : BaseApiController
    {
        /// <summary>
        /// Get all restaurants.
        /// </summary>
        /// <returns>List of restaurants.</returns>
        /// <response code="200">Returns the list of restaurants.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDTO>>> GetAll()
        {
            var restaurants = await mediator.Send(new GetAllRestaurantsQuery());
            return Ok(restaurants);
        }

        /// <summary>
        /// Get restaurant by ID.
        /// </summary>
        /// <param name="id">Restaurant ID.</param>
        /// <returns>The restaurant details.</returns>
        /// <response code="200">Returns the restaurant details.</response>
        /// <response code="404">If the restaurant is not found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RestaurantDTO>> GetById([FromRoute] int id)
        {
            return Ok(await mediator.Send(new GetRestaurantByIdQuery(id)));
        }

        /// <summary>
        /// Create a new restaurant.
        /// </summary>
        /// <param name="createRestaurantCommand">Details of the restaurant to create.</param>
        /// <returns>The ID of the newly created restaurant.</returns>
        /// <response code="201">If the restaurant is successfully created.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateRestaurantCommand createRestaurantCommand)
        {
            int id = await mediator.Send(createRestaurantCommand);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        /// <summary>
        /// Partially update an existing restaurant.
        /// </summary>
        /// <param name="id">Restaurant ID.</param>
        /// <param name="updateRestaurantCommand">Details of the restaurant to update.</param>
        /// <response code="204">If the restaurant is successfully updated.</response>
        /// <response code="404">If the restaurant is not found.</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRestaurantCommand updateRestaurantCommand)
        {
            updateRestaurantCommand.Id = id;

            await mediator.Send(updateRestaurantCommand);

            return NoContent();
        }

        /// <summary>
        /// Delete a restaurant by ID.
        /// </summary>
        /// <param name="id">Restaurant ID.</param>
        /// <response code="204">If the restaurant is successfully deleted.</response>
        /// <response code="404">If the restaurant is not found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            await mediator.Send(new DeleteRestaurantCommand(id));

            return NoContent();
        }
    }
}
