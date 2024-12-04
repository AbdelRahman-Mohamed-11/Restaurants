using LightResults;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

internal class UpdateRestaurantCommandHandler(
    ILogger<UpdateRestaurantCommandHandler> logger , 
    IRestaurantsRepository restaurantsRepository) : 
    IRequestHandler<UpdateRestaurantCommand>


{
    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("updating the restaurant with the id : {RestaurantId}" , request.Id);

        var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);

        if (restaurant is null)
            throw new NotFoundException($"Restaurant with ID {request.Id} was not found.");

        // map from CreateRestaurant to Restaurant 

        request.Adapt(restaurant);


        await restaurantsRepository.SaveAsync();

    }
}
