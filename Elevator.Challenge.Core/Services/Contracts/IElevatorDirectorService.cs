using Microsoft.Extensions.Hosting;

namespace Elevator.Challenge.Core.Services.Contracts
{
    public interface IElevatorDirectorService : IHostedService, IDisposable
    {
    }
}
