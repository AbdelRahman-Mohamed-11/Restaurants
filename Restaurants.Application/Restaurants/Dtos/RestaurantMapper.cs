using Mapster;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Dtos
{
    public class RestaurantMapper
    {
        public RestaurantMapper()
        {
            // Map Restaurant to RestaurantDTO with Address flattening
            TypeAdapterConfig<Restaurant, RestaurantDTO>.NewConfig()
                .Map(dest => dest.Street, src => src.Address != null ? src.Address.Street : null)
                .Map(dest => dest.PostalCode, src => src.Address != null ? src.Address.PostalCode : null)
                .Map(dest => dest.City, src => src.Address != null ? src.Address.City : null);

            // Map CreateRestaurantDto to Restaurant
            TypeAdapterConfig<CreateRestaurantCommand, Restaurant>.NewConfig()
                .Map(dest => dest.Address, src => src.Street != null || src.PostalCode != null || src.City != null
                    ? new Address
                    {
                        Street = src.Street,
                        PostalCode = src.PostalCode,
                        City = src.City
                    }
                    : null);

                
        }

        public RestaurantDTO MapRestaurantToDto(Restaurant restaurant)
        {
            return restaurant.Adapt<RestaurantDTO>();
        }

        public List<RestaurantDTO> MapRestaurantListToDtoList(IEnumerable<Restaurant> restaurants)
        {
            return restaurants.Adapt<List<RestaurantDTO>>();
        }

        public Restaurant MapCreateRestaurantCommandToRestaurant(CreateRestaurantCommand createRestaurantCommand)
        {
            return createRestaurantCommand.Adapt<Restaurant>();
        }

       
    }
}