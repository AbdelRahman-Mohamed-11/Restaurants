using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;

internal class GetDishByIdForRestaurantQueryHandler(IDishesRepository dishesRepository
    , ILogger<GetDishByIdForRestaurantQueryHandler> logger) : IRequestHandler<GetDishByIdForRestaurantQuery, DishDto>
{
    public async Task<DishDto> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("getting a dish with id {request.DishId} for Restaurant with ID {RestaurantId}", request.DishId, request.RestaurantId);

        var dish = await dishesRepository.GetByIdAsync(request.RestaurantId , request.DishId);

        if (dish is null)
        {
            logger.LogWarning("dish with ID : {DishId} for Restaurant with ID : {RestaurantId} not found.", request.DishId, request.RestaurantId);

            throw new NotFoundException($"dish with ID {request.DishId} for Restaurant with ID {request.RestaurantId} was not found.");
        }

        return dish.Adapt<DishDto>();
    }
}
