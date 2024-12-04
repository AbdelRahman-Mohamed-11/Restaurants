using LightResults;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler(
        ILogger<DeleteRestaurantCommandHandler> logger , 
        IRestaurantsRepository restaurantsRepository) : 
        IRequestHandler<DeleteRestaurantCommand>
    {
        public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("deleting the restaurant with the id : {RestaurantId}", request.Id);

            var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);

            if (restaurant is null)
            {
                logger.LogWarning("Restaurant with ID {RestaurantId} not found.", request.Id);
                
                throw new NotFoundException($"Restaurant with ID {request.Id} was not found.");
            }

            await restaurantsRepository.DeleteAsync(restaurant);

        }
    }
}
