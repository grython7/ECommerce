using BusinessDomain.Interfaces;
using BusinessDomain.Services;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.UnitofWork;
using Microsoft.Extensions.DependencyInjection;

namespace Runtime
{
    public static class ServiceExtensions
    {
        public static void ConfigureDependencyInjection(this IServiceCollection services)
        {
            // Scoped Lifetime: The Scoped lifetime ensures that a new instance of UnitOfWork is created for each HTTP request and shared within that request.
            // Dependency Injection
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
        }
    }
}
