using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

public class GetDishesForRestaurantQueryHandler(
    IRestaurantsRepository restaurantsRepository,RestaurantMapper mapper , ILogger<GetDishesForRestaurantQueryHandler> logger) : IRequestHandler<GetDishesForRestaurantQuery,
    IEnumerable<DishDto>>
{
    public async Task<IEnumerable<DishDto>> Handle(GetDishesForRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("getting dishes for Restaurant with ID {RestaurantId}", request.RestaurantId);

        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);

        if (restaurant is null)
        {
            logger.LogWarning("Restaurant with ID {RestaurantId} not found.", request.RestaurantId);

            throw new NotFoundException($"Restaurant with ID {request.RestaurantId} was not found.");
        }

        // get the dishes and map them to dishesDTO
        var dishes = mapper.MapDishesToDishesDto(restaurant.Dishes);

        return dishes;
    }
}
