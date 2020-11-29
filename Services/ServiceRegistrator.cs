using Microsoft.Extensions.DependencyInjection;
using ScheduleMaster.Services.Interfaces;

namespace ScheduleMaster.Services
{
    public static class ServiceRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddSingleton<IValidatorService, ValidatorService>()
            .AddSingleton<IDataService, BinarySerializer>()
        ;
    }
}