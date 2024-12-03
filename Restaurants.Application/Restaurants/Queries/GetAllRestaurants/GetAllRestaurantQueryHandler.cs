using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantQueryHandler(ILogger<GetAllRestaurantQueryHandler> logger , 
        RestaurantMapper mapper , IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetAllRestaurantsQuery,
        IEnumerable<RestaurantDTO>>
    {
        public async Task<IEnumerable<RestaurantDTO>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all restaurants");

            var restaurants = await restaurantsRepository.GetAllAsync();

            var restaurantDtos = mapper.MapRestaurantListToDtoList(restaurants);

            return restaurantDtos;
        }
    }
}
