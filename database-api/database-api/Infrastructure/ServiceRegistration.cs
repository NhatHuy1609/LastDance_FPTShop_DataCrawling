using database_api.Data;
using database_api.Interfaces;
using database_api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace database_api.Infrastructure
{
    public static  class ServiceRegistration
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container.

            // Api Endpoint services

            // Application Use Case services

            // Data - Infrastructure services
            var connectionString = configuration.GetConnectionString("Database");

            services.AddScoped<IProductRepository, ProductRespository>();

            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.UseNpgsql(connectionString);
            });

            return services;
        }

        public static IApplicationBuilder UseServices(this IApplicationBuilder app)
        {
            // Configure the HTTP request pipeline.

            // 1. Use Api Endpoint services

            // 2. Use Application Use Case services

            // 3. use Data - Infrastructure services

            app.UseMigration<ApplicationDbContext>();

            return app;
        }
    }
}
