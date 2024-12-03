using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;

namespace Restaurants.API.Controllers
{
    public class RestaurantsController(IMediator mediator) : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var restaurants = await mediator.Send(new GetAllRestaurantsQuery());

            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));

            if (restaurant is null)
                return NotFound();

            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRestaurantCommand createRestaurantCommand)
        {
            int id = await mediator.Send(createRestaurantCommand);

            return CreatedAtAction(nameof(GetById), new { id }, null);
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id ,[FromBody] 
            UpdateRestaurantCommand updateRestaurantCommand)
        {
            updateRestaurantCommand.Id = id;    
            
            bool isUpdated = await mediator.Send(updateRestaurantCommand);

            return isUpdated ? NoContent() : NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            bool isDeleted = await mediator.Send(new DeleteRestaurantCommand(id));

            return isDeleted ? NoContent() : NotFound();
        }

    }
}
