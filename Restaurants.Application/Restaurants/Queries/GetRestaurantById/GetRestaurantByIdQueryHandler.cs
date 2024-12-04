using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById
{
    public class GetRestaurantByIdQueryHandler(ILogger<GetRestaurantByIdQueryHandler> logger , 
        RestaurantMapper mapper,
        IRestaurantsRepository restaurantsRepository) :
        IRequestHandler<GetRestaurantByIdQuery,
            RestaurantDTO?>
    {
        public async Task<RestaurantDTO?> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting a restaurant with ID : {RestaurantId}", request.Id);

            var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);

            return restaurant != null ? mapper.MapRestaurantToDto(restaurant) : null;
        }
    }
}
