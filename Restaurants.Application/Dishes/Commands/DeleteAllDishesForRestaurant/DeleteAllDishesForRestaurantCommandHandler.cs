using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Commands.RemoveAllDishesForRestaurant;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteAllDishesForRestaurant;

public class DeleteAllDishesForRestaurantCommandHandler(ILogger<DeleteAllDishesForRestaurantCommandHandler> logger,
    IDishesRepository dishesRepository, IRestaurantsRepository restaurantsRepository) : IRequestHandler<DeleteAllDishesForRestaurantCommand>
{
    public async Task Handle(DeleteAllDishesForRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("removing all dishes for Restaurant with ID : {RestaurantId}", request.RestaurantId);

        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);

        if (restaurant is null)
        {
            logger.LogWarning("Restaurant with ID : {RestaurantId} not found.", request.RestaurantId);

            throw new NotFoundException($"Restaurant with ID {request.RestaurantId} was not found.");
        }


        await dishesRepository.DeleteAll(request.RestaurantId);

    }
}
