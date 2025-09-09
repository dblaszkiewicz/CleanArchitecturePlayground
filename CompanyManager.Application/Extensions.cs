using CompanyManager.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyManager.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<IEmployeeRecordNumberService, EmployeeRecordNumberService>();

            return services;
        }
    }
}
