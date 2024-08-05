using Elevator.Challenge.Core;
using Elevator.Challenge.Enums;
using Elevator.Challenge.Models;

namespace Elevator.Challenge.Factories
{
    internal static class ElevatorFactory
    {
        internal static IEnumerable<ElevatorEngine> GenerateElevators(int nr, int maxPax, int tickRate,
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

                    return new ElevatorEngine(elevator, pickup, getDirective, tickRate);
                });
        }
    }
}
