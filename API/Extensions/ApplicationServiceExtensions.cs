using MediatR;
using Microsoft.AspNetCore.Identity;
//Project Namespaces
using Application.Core;
using Application.Projects;
using Persistence;
using Domain;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddSingleton<DataContext>();
            services.AddSingleton<IPasswordHasher<AppUser>, PasswordHasher<AppUser>>();

            var ctx = services.BuildServiceProvider().GetService<DataContext>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
                });
            });

            services.AddMediatR(typeof(List.Handler));
            //Locate all the mapping profiles in the Application project
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            return services;
        }
    }
}