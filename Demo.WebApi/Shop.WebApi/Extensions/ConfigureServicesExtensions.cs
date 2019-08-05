using AutoMapper;
using Core.Configurations;
using Core.Interfaces.Repository;
using Core.Interfaces.Repository.Products;
using Core.Interfaces.Repository.Users;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Products;
using Infrastructure.Repositories.Users;
using Infrastructure.Services.Interfaces;
using Infrastructure.Services.Mappings;
using Infrastructure.Services.Products;
using Infrastructure.Services.Tokens;
using Infrastructure.Services.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Shop.WebApi.Extensions
{
    public static class ConfigureServicesExtensions
    {
        /// <summary>
        /// Register custom services
        /// </summary>
        public static void SetupAddServices(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IUserService, UserService>();

            services.AddAutoMapper(typeof(ProductMappingProfile), typeof(ProductCategoryMappingProfile));

            services.AddHealthChecks();
        }

        /// <summary>
        /// Register custom configurations
        /// </summary>
        public static void SetupAddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TokenConfiguration>(configuration.GetSection("TokenAuthentication"));
        }

        /// <summary>
        /// Setup authorization
        /// </summary>
        public static void SetupAddAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("Admin"));
                options.AddPolicy("RequireUserRole", policy => policy.RequireRole("Admin", "User"));
            });
        }

        /// <summary>
        /// Setup Swagger
        /// </summary>
        public static void SetupAddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Test API",
                    Description = "Shop demo Web API"
                });
            });
        }

        /// <summary>
        /// Setup dbcontext
        /// </summary>
        public static void SetupAddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ShopContext>(options => options.UseSqlServer(configuration.GetConnectionString("Shop")), ServiceLifetime.Scoped);
        }
    }
}
