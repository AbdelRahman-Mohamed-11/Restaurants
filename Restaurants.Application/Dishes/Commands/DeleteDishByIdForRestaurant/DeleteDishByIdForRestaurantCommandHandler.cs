using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Commands.RemoveDishByIdForRestaurant;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteDishByIdForRestaurant
{
    public class DeleteDishByIdForRestaurantCommandHandler(ILogger<DeleteDishByIdForRestaurantCommandHandler> logger,
        IDishesRepository dishesRepository
       ) : IRequestHandler<DeleteDishByIdForRestaurantCommand>
    {
        public async Task Handle(DeleteDishByIdForRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("removing the dish with id :{DishID} for Restaurant with ID : {RestaurantId}", request.DishId, request.RestaurantId);

            var dish = await dishesRepository.GetByIdAsync(request.RestaurantId, request.DishId);

            if (dish is null)
            {
                logger.LogWarning("Restaurant with ID : {RestaurantId} not found.", request.RestaurantId);

                throw new NotFoundException($"Restaurant with ID {request.RestaurantId} was not found.");
            }

            await dishesRepository.Delete(dish);
        }
    }
}
