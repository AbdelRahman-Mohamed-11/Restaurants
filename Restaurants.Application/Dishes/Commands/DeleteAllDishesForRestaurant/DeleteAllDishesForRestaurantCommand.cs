using MediatR;

namespace Restaurants.Application.Dishes.Commands.RemoveAllDishesForRestaurant;

public record DeleteAllDishesForRestaurantCommand(int RestaurantId) : IRequest;
