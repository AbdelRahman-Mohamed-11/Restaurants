using FluentValidation;
namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{
    private readonly List<string> ValidCategories = ["Italian", "Mexican", "Japanese", "American", "Indian"];
    public CreateRestaurantCommandValidator()
    {
        RuleFor(dto => dto.Name)
            .Length(3, 50)
            .WithMessage("Name must be between 3 and 50 characters");

        RuleFor(dto => dto.Category)
            .Must(ValidCategories.Contains)
            .WithMessage("categort must be (Italian or Mexican or Japanese or American or Indian )");



        RuleFor(dto => dto.ContactEmail)
            .EmailAddress()
            .WithMessage("Provide a valid email address");

        RuleFor(dto => dto.PostalCode)
            .Matches(@"^\d{5}(-\d{4})?$")
            .WithMessage("Please provide a valid postal code (e.g., 12345 or 12345-6789)");

        RuleFor(dto => dto.ContactNumber)
            .Matches(@"^(\+20)?(1[0125][0-9]{8}|2[0-9]{7})$")
            .WithMessage("Please provide a valid Egyptian phone number (e.g., +201012345678 or 01012345678)");
    }
}
