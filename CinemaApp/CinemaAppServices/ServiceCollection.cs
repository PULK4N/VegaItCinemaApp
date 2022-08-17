using CinemaAppContracts.Interfaces;
using CinemaAppContracts.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppContracts
{
    public static class ServiceCollection
    {
        public static void AddScopedServices(this IServiceCollection services)
        {
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IMovieService,MovieService>();
            services.AddScoped<IMovieScreeningService,MovieScreeningService>();
            services.AddScoped<IReservationService,ReservationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IImageService, ImageService>();
        }
    }
}
