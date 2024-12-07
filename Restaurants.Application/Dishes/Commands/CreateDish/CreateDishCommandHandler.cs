using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger , 
        IRestaurantsRepository restaurantsRepository , 
        IDishesRepository dishesRepository) 
        : IRequestHandler<CreateDishCommand ,int>
    {
        public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a new Dish for a restaurant {@restaurant}", request);

            var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);

            if (restaurant is null)
            {
                logger.LogWarning("Restaurant with ID {RestaurantId} not found.", request.RestaurantId);

                throw new NotFoundException($"Restaurant with ID {request.RestaurantId} was not found.");
            }

           return await dishesRepository.Create(request.Adapt<Dish>());
        }
    }
}
