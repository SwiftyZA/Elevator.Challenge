using Elevator.Challenge.Core.Managers;
using Elevator.Challenge.Domain.Contracts;
using Elevator.Challenge.Domain.Enums;
using Elevator.Challenge.Domain.Models;

namespace Elevator.Challenge.Core.Factories
{
    internal static class ElevatorFactory
    {
        internal static IEnumerable<ElevatorManager> GenerateElevators(IAppSettings appSettings,
            Func<Direction, int,int, IEnumerable<PassengerModel>> pickup,
            Func<int,int> getDirective)
        {
            return Enumerable.Range(0, appSettings.ElevatorCount).Select(x =>
                {
                    var elevator = new ElevatorModel()
                    {
                        Id = x,
                        MaxPax = appSettings.MaxPax,
                        CurrentFloor = 0,
                        Passengers = new List<PassengerModel>(),
                    };

                    return new ElevatorManager(elevator, pickup, getDirective, appSettings.TickRate);
                });
        }
    }
}
