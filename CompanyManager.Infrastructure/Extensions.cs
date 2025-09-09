using CompanyManager.Domain.Repositories;
using CompanyManager.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyManager.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IEmployeesRepository, EmployeesRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
