using Elevator.Challenge.Core.Managers;
using Elevator.Challenge.Domain.Models;

namespace Elevator.Challenge.Core.Factories
{
    public  static class FloorManagerFactory
    {
        public static IEnumerable<FloorManager> GenerateFloorManagers(IEnumerable<PassengerModel> passengers, int nrOfFloors)
        {
            return Enumerable.Range(0, nrOfFloors).Select(floor_nr => {
                var lemmings = passengers.Where(x => x.Origin == floor_nr).ToList();
                return new FloorManager(floor_nr, lemmings);
            });
        }
    }
}
