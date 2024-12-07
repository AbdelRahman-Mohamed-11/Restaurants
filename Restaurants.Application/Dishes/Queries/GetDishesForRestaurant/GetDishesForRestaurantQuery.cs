using MediatR;
using Restaurants.Application.Dishes.Dtos;

namespace Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

public record GetDishesForRestaurantQuery(int RestaurantId) : IRequest<IEnumerable<DishDto>>;

