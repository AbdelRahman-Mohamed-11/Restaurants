using MediatR;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommand : IRequest<int>
{
    public string Name { get; init; } = default!;
    public string Description { get; init; } = default!;
    public string Category { get; init; } = default!;
    public bool HasDelivery { get; init; }
    public string? ContactEmail { get; set; }

    public string? ContactNumber { get; set; }

    public string? City { get; init; }
    public string? Street { get; init; }
    public string? PostalCode { get; init; }
}
