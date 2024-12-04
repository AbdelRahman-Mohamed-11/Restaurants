using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger ,
    RestaurantMapper mapper , IRestaurantsRepository restaurantsRepository) :
    IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating a new restaurant {@restaurant}",request);

        var restaurant = mapper.MapCreateRestaurantCommandToRestaurant(request);

        var id = await restaurantsRepository.CreateAsync(restaurant);

        return id;
    }
}
