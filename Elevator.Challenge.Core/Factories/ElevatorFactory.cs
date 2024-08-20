using Elevator.Challenge.Core.Managers;
using Elevator.Challenge.Domain.Enums;
using Elevator.Challenge.Domain.Models;

namespace Elevator.Challenge.Core.Factories
{
    internal static class ElevatorFactory
    {
        internal static IEnumerable<ElevatorManager> GenerateElevators(int nr, int maxPax, int tickRate,
            Func<Direction, int,int, IEnumerable<PassengerModel>> pickup,
            Func<int,int> getDirective)
        {
            return Enumerable.Range(0, nr).Select(x =>
                {
                    var elevator = new ElevatorModel()
                    {
                        Id = x,
                        MaxPax = maxPax,
                        CurrentFloor = 0,
                        Passengers = new List<PassengerModel>(),
                    };

                    return new ElevatorManager(elevator, pickup, getDirective, tickRate);
                });
        }
    }
}
