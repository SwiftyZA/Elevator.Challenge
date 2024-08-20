using Elevator.Challenge.Core.Services;
using Elevator.Challenge.Core.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Elevator.Challenge.Core.IoC
{
    public static class IoCModule
    {
        public static void AddCoreElevatorServices(this IServiceCollection services)
        {
            services.AddSingleton<IStateService, StateService>();
            services.AddHostedService<ElevatorDirectorService>();
        }
    }
}
