using Elevator.Challenge.Domain.Contracts;
using Elevator.Challenge.Domain.Enums;
using Elevator.Challenge.Domain.Models;

namespace Elevator.Challenge.Core.Factories
{
    internal class PassengerFactory
    {
        internal static IEnumerable<PassengerModel> GeneratePassengers(IAppSettings appSettings)
        {
            return Enumerable.Range(0, appSettings.PassengerCount).Select(x =>
            {
                var origin = GetRandomFloor(appSettings.TopFloorNr, -1);
                var destination = GetRandomFloor(appSettings.TopFloorNr, origin);
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
