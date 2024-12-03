using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) {
            var applicationAssembly = typeof(DependencyInjection).Assembly;
            
            services.AddSingleton<RestaurantMapper>();

            services.AddValidatorsFromAssembly(applicationAssembly)
                    .AddFluentValidationAutoValidation();

            services.AddMediatR(opt => opt.RegisterServicesFromAssembly(applicationAssembly));
 
            return services;
       
        }
    }
}
