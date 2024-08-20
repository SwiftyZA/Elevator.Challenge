using Elevator.Challenge.Domain.Enums;
using Elevator.Challenge.Domain.Models;

namespace Elevator.Challenge.Factories
{
    internal class PassengerFactory
    {
        internal static IEnumerable<PassengerModel> GeneratePassengers(int nr, int topFloorNr)
        {
            return Enumerable.Range(0, nr).Select(x =>
            {
                var origin = GetRandomFloor(topFloorNr, -1);
                var destination = GetRandomFloor(topFloorNr, origin);
                return new PassengerModel()
                {
                    Id = x,
                    Origin = origin,
                    Destination = destination,
                    TravelDirection = origin < destination ? Direction.Up : Direction.Down
                };
            });
        }

        private static int GetRandomFloor(int topFloorNr, int excludeFloor)
        {
            var magic8Ball = new Random();
            var floors = Enumerable.Range(0, topFloorNr).Select(x => x).ToList();

            if (excludeFloor != -1)
                floors.RemoveAt(excludeFloor);
            
            var index = magic8Ball.Next(0, floors.Count);
            return floors[index];

        }
    }
}
