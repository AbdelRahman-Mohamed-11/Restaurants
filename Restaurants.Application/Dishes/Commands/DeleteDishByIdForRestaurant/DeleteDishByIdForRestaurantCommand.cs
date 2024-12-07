using MediatR;

namespace Restaurants.Application.Dishes.Commands.RemoveDishByIdForRestaurant;

public record DeleteDishByIdForRestaurantCommand(int RestaurantId, int DishId) : IRequest;

